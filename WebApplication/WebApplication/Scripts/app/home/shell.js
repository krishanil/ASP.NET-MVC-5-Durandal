define(['plugins/router', 'linq', 'knockout', 'jquery'], function (router, linq, ko, $) {
    return {
        isAuthenticated: false,
        brandHash: ko.observable(),
        router: router,
        activate: function () {
            var self = this;
            return $.when($.ajax("api/AccountApi"))
                .then(function (isAuthenticated) {

                    self.isAuthenticated = isAuthenticated;
                    return {
                        isAuthenticated: isAuthenticated,
                        routings: router.map([
                         { route: 'home', moduleId: 'app/home/home', title: 'Home', nav: true, visible: isAuthenticated, menuItem: true },
                         { route: 'admin', moduleId: 'app/admin/index', title: 'Admin', nav: true, visible: isAuthenticated, menuItem: true },
                         { route: 'logout', moduleId: 'app/account/logout', title: 'Logout', nav: true, visible: isAuthenticated, menuItem: false },
                         { route: 'login', moduleId: 'app/account/login', title: 'Login', nav: true, visible: !isAuthenticated, menuItem: false },
                         { route: 'register', moduleId: 'app/account/register', title: 'Register', nav: true, visible: !isAuthenticated, menuItem: false }
                        ]).buildNavigationModel().activate()
                    }

                }).then(function (initData) {

                    if (initData.isAuthenticated) {
                        self.brandHash = "#home";
                        var route = linq(router.navigationModel()).Where(function (r) { return r.hash == window.location.hash; });
                        router.navigate(linq(route).Any() ? route[0].route : 'home');
                    } else {
                        self.brandHash = "#login";
                        router.navigate('login');
                    }

                    return initData.routings;
                });
        }
    }
});