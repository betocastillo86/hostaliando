(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('hostelService', hostelService);

    hostelService.$inject = ['httpService'];

    function hostelService(http) {
        var baseUrl = '/api/v1/hostels/';

        var service = {
            getAll: getAll,
            get: get,
            post: post,
            put: put,
            getSourcesByHostel: getSourcesByHostel,
            patch: patch
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

        function getSourcesByHostel(id)
        {
            return http.get(baseUrl + id + '/sources');
        }
    }
})();