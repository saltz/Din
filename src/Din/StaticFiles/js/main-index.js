$(window).on("load",
    function () {
        $('#particles').particleground({
            dotColor: '#ff8d1c',
            lineColor: '#ff8d1c',
            parallax: true,
            parallaxMultiplier: 20
        });

        $('#login-form').submit(function (e) {
            e.preventDefault();
            showLoader();
            var data = {
                username: $('#username').val(),
                password: $('#password').val()
            };
            $.ajax({
                url: '/Authentication/LoginAsync',
                type: 'POST',
                data: data,
                success: function () {
                    location.reload();
                },
                error: function () {
                    hideLoader();
                    $('.log-status').addClass('wrong-entry');
                    $('#username').val("");
                    $('#password').val("");
                    $('.alert').fadeIn(500);
                    setTimeout("$('.alert').fadeOut(1500);", 3000);
                }
            });
        });

        $('.form-control').keypress(function () {
            $('.log-status').removeClass('wrong-entry');
        });
    });