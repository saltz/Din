function showLoader() {
    $('.lds-ring').css({ visibility: "visible", opacity: 0 }).animate({ opacity: 1 }, 500);
}

function hideLoader() {
    $('.lds-ring').css({ visibility: "hidden", opacity: 0 });
}