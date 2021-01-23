'use strict';
const menu = document.querySelector("#menu-block");
const arrowBox = document.querySelector("#arrow-box");
const dots = document.querySelectorAll(".dot");

const showMenu = () => {
	menu.style = "left: 0%";
	arrowBox.style = "left: 50%; transform: rotateY(180deg);";
	menu.hidden = false;
}
const hideMenu = () => {
	menu.style = "left: -75%";
	arrowBox.style = "left: 0; transform: rotateY(0deg);";
	menu.hidden = true;
}

arrowBox.onclick = () => menu.hidden ? showMenu() : hideMenu();

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