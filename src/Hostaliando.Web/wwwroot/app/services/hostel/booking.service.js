(function() {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('bookingService', bookingService);

    bookingService.$inject = ['httpService'];

    function bookingService(http) {

        var baseUrl = '/api/v1/bookings/';

        var service = {
            getAll: getAll,
            get: get,
            post: post,
            put: put,
            delete: remove
        };

        return service;

        function getAll(filter) {
            return http.get(baseUrl, { params: filter });
        }

        function get(id) {
            return http.get(baseUrl + id);
        }

        function post(model) {
            return http.post(baseUrl, model);
        }

        function put(id, model) {
            return http.put(baseUrl + id, model);
        }

        function remove(id)
        {
            return http.delete(baseUrl + id);
        }
    }
})();
