'use strict';
const menu = document.querySelector("#menu-block");
const menuWidth = +window.getComputedStyle(menu).getPropertyValue('width').replace("px", "");
const arrowBox = document.querySelector("#arrow-box");
const dots = document.querySelectorAll(".dot");

const showMenu = () => {
	menu.style = "left: 0%";
	arrowBox.style = `left: ${menuWidth}px; transform: rotateY(180deg);`;
	menu.hidden = false;
}
const hideMenu = () => {
	menu.style = "left: -75%";
	arrowBox.style = "left: 0; transform: rotateY(0deg);";
	menu.hidden = true;
}

arrowBox.onclick = () => menu.hidden ? showMenu() : hideMenu();

//dragging sidebar menu
arrowBox.ontouchstart = event => {
	const touchX = event.touches[0].clientX;
	const touchY = event.touches[0].clientY;
	const arrowBoxStartX = +window.getComputedStyle(arrowBox).getPropertyValue('left').replace("px", "");
	
	arrowBox.ontouchmove = event => {
		const direction = menu.hidden ? 1 : 0;
		const arrowBoxX = arrowBoxStartX + event.touches[0].clientX - touchX;
		const menuX = arrowBoxX - menuWidth;
		if (arrowBoxX < menuWidth && arrowBoxX >= 0) {
			arrowBox.style = `left: ${arrowBoxX}px; transform: rotateY(${direction === 1 ? "0deg" : "180deg"}); transition: 0s;`;
			menu.style = `left: ${menuX}px; transition: 0s;`;
		}
	}

	arrowBox.ontouchend = event => {
		arrowBox.ontouchmove = undefined;
		const x = event.changedTouches[0].clientX;
		if (x > menuWidth * 0.5) {
			showMenu();
		} else {
			hideMenu();
		}
	}
}

//doing staff with dots
const activeButton = "⚪";
const inactiveButton = "🔵";

const showPage = id => {
	const nextElement = document.querySelector(`#${id}`);
	if (nextElement) {
		const nextDot = document.querySelector(`.dot[for = "${id}"]`);
		const activeDot = document.querySelector(".dot[active = 'true']");
		const activeElementID = activeDot.getAttribute("for");
		const activeElement = document.querySelector(`#${activeElementID}`);

		activeDot.innerText = inactiveButton;
		nextDot.innerText = activeButton;

		activeDot.setAttribute("active", false);
		nextDot.setAttribute("active", true);

		activeElement.classList.add("hidden");
		nextElement.classList.remove("hidden");

		document.querySelector("header").scrollIntoView({behavior: "smooth"});
	}
}

for (let i = 0; i < dots.length; i++) {
	dots[i].setAttribute("active", i === 0 ? true : false);
}

dots.forEach(dot => {
	dot.innerText = dot.getAttribute("active") == "true" ? activeButton : inactiveButton;
	dot.onclick = () => {
		showPage(dot.getAttribute("for"));
	};
});