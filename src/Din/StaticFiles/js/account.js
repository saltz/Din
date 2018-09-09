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
        $('#account-view-info').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

$(document).delegate('#account-menu-btn-addedcontent',
    'click',
    function (e) {
        hideAllViews();
        $('#account-view-addedcontent').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

$(document).delegate('#account-menu-btn-moviecalender',
    'click',
    function (e) {
        $.ajax({
            url: '/Account/GetMovieCalendarAsync',
            type: 'GET',
            success: function (view) {
                console.log('cool');
            },
            error: function (error) {
                console.log(error);
            }
        });
        hideAllViews();
        $('#account-view-moviecalender').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

$(document).delegate('#account-menu-btn-tvshowcalender',
    'click',
    function(e) {
        hideAllViews();
        $('#account-view-tvshowcalender').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

function hideAllViews() {
    $.each($('.data-pane').find('.account-partial'), function (i, e) {
        if ($(e).val() === 'visible') {
            $(e).css({ display: 'none' }).val('');
            return false;
        }
    });
}