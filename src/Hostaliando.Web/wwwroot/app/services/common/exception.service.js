﻿(function () {
    'use strict';

    angular
        .module('hostaliandoServices')
        .factory('exceptionService', exceptionService);

    function exceptionService() {
        var service = {
            handle: handle
        };

        function handle(exception) {
            alert(exception);
        }

        return service;
    }
})();