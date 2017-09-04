(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('notificationService', notificationService);

    notificationService.$inject = ['httpService'];

    function notificationService(http) {
        var baseUrl = '/api/v1/notifications/';

        return {
            getAll: getAll,
            getById: getById,
            put: put,
            getMyNotifications: getMyNotifications
        };

        function getAll(filter) {
            return http.get(baseUrl, { params: filter });
        }

        function getById(id) {
            return http.get(baseUrl + id);
        }

        function put(id, model) {
            return http.put(baseUrl + id, model);
        }

        function getMyNotifications(filter) {
            return http.get(baseUrl + 'mine', { params: filter });
        }
    }
})();