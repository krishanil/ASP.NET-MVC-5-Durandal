define(['lib/require', 'durandal/app', 'plugins/router', 'knockout', 'jquery'], function (require, app, router, ko, $) {
    debugger;
    var modelView = {
        router: router,
        Email: ko.observable().extend({
            highlight: this,
            required: true,
            minLength: 6,
            pattern: {
                message: 'Please enter a valid email address',
                params: '^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$'
            }
        }),
        Password: ko.observable().extend({
            required: true,
            minLength: 6,
        }),
        RememberMe: ko.observable(false),
        Login: function (model, event) {

            if (model.errors().length > 0) return;

            var self = model;
            var loginModel = {
                Email: self.Email(),
                Password: self.Password(),
                RememberMe: self.RememberMe()
            };

            debugger;
            $.when(
                $.ajax({
                    method: 'POST',
                    url: 'api/AccountApi',
                    data: loginModel,
                    success: function(data) {
                        debugger;
                    },
                    error: function(data) {
                        debugger;
                    }
                }).done(function(data) {
                    debugger;
                }).fail(function(data) {
                    debugger;
                })).then(function (data) {
                    debugger;
                    if (data.result == "Success") {

                    }

                });
        },

    };

    modelView.errors = ko.validation.group(modelView);
    return modelView;
});