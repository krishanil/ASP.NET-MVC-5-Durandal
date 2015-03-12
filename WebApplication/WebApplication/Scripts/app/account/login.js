define(['lib/require', 'durandal/app', 'plugins/router', 'knockout'], function (require, app, router, ko) {
    debugger;
    return {
        router: router,
        Email: ko.observable(),
        Password: ko.observable(),
        RememberMe: ko.observable(false),
        Login: function(model, event) {
            debugger;
        },
//        activate: function () {
//            return router.activate();
//        }
    };
});