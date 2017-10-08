(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('sessionService', sessionService);

    sessionService.$inject = ['$localStorage'];

    function sessionService($localStorage) {
        var service = {
            setCurrentUser: setCurrentUser,
            removeCurrentUser: removeCurrentUser,
            getCurrentUser: getCurrentUser,
            getToken: getToken,
            isAuthenticated: isAuthenticated,
            isAdmin: isAdmin,
            getHostel: getHostel
        };

        return service;

        function setCurrentUser(currentUser) {
            $localStorage.currentUser = currentUser;
        }

        function getCurrentUser() {
            return $localStorage.currentUser;
        }

        function removeCurrentUser() {
            $localStorage.$reset({ currentUser: undefined });
        }

        function getToken() {
            return $localStorage.currentUser ? $localStorage.currentUser.token : undefined;
        }

        function isAuthenticated() {
            return $localStorage.currentUser !== undefined;
        }

        function isAdmin()
        {
            return getCurrentUser().role === 'Admin';
        }

        function canAccessHostel(hostelId)
        {
            return isAdmin() || getCurrentUser().hostel.id == hostelId;
        }

        function getHostel()
        {
            var user = getCurrentUser();
            return user ? user.hostel : undefined;
        }
    }
})();
