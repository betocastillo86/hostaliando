(function () {
    'use strict';

    angular
        .module('hostaliando')
        .config(hostaliandoRoutes);

    hostaliandoRoutes.$inject = ['$routeProvider'];

    function hostaliandoRoutes($routeProvider, templateHandlerService)
    {
        $routeProvider
            .when('/', {
                templateUrl: getTemplate('home/dashboard')/*,
                controller: 'DashboardController',
                controllerAs: 'main'*/
            });

        function getTemplate(name)
        {
            return '/app/admin/' + name + '.html';
        }
    }
})();