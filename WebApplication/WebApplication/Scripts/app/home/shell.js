define(['plugins/router'], function (router) {
    return {
        isAuthenticated: false,
        router: router,
        activate: function () {
            var model = this;
            return $.when($.ajax("api/AccountApi"))
                .then(function (isAuthenticated) {
                    model.isAuthenticated = isAuthenticated;
                    var routings = router.map([
                         { route: '', moduleId: 'app/home/home', title: 'Web application', nav: true, visible: true, menuItem: true },
                         { route: 'home', moduleId: 'app/home/home', title: 'Home', nav: true, visible: isAuthenticated, menuItem: true },
                         { route: 'admin', moduleId: 'app/admin/index', title: 'Admin', nav: true, visible: isAuthenticated, menuItem: true },
                         { route: 'logout', moduleId: 'app/account/logout', title: 'Logout', nav: true, visible: isAuthenticated, menuItem: false },
                         { route: 'account/login', moduleId: 'app/account/login', title: 'Login', nav: true, visible: !isAuthenticated, menuItem: false },
                         { route: 'account/register', moduleId: 'app/account/register', title: 'Register', nav: true, visible: !isAuthenticated, menuItem: false }
                    ]).buildNavigationModel().activate();

                    return routings;
            });
        }
    }
});