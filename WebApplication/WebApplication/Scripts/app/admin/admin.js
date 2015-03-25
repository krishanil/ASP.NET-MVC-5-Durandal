define(['plugins/router', 'services/security', 'knockout'], function (router, security, ko) {
    var vm = {
        router: router,
        Users: ko.observableArray(),
        activate: function () {
            var self = this;
            
            return $.when(security.getData("api/Admin/Users")).then(function (users) { self.Users(users); });
        }
    };

    return vm;
});