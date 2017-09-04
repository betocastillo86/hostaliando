(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('roomService', roomService);

    roomService.$inject = ['httpService'];

    function roomService(http) {

        var baseUrl = '/api/v1/rooms/';

        var service = {
            getAll: getAll,
            get: get,
            post: post,
            put: put
        };

        return service;

        function getAll(filter)
        {
            return http.get(baseUrl, { params: filter });
        }

        function get(id)
        {
            return http.get(baseUrl + id);
        }

        function post(model)
        {
            return http.post(baseUrl, model);
        }

        function put(id, model)
        {
            return http.put(baseUrl + id, model);
        }
    }
})();
