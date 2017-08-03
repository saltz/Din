$(document).ready(function($) {
    //password validation
    $('.form-control').keyup(function () {
        var count = 0;
        var pass1 = $('#password1').val();
        var pass2 = $('#password2').val();

        if (pass1.length >= 8) {
            $('#length').removeClass('invalid').addClass('valid');
            count++;
        } else {
            $('#length').removeClass('valid').addClass('invalid');
            count--;
        }

        if (pass1.match(/\d/)) {
            $('#number').removeClass('invalid').addClass('valid');
            count++;
        } else {
            $('#number').removeClass('valid').addClass('invalid');
            count--;
        }

        if (pass1 === pass2 && count === 2) {
            console.log("pass1    " + pass1 + "    pass2    " + pass2);
            $('#passwordSubmit').prop("disabled", false);
        } else {
            $('#passwordSubmit').prop("disabled", true);
        }
    }).focus(function () {
        $('#pswd_info').show();
    }).blur(function () {
        $('#pswd_info').hide();
    });
});