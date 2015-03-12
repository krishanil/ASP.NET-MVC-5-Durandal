requirejs.config({
    paths: {
        'text': '../Scripts/lib/text',
        'durandal': '../Scripts/lib/durandal',
        'plugins': '../Scripts/lib/durandal/plugins',
        'transitions': '../Scripts/lib/durandal/transitions',

        'lib': '/Scripts/lib',
        'app': '/Scripts/app',
    }
});

define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['jquery', 'durandal/system', 'durandal/app', 'durandal/viewLocator', 'durandal/viewEngine','plugins/router', 'knockout'],
    function ($, system, app, viewLocator, viewEngine, router, ko) {

        app.title = 'Durandal web application';

        app.model = {
            IsAuthenticated: ko.observable(false),
            Login: function () {
                debugger;
                return router.navigate('app/account/login', false);
            },
            Register: function() {
                debugger;
                return router.navigate('app/account/register', false);
            },
            Home: function() {
                debugger;
                return router.navigate('app/home/home');
            },
        }

        ko.applyBindings(app.model, $('body')[0]);

        $.getJSON('api/AccountApi').done(function (isAuthenticated) {
            app.model.IsAuthenticated(isAuthenticated);
            app.start().then(function () {
                viewLocator.useConvention();
                viewEngine.viewExtension = '/';
                app.setRoot(isAuthenticated ? 'app/home/home' : 'app/account/login', 'entrance');
            });
        }).fail(function () {
            console.log("error");
        });

        app.configurePlugins({
            router: true,
            dialog: true
        });
    });