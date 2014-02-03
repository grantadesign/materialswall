var fetchUpToPage = 1;

$(document).ready(function () {
    if (window.location.hash) {
        fetchUpToPage = window.location.hash.slice(1);
        fetchPages(fetchUpToPage);
    }

    initialiseScrollLoader();
});

function fetchPages(upToPage) {
    $("#loading").show();

    for (var page = 2; page <= upToPage; page++) {
        var nextPageUrl = nextPageStub + page;

        $.ajax({
            async: false,
            url: nextPageUrl,
            success: function (data) {
                $(data).appendTo('#cardrow');
            }
        });
    }

    $('#loading').hide();
    scrollElementIntoView('#pagemarker' + upToPage);
}

function scrollElementIntoView(elementId) {
    var scrollToElement = $(elementId);
    var top = scrollToElement.offset().top;
    var height = scrollToElement.height();
    var scrollPosition = top + height;
    $('html, body').animate({ scrollTop: scrollPosition }, 500);
}

function initialiseScrollLoader() {
    $('#cardcontainer').infiniteScrollHelper({
        startingPageCount: fetchUpToPage,
        loadMore: function (pageNumber, done) {
            var nextPageUrl = nextPageStub + pageNumber;

            $("#loading").show();

            $.ajax({
                url: nextPageUrl,
                success: function (data) {
                    $('#loading').hide();
                    $(data).appendTo('#cardrow');
                    done();

                    window.location.hash = pageNumber;
                    window.location = window.location;
                }
            });
        }
    });
}
