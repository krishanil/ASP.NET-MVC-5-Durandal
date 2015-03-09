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

define(['jquery', 'durandal/system', 'durandal/app', 'durandal/viewLocator', 'durandal/viewEngine'], function ($, system, app, viewLocator, viewEngine) {
    //>>excludeStart("build", true);
    system.debug(true);
    //>>excludeEnd("build");
    debugger;
    app.title = 'Durandal Starter Kit';

    app.configurePlugins({
        router: true,
        dialog: true
    });

    $.getJSON('api/AccountApi').done(function (isAuthenticated) {
        debugger;
        app.start().then(function () {
            debugger;
            viewLocator.useConvention();
            viewEngine.viewExtension = '/';
            app.setRoot(isAuthenticated ? 'app/home/home' : 'app/account/login', 'entrance');
        });
    }).fail(function () {
        console.log("error");
    });
});