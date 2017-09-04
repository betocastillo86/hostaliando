(function() {
    angular.module('hostaliandoComponents')
        .directive('hostPager', listPager);

    listPager.$inject = ['templateService'];

    function listPager(templateService) {
        var directive = {
            restrict: 'E',
            templateUrl: templateService.getComponent('pager/pager'),
            controller: 'PagerController',
            controllerAs: 'pager',
            scope: {
                model: '=',
                pageChanged: '&'
            },
            bindToController: true
        };

        return directive;
    };
})();