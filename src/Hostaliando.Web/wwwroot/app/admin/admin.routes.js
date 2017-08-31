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
            .when('/rooms/new', {
                templateUrl: templateService.get('rooms/edit-room'),
                controller: 'EditRoomController',
                controllerAs: 'main'
            })
            .when('/rooms', {
                templateUrl: templateService.get('rooms/list-rooms'),
                controller: 'ListRoomsController',
                controllerAs: 'main'
            })
            .when('/users/:id/edit', {
                templateUrl: templateService.get('users/edit-user'),
                controller: 'EditUserController',
                controllerAs: 'main'
            })
            .when('/users/new', {
                templateUrl: templateService.get('users/edit-user'),
                controller: 'EditUserController',
                controllerAs: 'main'
            })
            .when('/users', {
                templateUrl: templateService.get('users/list-users'),
                controller: 'ListUsersController',
                controllerAs: 'main'
            });
    }
})();