; (function (window, app, $, undefined) {

    var sessionsApiUrl = '/api/sessions';

    app.dataservice = (function () {
        var getSessions = function (success, error) {
            $.ajax({
                url: sessionsApiUrl,
                type: 'GET',
                dataType: 'json',
                success: success,
                error: error
            });
        };

        return {
            getSessions: getSessions
        };
    }());
}(window, window.app = window.app || {}, jQuery));