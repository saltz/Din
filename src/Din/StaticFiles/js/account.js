/* Menu Highlighting */

$(document).delegate('.nav a',
    'click',
    function() {
        $('.nav').find('.active').removeClass('active');
        $(this).parent().addClass('active');
    });

/* Menu Navigation render partial views */

$(document).delegate('#account-menu-btn-information',
    'click',
    function (e) {
        hideAllViews();
        $('#account-view-info').css({ 'display': '' });
    });

$(document).delegate('#account-menu-btn-addedcontent',
    'click',
    function (e) {
        hideAllViews();
        $('#account-view-addedcontent').css({ 'display': '' });
    });

$(document).delegate('#account-menu-btn-moviecalender',
    'click',
    function(e) {
        hideAllViews();
        $('#account-view-moviecalender').css({ 'display': '' });
    });

$(document).delegate('#account-menu-btn-tvshowcalender',
    'click',
    function(e) {
        hideAllViews();
        $('#account-view-tvshowcalender').css({ 'display': '' });
    });

function hideAllViews() {
    console.log('asdadads');
    $('#account-view-info').css({ 'display': 'hidden' });
    $('#account-view-addedcontent').css({ 'display': 'hidden' });
    $('#account-view-moviecalender').css({ 'display': 'hidden' });
    $('#account-view-tvshowcalender').css({ 'display': 'hidden' });
}