const menu = document.querySelector("#menu-block");
const arrow_box = document.querySelector("#arrow-box");
const showMenu = () => {
	menu.style = "left: 0%";
	arrow_box.style = "left: 50%; transform: rotateY(180deg);";
	menu.hidden = false;
}
const hideMenu = () => {
	menu.style = "left: -75%";
	arrow_box.style = "left: 0; transform: rotateY(0deg);";
	menu.hidden = true;
}

arrow_box.onclick = () => menu.hidden ? showMenu() : hideMenu();