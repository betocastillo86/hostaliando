(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('MenuController', MenuController);

    MenuController.$inject = ['$location', 'menuService', 'exceptionService'];

    function MenuController($location, menuService, exceptionService) {
        var vm = this;
        vm.options = [];
        vm.currentOption = undefined;
        vm.currentChild = undefined;

        activate();

        function activate() {
            getOptions();
        }

        function getOptions() {
            menuService.getAll()
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response) {
                vm.options = response;

                for (var i = 0; i < vm.options.length; i++) {
                    var option = vm.options[i];

                    if (option.url == $location.$$url) {
                        vm.currentMenuOption = option.key;
                    }

                    if (option.children)
                    {
                        for (var j = 0; j < option.children.length; j++) {
                            var child = option.children[j];

                            if (child.url == $location.$$url)
                            {
                                vm.currentChild = child.key;
                                vm.currentOption = option.key;
                            }
                        }
                    }
                }
            }
        }
    }
})();