(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('routingService', routingService);

    routingService.$inject = ['$window'];

    function routingService($window) {
        var service = {
            configServiceUrl: configServiceUrl,
            pathApi: pathApi
        };

        return service;

        function configServiceUrl(localUrl, modalService) {
            if ($window.isIE) {
                var rdn = Math.floor(Math.random() * 600) + 1;
                var url = localUrl;
                return url.indexOf('?') > -1 ? url + '&rdn=' + rdn : url + '?rdn=' + rdn;
            } else {
                return localUrl;
            }
        }

        function pathApi(resource, id, complement)
        {
            if (id) {
                return '/api/v1/' + resource + '/' + id + '/' + complement;
            }
            else {
                return '/api/v1/' + resource;
            }
        }
    }
})();
