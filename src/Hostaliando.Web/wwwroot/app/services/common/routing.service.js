(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('routingService', routingService);

    routingService.$inject = ['$window'];

    function routingService($window) {
        var service = {
            configServiceUrl: configServiceUrl,
            getRoute: getRoute,
            getTemplate: getTemplate
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

        function getRoute(routeName, params) {
            switch (routeName) {
                case 'rooms':
                    return '/rooms';
                case 'newroom':
                    return '/rooms/new';
                case 'editroom':
                    return '/rooms/' + params.id + '/edit';
                case 'users':
                    return '/users';
                case 'newuser':
                    return '/users/new';
                case 'edituser':
                    return '/users/' + params.id + '/edit';
                case 'notifications':
                    return '/notifications';
                case 'editnotification':
                    return '/notifications/' + params.id + '/edit';
                case 'home':
                    return '/rooms';
                case 'login':
                    return '/login';
                default:
                    return '/';
            }
        }

        function getTemplate(name) {
            return '/app/admin/' + name + '.html';
        }
    }
})();