$(document).ready(function($) {

    //first time initalization
    if ($('.menuPanel').is(':visible')) {
        $('.accountPanel').toggle();
        $('.pwdChangeContainer').toggle();
        $('.moviePanel').toggle();
        $('.tvshowPanel').toggle();
    }

    //menu items clicked
    $('.menuButton').click(function() {
        $('.menuPanel').slideToggle();
        switch ($(this).data("selection")) {
        case "account":
            $('.accountPanel').slideToggle();
            break;
        case "movie":
            $('.moviePanel').slideToggle();
            break;
        case "tvshow":
            $('.tvshowPanel').slideToggle();
            break;
        }
    });

    //backbuttons clicked
    $('.backButton').click(function() {
        switch ($(this).data('bckbtn')) {
        case "account":
            $('.accountPanel').slideToggle();
            break;
        case "movie":
            $('.moviePanel').slideToggle();
            break;
        case "tvshow":
            $('.tvshowPanel').slideToggle();
            break;
        }
        $('.menuPanel').slideToggle();
    });

    //password change button
    $('.pwdButton').click(function() {
        $('#passwordSubmit').prop("disabled", true);
        $('#password1').val("");
        $('#password2').val("");
        $('.pwdChangeContainer').toggle();
    });

    $('.pwdChangeBackButton').click(function() {
        $('.pwdChangeContainer').toggle();
    });
});
