(function ($) {

    $(document).ready(function () {

        $('body').on('focus', ".floatLabel", function (e) {
            $(e.target).next().addClass("active");
        });

        $('body').on('click', ".controls label", function (e) {
            $(e.target).prev().trigger('focus');
        });

        $('body').on('blur', ".floatLabel", function (e) {
            if ($(e.target).val() === '' || $(e.target).val() === 'blank') {
                $(e.target).next().removeClass();
            }
        });
    });

})(jQuery);