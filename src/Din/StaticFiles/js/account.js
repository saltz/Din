$(document).delegate('.nav a',
    'click',
    function () {
        $('.nav').find('.active').removeClass('active');
        $(this).parent().addClass('active');
    });
