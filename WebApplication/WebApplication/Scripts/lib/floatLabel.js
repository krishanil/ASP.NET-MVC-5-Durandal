(function ($) {

//    $(document).ready(function () {
//        $("body").on("DOMNodeInserted", ".floatLabel", function () {
//            $(this).livequery('focus', function () { $(this).next().addClass("active") });
//
//            $(this).livequery('blur', function () {
//                if ($(this).val() === '' || $(this).val() === 'blank') {
//                    $(this).next().removeClass();
//                }
//            });
//        });
//    });

    $(document).ready(function () {
        $('body').on('focus', ".floatLabel", function (e) {
            debugger;
            $(e.target).next().addClass("active");
            
        });

        $('body').on('blur', ".floatLabel", function (e) {
            debugger;
            if ($(e.target).val() === '' || $(e.target).val() === 'blank') {
                $(e.target).next().removeClass();
            }

        });
    });

})(jQuery);