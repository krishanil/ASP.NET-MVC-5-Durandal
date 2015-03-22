(function ($) {

    $(document).ready(function () {

        $('body').on('focus', ".floatLabel", function (e) {
            $(e.target).nextAll('label').addClass("active");
            validationInput($(e.target));
        });

        $('body').on('blur', ".floatLabel", function (e) {
            validationInput($(e.target));
            if ($(e.target).val() === '' || $(e.target).val() === 'blank') {
                $(e.target).nextAll('label').removeClass();
            }
        });

        $('body').on('click', ".controls label", function (e) {
            $(e.target).prevAll('input').trigger('focus');
        });

        // validation 

        function validationInput(input) {
            if (isValid()) {
                $(input).addClass('error');
                $(input).nextAll('label').addClass('error');
            } else {
                $(input).removeClass('error');
                $(input).nextAll('label').removeClass('error');
            }

            function isValid() {
                var validationContainer = $(input).nextAll('span.validationMessage');
                return validationContainer.length > 0 && $(validationContainer[0]).text().length > 0;
            }
        }

    });

})(jQuery);