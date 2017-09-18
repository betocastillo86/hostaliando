(function(){
    'use strict';

    angular
        .module('hostaliando')
        .run(initHostaliando);

    initHostaliando.$inject = [
        '$rootScope',
        '$http',
        '$location',
        'sessionService'];

    function initHostaliando(
        $rootScope,
        $http,
        $location,
        sessionService)
    {
        if (sessionService.getCurrentUser()) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + sessionService.getToken();
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            var publicPages = ['/login', '/Login', '/passwordrecovery'];
            var restrictedPage = publicPages.indexOf($location.path()) === -1 && $location.path().indexOf('/passwordrecovery') === -1;
            if (restrictedPage && !sessionService.isAuthenticated()) {
                document.location = '/login';
            }
            else if (!restrictedPage && sessionService.isAuthenticated()) {
                document.location = '/';
            }
        });
    }
})();