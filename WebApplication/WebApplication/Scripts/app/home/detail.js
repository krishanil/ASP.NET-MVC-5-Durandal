define(['plugins/router', 'knockout'], function (router, ko) {
    var vm = {
        router: router,
        title: ko.observable('Details'),
        description: ko.observable(''),
        place: ko.observable(''),
        activate: function (data) {
            this.place(data.title);
            this.description(data.description);
        }
    };

    return vm;
});