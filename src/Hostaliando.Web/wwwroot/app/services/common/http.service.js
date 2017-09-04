(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('httpService', httpService);

    httpService.$inject = [
        '$http',
        '$q',
        'routingService'];

    function httpService(
        $http,
        $q,
        routingService) {

        var service = {
            post: post,
            get: get,
            put: put,
            delete: del,
            patch: patch
        };

        return service;

        function post(url, model, params) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http.post(routingService.configServiceUrl(url), model, params)
                .then(GetComplete.bind(null, defered), GetFailed.bind(null, defered));
            return promise;
        }

        function get(url, model) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http.get(routingService.configServiceUrl(url), model)
                .then(GetComplete.bind(null, defered), GetFailed.bind(null, defered));
            return promise;
        }

        function put(url, model, params) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http.put(routingService.configServiceUrl(url), model, params)
                .then(GetComplete.bind(null, defered), GetFailed.bind(null, defered));
            return promise;
        }

        function del(url, model) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http.delete(routingService.configServiceUrl(url), model)
                .then(GetComplete.bind(null, defered), GetFailed.bind(null, defered));
            return promise;
        }

        function patch(url, model, params) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http.patch(routingService.configServiceUrl(url), model, params)
                .then(GetComplete.bind(null, defered), GetFailed.bind(null, defered));
            return promise;
        }

        function GetComplete(defered, response) {
            defered.resolve(response.data);
        }

        function GetFailed(defered, response) {
            defered.reject(response);
        }
    }
})();