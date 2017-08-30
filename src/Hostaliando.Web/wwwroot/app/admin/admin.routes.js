(function () {
    'use strict';

    angular
        .module('hostaliando')
        .config(hostaliandoRoutes);

    hostaliandoRoutes.$inject = ['$routeProvider', 'templateService'];

    function hostaliandoRoutes($routeProvider, templateService)
    {
        $routeProvider
            .when('/', {
                templateUrl: templateService.get('home/dashboard')/*,
                controller: 'DashboardController',
                controllerAs: 'main'*/
            })
            .when('/rooms/:id/edit', {
                templateUrl: templateService.get('rooms/edit-room'),
                controller: 'EditRoomController',
                controllerAs: 'main'
            })
            .when('/rooms', {
                templateUrl: templateService.get('rooms/list-rooms'),
                controller: 'ListRoomsController',
                controllerAs: 'main'
            });
    }
})();