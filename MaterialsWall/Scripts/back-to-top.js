$(window).scroll(function () {
    if ($(this).scrollTop() > 530) {
        $('#to-top').fadeIn();
    } else {
        $('#to-top').fadeOut();
    }
});
