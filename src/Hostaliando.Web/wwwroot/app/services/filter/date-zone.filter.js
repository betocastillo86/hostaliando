(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .filter('dateZone', dateZone);

    dateZone.$inject = ['sessionService'];

    function dateZone(sessionService) {
        return function (value) {
            if (value) {
                return moment(value).add(sessionService.getCurrentUser().timeZone, 'hours').format(app.Settings.general.dateFormat + ' HH:mm');
            }
            else
            {
                return '';
            }
        };
    }
})();