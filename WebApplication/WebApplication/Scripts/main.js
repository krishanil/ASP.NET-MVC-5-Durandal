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

define(['jquery', 'durandal/system', 'durandal/app', 'durandal/viewLocator', 'durandal/viewEngine', 'plugins/router', 'knockout'],
    function ($, system, app, viewLocator, viewEngine, router, ko) {

        app.title = 'Durandal web application';

        app.configurePlugins({
            router: true,
            dialog: true
        });

        app.model = {
            viewExtension: '/',
            animation: 'entrance',

            currentModule: '',
            loginModule: 'app/account/login',
            registerModule: 'app/account/register',
            homeModule: 'app/home/home',

            apiIsAuthUrl: 'api/AccountApi',

            IsAuthenticated: ko.observable(false),

            Login: function (model, event) {
                model.gotoModule(model, model.loginModule);
            },

            Register: function (model, event) {
                model.gotoModule(model, model.registerModule);
            },

            Home: function (model, event) {
                if (model.IsAuthenticated()) {
                    model.gotoModule(model, model.homeModule);
                }
            },

            gotoModule: function (model, modelPath) {
                if (model.currentModule != modelPath) {
                    model.currentModule = modelPath;
                    app.setRoot(model.currentModule, model.animation);
                }
            }
        }

        ko.applyBindings(app.model, $('body')[0]);

        $.getJSON(app.model.apiIsAuthUrl).done(function (isAuthenticated) {
            app.model.IsAuthenticated(isAuthenticated);
            app.start().then(function () {
                viewLocator.useConvention();
                viewEngine.viewExtension = '/';
                app.model.currentModule = isAuthenticated ? app.model.homeModule : app.model.loginModule;
                app.setRoot(app.model.currentModule, app.model.animation);
            });
        }).fail(function () {
            console.log("error");
        });
    });