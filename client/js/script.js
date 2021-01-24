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

	const displayLots = data => {
		const shopOffers = document.querySelectorAll(".offer");

		shopOffers.forEach(el => {
			el.remove();
		});

		for (let i = 0; i < data.length; i++) {
			const currentLot = data[i];
			const shopName = currentLot.shop;

			const shopElement = document.querySelector(`.offer-block[id="${shopName}-offers"]`);
			const offer = document.createElement("div");
			const offerImage = document.createElement("img");
			const textArea = document.createElement("div");
			const title = document.createElement("h4");
			const price = document.createElement("p");
			const link = document.createElement("a");

			offer.classList.add("offer");
			textArea.classList.add("text-area");
			title.id = "title";
			price.id = "price";

			Object.assign(offer, data);
			console.log(offer);
			offerImage.src = currentLot.imageLink;
			title.innerText = currentLot.title;
			price.innerText = currentLot.price + ` грн/${currentLot.weightInGrams}г`;
			link.href = currentLot.link;
			link.innerText = "Перейти на сайт";

			shopElement.append(offer);
			offer.append(offerImage,textArea);
			textArea.append(title, price, link);
		}
	}

	displayLots([
		{
			"title": "Êðóïà ãðå÷íåâàÿ, 212 ã",
			"imageLink": "https://images.ua.prom.st/2683272104_w200_h200_krupa-grechnevaya-212.jpg",
			"link": "https://prom.ua/p1280075973-krupa-grechnevaya-212.html",
			"manufacturer": null,
			"weightInGrams": 212,
			"shop": "prom",
			"price": null,
			"id": 1
																									}]);
});