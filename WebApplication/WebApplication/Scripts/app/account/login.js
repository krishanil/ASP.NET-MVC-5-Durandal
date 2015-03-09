define(['plugins/router', 'durandal/app', 'durandal/system'], function (router, app, system) {
    //>>excludeStart("build", true);
    system.debug(true);
    //>>excludeEnd("build");
    debugger;
    return {
        router: router,
        search: function () {
            //It's really easy to show a message box.
            //You can add custom options too. Also, it returns a promise for the user's response.
            app.showMessage('Search not yet implemented...');
        },
//        activate: function () {
//            router.map([
//                { route: '', title: 'Welcome', moduleId: 'app/home/welcome', nav: true },
//                { route: 'flickr', moduleId: 'app/home/flickr', nav: true }
//            ]).buildNavigationModel();
//
//            return router.activate();
//        }
    };
});