(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('settingService', settingService);

    settingService.$inject = ['httpService'];

    function settingService(http) {
        var baseUrl = '/api/v1/systemsettings/';

        return {
            get: get,
            put: put
        };

        function get(filter) {
            return http.get(baseUrl, { params: filter });
        }

        function put(model) {
            return http.put(baseUrl + model.id, model);
        }
    }
})();