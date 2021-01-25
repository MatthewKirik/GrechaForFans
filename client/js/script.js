'use strict';
document.addEventListener("DOMContentLoaded", () => {
	import { config } from "./config.js";
	console.log(config.message);

	//include mobile.js if needed
	const include = url => {
		const script = document.createElement('script');
		script.src = url;
		document.getElementsByTagName('body')[0].appendChild(script);
	}

	if(window.innerWidth < 1023) {
		include("js/mobile.js");
	}

	//reload page on resize for re-enumarating parameters in mobile.js
	document.querySelector("body").onresize = () => {
		window.location.reload();
	};


	//request data from server
	const getData = async (reversed = false, filters, limit = 50) => {
		const url = `/api/lots/cheapest?reversed=${reversed}&fromWeight=${filters.weight.minWeight}&toWeight=${filters.weight.maxWeight}&Limit=${limit}`;

		let response = await fetch(url, {method: "GET"});

		if (response.ok) {
			let json = await response.json();
			return json;
		} else {
			alert("Ошибка HTTP: " + response.status);
		}
	}

	//display data
	const displayLots = promise => {
		promise.then(res => {
			const data = res;

			const showLot = lot => {
				const shopName = lot.shop;

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
				price.innerText = offer.price + ` грн/${offer.weightInGrams}г`;
				link.href = offer.link;
				link.innerText = "Перейти на сайт";

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
	const FILTERS = {
		weight: {minWeight: null, maxWeight: null},
	};
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
		sorterButton.innerText = sorterButton.reversed ? "Сортувати за ціною: ▲" : "Сортувати за ціною: ▼"
		displayLots(getData(sorterButton.reversed, FILTERS));
	}

	filtersAcceptButton.onclick = () => {
		getFilters();
		displayLots(getData(sorterButton.reversed, FILTERS));
	}

	//load initial lots
	displayLots(getData());
});