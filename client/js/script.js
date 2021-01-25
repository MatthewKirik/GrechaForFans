'use strict';
const CONFIG = {
	MOBILE_SCRIPT_PATH: "js/mobile.js",
	API_CHEAPEST_URL: "/api/lots/cheapest",
	QUERY_PARAMETERS: { reversed: "reversed", minWeight: "fromWeight", maxWeight: "toWeight", limit: "Limit"},
	API_GET_ERROR_MESSAGE: "Помилка отримання даних",
	LINK_MESSAGE: "Перейти на сайт",
	FILTERS_TEMPLATE: { weight: {minWeight: null, maxWeight: null}, },
	SORT_PRICE_UP_MESSAGE: "Сортувати за ціною: ▲",
	SORT_PRICE_DOWN_MESSAGE: "Сортувати за ціною: ▼", 
}

document.addEventListener("DOMContentLoaded", () => {

	//include mobile.js if needed
	const include = url => {
		const script = document.createElement('script');
		script.src = url;
		document.getElementsByTagName('body')[0].appendChild(script);
	}

	if(window.innerWidth < 1023) {
		include(CONFIG.MOBILE_SCRIPT_PATH);
	}

	//reload page on resize for re-enumarating parameters in mobile.js
	document.querySelector("body").onresize = () => {
		window.location.reload();
	};


	//request data from server
	const getData = async (reversed = false, filters, limit = 50) => {
		const url = `${CONFIG.API_CHEAPEST_URL}?${CONFIG.QUERY_PARAMETERS.reversed}=${reversed}&${CONFIG.QUERY_PARAMETERS.minWeight}=${filters.weight.minWeight}&${CONFIG.QUERY_PARAMETERS.maxWeight}=${filters.weight.maxWeight}&${CONFIG.QUERY_PARAMETERS.limit}=${limit}`;

		let response = await fetch(url, {method: "GET"});

		if (response.ok) {
			let json = await response.json();
			return json;
		} else {
			console.error(CONFIG.API_GET_ERROR_MESSAGE + ": " + response.status);
		}
	}

	//display data
	const displayLots = promise => {
		promise.then(res => {
			const data = res;

			if (!data) {
				return false;
			}

			const showLot = lot => {
				const shopName = lot.shop.name;

				const shopElement 	= 	document.querySelector(`.offer-block[id="${shopName}-offers"]`);
				const offer 		= 	document.createElement("div"								  );
				const offerImage 	= 	document.createElement("img"								  );
				const textArea 		= 	document.createElement("div"								  );
				const title 		= 	document.createElement("h4"									  );
				const price 		= 	document.createElement("p"									  );
				const link 			= 	document.createElement("a"									  );

				offer.classList.add("offer");
				textArea.classList.add("text-area");
				title.id = "title";
				price.id = "price";

				Object.assign(offer, lot);

				offerImage.src = offer.imageLink;
				title.innerText = offer.title;
				price.innerText = `${offer.price.value} грн/${offer.weightInGrams}г`;
				link.href = offer.link;
				link.innerText = CONFIG.LINK_MESSAGE;

				textArea.append(title, price, link);
				offer.append(offerImage, textArea);
				shopElement.append(offer);
			}

			const shopOffers = document.querySelectorAll(".offer");

			//make offer-block clear
			shopOffers.forEach(el => {
				el.remove();
			});

			//fulfill with data
			for (let i = 0; i < data.length; i++) {
				showLot(data[i]);
			}
		});
	}

	//check filters & sorter
	const FILTERS = CONFIG.FILTERS_TEMPLATE;
	const filtersAcceptButton = document.querySelector("#filter-accept");
	const getFilters = () => {
		const minWeightInput = document.querySelector("#minWeight");
		const maxWeightInput = document.querySelector("#maxWeight");

		FILTERS.weight.minWeight = minWeightInput.value ? +minWeightInput.value : null;
		FILTERS.weight.maxWeight = maxWeightInput.value ? +maxWeightInput.value : null;
	}

	const sorterButton = document.querySelector("#sorter");
	sorterButton.reversed = false;
	sorterButton.onclick = () => {
		sorterButton.reversed = sorterButton.reversed ? false : true;
		sorterButton.innerText = sorterButton.reversed ? CONFIG.SORT_PRICE_UP_MESSAGE : CONFIG.SORT_PRICE_DOWN_MESSAGE;
		displayLots(getData(sorterButton.reversed, FILTERS));
	}

	filtersAcceptButton.onclick = () => {
		getFilters();
		displayLots(getData(sorterButton.reversed, FILTERS));
	}

	//load initial lots
	displayLots(getData(sorterButton.reversed, FILTERS));
});