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
            delete: remove,
            patch: patch,
            getStatusName: getStatusName,
            cancel: cancel,
            checkin: checkin
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

        function patch(id, model) {
            return http.patch(baseUrl + id, model);
        }

        function cancel(id)
        {
            var json = [{ op: 'replace', path: '/status', value: 'Canceled' }];
            return patch(id, json);
        }

        function checkin(id) {
            var json = [{ op: 'replace', path: '/status', value: 'CheckedIn' }];
            return patch(id, json);
        }

        function remove(id)
        {
            return http.delete(baseUrl + id);
        }

        function getStatusName(status)
        {
            switch (status) {

                default:
                case 'Booked':
                    return 'Reservado';
                case 'Canceled':
                    return 'Cancelado'
                case 'CheckedIn':
                    return 'Chequeado'
            }
        }
    }
})();
