$(document).ready(function() {
    /* background carousel */
    $('.carousel').carousel({
        interval: 5000
    });

    /* Home Menu animations */

    var menu = {
        body: $('.menu'),
        button: $('.menu-button'),
        tools: $('.items')
    };

    menu.button.click(function() {
        toggleMenu();
    });

    function toggleMenu() {
        menu.body.toggleClass('menu--closed');
        menu.body.toggleClass('menu--open');
        menu.tools.toggleClass('items--visible');
        menu.tools.toggleClass('items--hidden');
    }

    /* Home Menu elements */

    $('.account-btn').click(function (e) {
        showLoader();
        e.preventDefault();
        e.stopPropagation();
        $.ajax({
            url: '/Account/GetUserViewAsync',
            type: 'GET',
            success: function(view) {
                $('div.ajax-div').replaceWith(function () {
                    hideLoader();
                    return $(view).hide().fadeIn();
                });
                $('#account-view-info').val('visible');
                toggleMenu();
            },
            error: function(error) {
                console.log(error);
            }
        });
    });


    /* TEMP Launch Client */

    $('#launch-client').click(function() {
        window.location.href = 'https://plex.tv';
    });

    /* Search Movie */

    $('#search-movie-form').submit(function(e) {
        showLoader();
        e.preventDefault();
        e.stopPropagation();
        var data = {
            query: $('.search-movie').val()
        };
        $.ajax({
            url: '/Movie/SearchMovieAsync',
            type: 'POST',
            data: data,
            success: function(view) {
                hideLoader();
                $('div.ajax-div').replaceWith(view);
                $('#add-movie').modal('hide');
                $('.ajax-div').modal('show');
                $('.search-movie').val("");
            },
            error: function(error) {
                console.log(error);
            }
        });
    });

    /* Search TvShow */

    $('#search-show-form').submit(function(e) {
        showLoader();
        e.preventDefault();
        e.stopPropagation();
        var data = {
            query: $('.search-show').val()
        };
        $.ajax({
            url: '/TvShow/SearchTvShowAsync',
            type: 'POST',
            data: data,
            success: function(view) {
                hideLoader();
                $('div.ajax-div').replaceWith(view);
                $('#add-tvshow').modal('hide');
                $('.ajax-div').modal('show');
                $('.search-show').val("");
            },
            error: function() {
                console.log("error");
            }
        });
    });
});

/* Add Movie */
$(document).delegate('.add-movie',
    'click',
    function(e) {
        e.preventDefault();
        e.stopPropagation();
        var data = {
            movieData: $(this).attr('data-model')
        };
        $.ajax({
            url: '/Movie/AddMovieAsync',
            type: 'POST',
            data: data,
            success: function(view) {
                $('.modal-backdrop').remove();
                $('.ajax-div').replaceWith(view);
                $('#result-dialog').modal('show');
            },
            error: function() {
                console.log("error");
            }
        });
    });

/* Add TvShow */
$(document).delegate('.add-tvshow',
    'click',
    function(e) {
        e.preventDefault();
        e.stopPropagation();
        var data = {
            tvShowData: $(this).attr('data-model')
        };
        $.ajax({
            url: '/TvShow/AddTvShowAsync',
            type: 'POST',
            data: data,
            success: function(view) {
                $('.modal-backdrop').remove();
                $('.ajax-div').replaceWith(view);
                $('#result-dialog').modal('show');
            },
            error: function() {
                console.log("error");
            }
        });
    });

/* Partial view fade out animation */

$(document).mouseup(function(e) {
    var container = $('.partial-screen');
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        container.fadeOut(function() {
            container.replaceWith('<div class="ajax-div"></div>');
        });
    }
});