'use strict';
document.addEventListener("DOMContentLoaded", () => {
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
	const getData = (reversed = false) => {
		const data = [
	{
		"title": "Ïðåì³ÿ. Êðóïà Ïðåì³ÿ ãðå÷íåâàÿ ÿäðèöà áûñòðîðîçâàðèâàåìàÿ 400 ã (4823096405322)",
		"imageLink": "https://images.ua.prom.st/1935734351_w200_h200_premiya-krupa-premiya.jpg",
		"link": "https://prom.ua/p1025770411-premiya-krupa-premiya.html",
		"manufacturer": null,
		"weightInGrams": 400,
		"shop": "epicentr",
		"price": 13,
		"id": 3
	},
	{
		"title": "Êðóïà ãðå÷íåâàÿ, 212 ã",
		"imageLink": "https://images.ua.prom.st/910711515_w200_h200_krupa-grechnevaya-212.jpg",
		"link": "https://prom.ua/p585461881-krupa-grechnevaya-212.html",
		"manufacturer": null,
		"weightInGrams": 212,
		"shop": "rozetka",
		"price": 14,
		"id": 2
	},

	{	"title": "Êðóïà ãðå÷íåâàÿ, 212 ã",
		"imageLink": "https://images.ua.prom.st/2683272104_w200_h200_krupa-grechnevaya-212.jpg",
		"link": "https://prom.ua/p1280075973-krupa-grechnevaya-212.html",
		"manufacturer": "Жменька",
		"weightInGrams": 212,
		"shop": "prom",
		"price": 21,
		"id": 1
	},
	{
		"title": "Îë³ìï. Êðóïà Îë³ìï Ãðå÷íåâàÿ ßäðèöà 400ã (4820055940443)",
		"imageLink": "https://images.ua.prom.st/1935734349_w200_h200_olimp-krupa-olimp.jpg",
		"link": "https://prom.ua/p1025770405-olimp-krupa-olimp.html",
		"manufacturer": null,
		"weightInGrams": 400,
		"shop": "prom",
		"price": 22,
		"id": 4
	}]
	return reversed ? data.reverse() : data;
}

	//display data
	const displayLots = (data, filter = false) => {
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

		for (let i = 0; i < data.length; i++) {
			const currentLot = data[i];

			if (!filter || filter(currentLot)){
				showLot(currentLot);
			}
		}
	}

	//check filters
	const FILTERS = {};
	const filtersAcceptButton = document.querySelector("#filter-accept");
	const getFilters = () => {
		if (Object.keys(FILTERS) > 0) {

		}
	}

	//check sorter
	const sorterButton = document.querySelector("#sorter");
	sorterButton.reversed = false;
	sorterButton.onclick = () => {
		sorterButton.reversed = sorterButton.reversed ? false : true;
		sorterButton.innerText = sorterButton.reversed ? "Сортувати за ціною: 🠕" : "Сортувати за ціною: 🠗"
		displayLots(getData(sorterButton.reversed), getFilters());
	}

	displayLots(getData(), getFilters());
});