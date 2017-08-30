(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('EditRoomController', EditRoomController);

    EditRoomController.$inject = ['$routeParams', 'exceptionService', 'roomService'];

    function EditRoomController($routeParams, exceptionService, roomService) {
        var vm = this;
        vm.model = { id: $routeParams.id };
        vm.isSending = false;

        vm.save = save;

        activate();

        function activate()
        {
            getRoom();
        }

        function getRoom()
        {
            roomService.get(vm.model.id)
                .then(getCompleted)
                .catch(exceptionService.handle);

            function getCompleted(response)
            {
                vm.model = response;
            }            
        }

        function save()
        {
            if (!vm.isSending && vm.form.$valid) {
                if (vm.model.id) {
                    roomService.put(vm.model.id, vm.model)
                        .then(putCompleted)
                        .catch(exceptionService.handle);

                    function putCompleted(response)
                    {
                        alert("Se ha guardado correctamente");
                    }
                }
                else {

                }
            }
        }
    }
})();

