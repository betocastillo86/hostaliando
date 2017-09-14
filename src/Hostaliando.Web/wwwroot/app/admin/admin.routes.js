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
                templateUrl: templateService.get('home/dashboard'),
                controller: 'DashboardController',
                controllerAs: 'main'
            })
            .when('/calendar', {
                templateUrl: templateService.get('bookings/calendar'),
                controller: 'CalendarController',
                controllerAs: 'main'
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
            })
            .when('/hostels/:id/edit', {
                templateUrl: templateService.get('hostels/edit-hostel'),
                controller: 'EditHostelController',
                controllerAs: 'main'
            })
            .when('/hostels/new', {
                templateUrl: templateService.get('hostels/edit-hostel'),
                controller: 'EditHostelController',
                controllerAs: 'main'
            })
            .when('/hostels', {
                templateUrl: templateService.get('hostels/list-hostels'),
                controller: 'ListHostelsController',
                controllerAs: 'main'
            })
            .when('/systemsettings', {
                templateUrl: templateService.get('settings/list-settings'),
                controller: 'ListSettingsController',
                controllerAs: 'main'
            })
            .when('/notifications/:id/edit', {
                templateUrl: templateService.get('notifications/edit-notification'),
                controller: 'EditNotificationController',
                controllerAs: 'main'
            })
            .when('/notifications', {
                templateUrl: templateService.get('notifications/list-notifications'),
                controller: 'ListNotificationsController',
                controllerAs: 'main'
            })
            .when('/logs', {
                templateUrl: templateService.get('logs/list-logs'),
                controller: 'ListLogsController',
                controllerAs: 'main'
            })
            .when('/emailnotifications/:id/edit', {
                templateUrl: templateService.get('emailnotifications/edit-emailnotification'),
                controller: 'EditEmailNotificationController',
                controllerAs: 'main'
            })
            .when('/emailnotifications', {
                templateUrl: templateService.get('emailnotifications/list-emailnotifications'),
                controller: 'ListEmailNotficationsController',
                controllerAs: 'main'
            });
    }
})();