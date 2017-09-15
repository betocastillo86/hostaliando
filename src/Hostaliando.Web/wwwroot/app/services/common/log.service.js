(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('logService', logService);

    logService.$inject = ['httpService'];

    function logService(http) {
        var baseUrl = '/api/v1/logs/';

        return {
            getAll: getAll,
            post: post,
            clean: clean
        };

        function getAll(filter) {
            return http.get(baseUrl, { params: filter });
        }

        function post(model) {
            return http.post(baseUrl, model);
        }

        function clean() {
            return http.post(baseUrl + 'clean');
        }
    }
})();