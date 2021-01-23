'use strict';
document.addEventListener("DOMContentLoaded", () => {
	const include = url => {
		const script = document.createElement('script');
		script.src = url;
		document.getElementsByTagName('body')[0].appendChild(script);
	}
	if(window.innerWidth < 1023) {
		include("js/mobile.js");
	}
	document.querySelector("body").onresize = () => {
		window.location.reload();
	};
});