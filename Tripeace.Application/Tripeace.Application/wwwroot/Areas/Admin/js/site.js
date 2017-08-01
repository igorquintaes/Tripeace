var page = {
    wrapperHeight: function () {
        var originalHeight = $("#page-wrapper").height();
        var desiredHeight =
            $(window).height()
            - $("body").height()
            - $("nav[role='navigation']").height()
            + $("#page-wrapper").height();

        if (originalHeight < desiredHeight)
            $("#page-wrapper").css("height", desiredHeight);
    }
};

$(document).ready(function ($) {
    'use strict',

    page.wrapperHeight();

    $(window).resize(function () {
        page.wrapperHeight();
    });
    // dashboard menu
    $(document).ready(function() {
        var $active_item = $('#active-header').val();
        $('#' + $active_item).addClass('active');
    });
});