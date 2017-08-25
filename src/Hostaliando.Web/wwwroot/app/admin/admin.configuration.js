(function () {
    'use strict';

    angular.module('hostaliando')
        .config(hostaliandoConfiguration);

    hostaliandoConfiguration.$inject = ['$locationProvider', '$httpProvider', '$compileProvider'];

    function hostaliandoConfiguration($locationProvider, $httpProvider, $compileProvider)
    {
        $compileProvider.debugInfoEnabled(false);
        $compileProvider.commentDirectivesEnabled(false);
        $compileProvider.cssClassDirectivesEnabled(false);

        $locationProvider.html5Mode(true);
        $locationProvider.hashPrefix("!");
    }

})();