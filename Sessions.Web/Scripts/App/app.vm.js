; (function (window, app, dataservice, $, ko, undefined) {
    app.vm = (function () {
        var SessionList = function () {
            var self = this;
            self.sessions = ko.observableArray([]);

            self.loadSessions = function () {
                dataservice.getSessions(bindEvents, logError);
            };

            function bindEvents(data) {
                self.sessions(data);
            }

            function logError(jqXHR, textStatus, errorThrown) {
                console.log(textStatus + " " + errorThrown);
            }
        };

        return {
            SessionList: SessionList
        };
    }());
}(window, window.app = window.app || {}, window.app.dataservice, jQuery, ko));