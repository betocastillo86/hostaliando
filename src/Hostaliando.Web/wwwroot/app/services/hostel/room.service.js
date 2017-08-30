
(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('roomService', roomService);

    roomService.$inject = ['httpService'];

    function roomService(http) {

        var baseUrl = '/api/v1/rooms';

        var service = {
            getAll: getAll
        };

        return service;

        function getAll(filter)
        {
            return http.get(baseUrl, { params: filter });
        }
    }
})();
