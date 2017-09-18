(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('userService', userService);

    userService.$inject = ['httpService'];

    function userService(http) {

        var baseUrl = '/api/v1/users/';

        var service = {
            getAll: getAll,
            get: get,
            post: post,
            put: put,
            postPasswordRecovery: postPasswordRecovery,
            getPasswordRecovery: getPasswordRecovery,
            putPasswordRecovery: putPasswordRecovery
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

        function postPasswordRecovery(model)
        {
            return http.post(baseUrl + 'passwordrecovery', model);
        }

        function getPasswordRecovery(token) {
            return http.get(baseUrl + 'passwordrecovery/' + token);
        }

        function putPasswordRecovery(token, model) {
            return http.put(baseUrl + 'passwordrecovery/' + token, model);
        }
    }
})();
