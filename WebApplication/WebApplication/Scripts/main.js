﻿requirejs.config({
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

define(['durandal/app', 'durandal/viewLocator', 'durandal/viewEngine'],
function (app, viewLocator, viewEngine) {

    app.title = 'Durandal web application';

    app.configurePlugins({
        router: true,
        dialog: true,
        widget: {
            kinds: ['expander']
        }
    });

    app.start().then(function () {
        viewLocator.useConvention();
        viewEngine.viewExtension = '/';
        app.setRoot('app/home/shell', 'entrance');
    });
});