/* variables */

var calendarData;


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
    function(e) {
        hideAllViews();
        $('#account-view-info').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

$(document).delegate('#account-menu-btn-addedcontent',
    'click',
    function(e) {
        hideAllViews();
        $('#account-view-addedcontent').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

$(document).delegate('#account-menu-btn-calendar',
    'click',
    function(e) {
        generateCalendar();
        hideAllViews();
        $('#account-view-calendar').css({ display: '', opacity: 0 }).animate({ opacity: 1 }, 500).val('visible');
    });

function hideAllViews() {
    $.each($('.data-pane').find('.account-partial'),
        function(i, e) {
            if ($(e).val() === 'visible') {
                $(e).css({ display: 'none' }).val('');
                return false;
            }
        });
}

/* Account validate email */

$(document).delegate('#invite-friend-input',
    'keyup',
    function() {
        var value = $(this).val();
        if (validateEmail(value)) {
            $('#invite-friend-btn').attr('disabled', false);
        } else {
            $('#invite-friend-btn').attr('disabled', true);
        }
    });

function validateEmail(email) {
    var re =
        /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

/* Account validate account deletion */

$(document).delegate('#delete-account-input',
    'keyup',
    function() {
        var value = $(this).val();
        if (value === 'delete') {
            $('#delete-account-btn').attr('disabled', false);
        } else {
            $('#delete-account-btn').attr('disabled', true);
        }
    });

/* Account form validations */

$(document).delegate('#account-valid-password',
    'keyup',
    function() {
        var button = $('#account-section-btn');
        var pass = $('#account-password').val();
        var pass2 = $(this);

        if (pass !== null || pass !== "") {
            if (pass !== pass2.val()) {
                button.attr('disabled', true);
                pass2.css('border-color', '#b43232');
            } else {
                button.attr('disabled', false);
                pass2.css('border-color', '#06b269');
            }
        } else {
            button.attr('disabled', false);
        }
    });

/* Account form submits */

$(document).delegate('.user-information-form',
    'submit',
    function(e) {
        showLoader();
        var formData = $(this).serialize();
        e.preventDefault();
        e.stopPropagation();
        $.ajax({
            url: '/Account/UpdatePersonalInformation/',
            type: 'POST',
            data: formData,
            success: function(view) {
                hideLoader();
                $('div.ajax-div').replaceWith(view);
                $('.ajax-div').modal('show');
            },
            error: function() {
                console.log("error");
            }
        });
    });

$(document).delegate('.account-information-form',
    'submit',
    function (e) {
        showLoader();
        var formData = $(this).serialize();
        e.preventDefault();
        e.stopPropagation();
        $.ajax({
            url: '/Account/UpdateAccountInformation/',
            type: 'POST',
            data: formData,
            success: function (view) {
                hideLoader();
                $('div.ajax-div').replaceWith(view);
                $('.ajax-div').modal('show');
            },
            error: function () {
                console.log("error");
            }
        });
    });


/* Release Calendar */

function generateCalendar() {
    showLoader();
    if (calendarData === null || calendarData === undefined) {
        getCalendarData(drawCalendar);
    } else {
        drawCalendar();
    }
    hideLoader();
}

function drawCalendar() {
    console.log('drawing calendar');
    $('#release-calendar').fullCalendar({
        defaultView: 'month',
        events: calendarData.items,
        height: 'parent',
        eventRender: function(eventObj, $el) {
            $el.popover({
                content: eventObj.title,
                trigger: 'hover',
                placement: 'top'
            });
        }
    });
    if ($('.fc-center').find('.legend').length === 0) {
        $('.fc-center').append(
            '<div class="legend"><div class="legend-item"><div id="today" class="legend-item-color"></div><p>Today</p></div><div class="legend-item">' +
            '<div id="downloaded" class="legend-item-color"></div><p>Downloaded</p></div><div class="legend-item"><div id="not-downloaded" class="legend-item-color"></div>' +
            '<p>Not Downloaded</p></div></div>');
    }
}

function getCalendarData(callback) {
    console.log('getting calendar data');
    $.ajax({
        url: '/Information/GetReleaseCalendarAsync',
        type: 'GET',
        success: function(data) {
            console.log('data recieved');
            calendarData = data;
            callback();
        },
        error: function(error) {
            console.log(error);
        }
    });
}