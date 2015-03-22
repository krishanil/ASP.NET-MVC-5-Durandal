requirejs.config({
    paths: {
        'text': 'lib/durandal/text',
        'durandal': 'lib/durandal',
        'plugins': 'lib/durandal/plugins',
        'transitions': 'lib/durandal/transitions',
        'knockout': 'lib/knockout-3.3.0',
        'knockout.validation': 'lib/knockout.validation',
        'jquery.utilities': 'lib/jquery.utilities',
        'toastr': 'lib/toastr',
        'app': 'app'
    },
    shim: {
        'jquery.utilities': {
            deps: ['jquery']
        },
        'bootstrap': {
            deps: ['jquery'],
            exports: 'jQuery'
        },
        'knockout.validation': {
            deps: ['knockout']
        }
    }
});

define('jquery', function () { return jQuery; });
define('linq', function () { return Enumerable; });

define(['durandal/system', 'durandal/app', 'durandal/viewLocator', 'durandal/viewEngine', 'durandal/composition', 'global/session', 'knockout', 'knockout.validation'],
    function (system, app, viewLocator, viewEngine, composition, session, ko) {

        app.title = 'Web application';

        app.configurePlugins({
            router: true,
            dialog: true,
            widget: true
        });

        composition.addBindingHandler('hasFocus');

        configureKnockout();

        app.start().then(function () {
            viewLocator.useConvention();
            viewEngine.viewExtension = '/';
            app.setRoot('app/home/shell', 'entrance');
        });

        function configureKnockout() {

            ko.validation.init({
                insertMessages: true,
                decorateElement: true,
//                errorElementClass: 'has-error',
//                errorMessageClass: 'help-block'
            });

            if (!ko.utils.cloneNodes) {
                ko.utils.cloneNodes = function (nodesArray, shouldCleanNodes) {
                    for (var i = 0, j = nodesArray.length, newNodesArray = []; i < j; i++) {
                        var clonedNode = nodesArray[i].cloneNode(true);
                        newNodesArray.push(shouldCleanNodes ? ko.cleanNode(clonedNode) : clonedNode);
                    }
                    return newNodesArray;
                };
            }

            ko.bindingHandlers.ifIsInRole = {
                init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    ko.utils.domData.set(element, '__ko_withIfBindingData', {});
                    return { 'controlsDescendantBindings': true };
                },
                update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {

                    var withIfData = ko.utils.domData.get(element, '__ko_withIfBindingData'),
                        dataValue = ko.utils.unwrapObservable(valueAccessor()),
                        shouldDisplay = session.userIsInRole(dataValue),
                        isFirstRender = !withIfData.savedNodes,
                        needsRefresh = isFirstRender || (shouldDisplay !== withIfData.didDisplayOnLastUpdate),
                        makeContextCallback = false;

                    if (needsRefresh) {

                        if (isFirstRender) {
                            withIfData.savedNodes = ko.utils.cloneNodes(ko.virtualElements.childNodes(element), true /* shouldCleanNodes */);
                        }

                        if (shouldDisplay) {
                            if (!isFirstRender) {
                                ko.virtualElements.setDomNodeChildren(element, ko.utils.cloneNodes(withIfData.savedNodes));
                            }
                            ko.applyBindingsToDescendants(makeContextCallback ? makeContextCallback(bindingContext, dataValue) : bindingContext, element);
                        } else {
                            ko.virtualElements.emptyNode(element);
                        }

                        withIfData.didDisplayOnLastUpdate = shouldDisplay;
                    }
                }
            };
        }
    });