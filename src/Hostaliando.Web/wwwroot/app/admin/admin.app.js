(function () {
    angular.module('hostaliando', [
        // Angular modules
        'ngRoute',
        'ngStorage',
        'ngSanitize',


        // Custom modules
        'hostaliandoServices',
        'hostaliandoComponents',

        // 3rd Party Modules
        'angucomplete-alt',
        'underscore',
        'textAngular',
        'ui.bootstrap.contextMenu'
    ]);
})();