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
		const shopOffers = document.querySelectorAll(".offer-block");

		for (let i = 0; i < shopOffers.length; i++) {
			// shopOffers[i].innerHTML = "";

			for (let j = 0; j < data.length; j++) {
				const currentLot = data[j];
				const offer = document.createElement("div");
				const offerImage = document.createElement("img");

				offer.classList.add("offer");
				offerImage.src = currentLot.imageLink;

				shopOffers[i].appendChild(offer);
				offer.appendChild(offerImage);
			}
		}
	}

	// displayLots([
	// 	{
	// 		"title": "Êðóïà ãðå÷íåâàÿ, 212 ã",
	// 		"imageLink": "https://images.ua.prom.st/2683272104_w200_h200_krupa-grechnevaya-212.jpg",
	// 		"link": "https://prom.ua/p1280075973-krupa-grechnevaya-212.html",
	// 		"manufacturer": null,
	// 		"weightInGrams": 212,
	// 		"shop": null,
	// 		"price": null,
	// 		"id": 1
	// 																								}]);
});