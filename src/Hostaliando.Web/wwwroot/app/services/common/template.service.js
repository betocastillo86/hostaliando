(function() {
    'use strict';

    angular
        .module('hostaliandoServices')
        .constant("templateService", {
            "get": function(name) {
                return '/app/admin/' + name + '.html';
            },
            "getComponent": function(name) {
                return '/app/components/' + name + '.html';
            }
        });
})();
