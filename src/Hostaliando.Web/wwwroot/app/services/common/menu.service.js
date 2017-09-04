(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('menuService', menuService);

    menuService.$inject = ['httpService'];

    function menuService(http) {
        var baseUrl = '/api/v1/menuoptions';

        var service = {
            getAll: getAll
        };
        return service;

        function getAll() {
            return http.get(baseUrl);
        }
    }
})();