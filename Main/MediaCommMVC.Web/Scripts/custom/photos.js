$(function () {
    var imageWrapper = $(".highslide-gallery ul li");

    var originialBorder;

    imageWrapper.mouseenter(function () {
        originialBorder = $(this).css("border");
        $(this).css("border", "1px solid red");
    });

    imageWrapper.mouseleave(function () {
        $(this).css("border", originialBorder);
    });
});