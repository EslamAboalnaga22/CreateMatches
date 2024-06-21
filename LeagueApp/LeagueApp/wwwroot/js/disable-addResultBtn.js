let btnAdd = document.querySelectorAll("#AddResultBtn");


btnAdd.forEach((element) => {
    if (!(element.previousElementSibling.value == "") &&
        !(element.nextElementSibling.nextElementSibling.value == "")) {

        // make "AddResultBtn" disable --> if input has value
        element.style.pointerEvents = "none";
        element.style.cursor = "default";

        // make "EditResultBtn" able --> if "AddResultBtn" is disable
        element.nextElementSibling.style.pointerEvents = "auto";
        element.nextElementSibling.style.cursor = "pointer";
    }
})

