define(['lib/require', 'durandal/app', 'plugins/router', 'knockout'], function (require, app, router, ko) {
    debugger;
    return {
        router: router,
        Email: ko.observable(),
        Password: ko.observable(),
        RememberMe: ko.observable(false),
        
//        activate: function () {
//            debugger;
//            return router.map([
//                { route: '', moduleId: 'app/home/home', nav: true },
//                { route: '', moduleId: 'app/account/login', nav: true },
//                { route: '', moduleId: 'app/account/register', nav: true }
//            ]).buildNavigationModel()
//              .mapUnknownRoutes('app/home/home', 'not-found')
//              .mapUnknownRoutes('app/account/login', 'not-found')
//              .mapUnknownRoutes('app/account/register', 'not-found')
//              .activate();
//        }
    };
});