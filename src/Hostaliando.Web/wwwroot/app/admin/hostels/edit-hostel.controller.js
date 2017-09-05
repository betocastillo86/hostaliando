(function () {
    'use strict';

    angular
        .module('hostaliando')
        .controller('EditHostelController', EditHostelController);

    EditHostelController.$inject = [
        '$routeParams',
        'routingService',
        'exceptionService',
        'modalService',
        'hostelService'];

    function EditHostelController(
        $routeParams,
        routingService,
        exceptionService,
        modalService,
        hostelService) {

        var vm = this;
        vm.model = { id: $routeParams.id };
        vm.isSending = false;
        vm.continueAfterSaving = false;
        vm.currencies = app.Settings.currencies;

        vm.changeLocation = changeLocation;
        vm.addBookingSource = addBookingSource;
        vm.removeBookingSource = removeBookingSource;
        vm.save = save;

        activate();

        function activate() {
            getHostel();
        }

        function getHostel() {
            if (vm.model.id) {
                hostelService.get(vm.model.id)
                    .then(getCompleted)
                    .catch(exceptionService.handle);

                function getCompleted(response) {
                    vm.model = response;
                    hostelService.getSourcesByHostel(vm.model.id)
                        .then(getSourcesCompleted)
                        .catch(exceptionService.handle);
                }

                function getSourcesCompleted(response)
                {
                    vm.model.sources = response;
                }
            }
        }

        function save() {
            if (!vm.isSending && vm.form.$valid) {
                if (vm.model.id) {
                    hostelService.put(vm.model.id, vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }
                else {
                    hostelService.post(vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }

                function saveCompleted(response) {
                    var message = response.id ? 'Habitación creada correctamente' : 'Habitación actualizada correctamente';
                    vm.model.id = vm.model.id || response.id;
                    var redirectAfterClose = routingService.getRoute(vm.continueAfterSaving ? 'edithostel' : 'hostels', { id: vm.model.id });

                    modalService.show({ message: message, redirectAfterClose: redirectAfterClose });
                    vm.isSending = false;
                }

                function saveError(error) {
                    exceptionService.handle(error);
                    vm.isSending = false;
                }
            }
        }

        function addBookingSource(selected)
        {
            if (selected)
            {
                if (vm.model.id) {
                    var patchJson = [{ op: 'add', path: '/sources', value: selected.originalObject.id }];
                    hostelService.patch(vm.model.id, patchJson)
                        .then(patchCompleted)
                        .catch(exceptionService.handle);

                    function patchCompleted(response)
                    {
                        vm.model.sources = vm.model.sources || [];
                        vm.model.sources.push(selected.originalObject);
                    }
                }
                else {
                    vm.model.sources = vm.model.sources || [];
                    vm.model.sources.push(selected.originalObject);
                }
            }
        }

        function removeBookingSource(source)
        {
            if (confirm("¿Seguro desea eliminar este registro?"))
            {
                if (vm.model.id) {
                    var patchJson = [{ op: 'remove', path: '/sources', value: source.id }];

                    hostelService.patch(vm.model.id, patchJson)
                        .then(patchCompleted)
                        .catch(exceptionService.handle);
                }
                else
                {
                    patchCompleted();
                }
                
                function patchCompleted(response) {
                    vm.model.sources = _.reject(vm.model.sources, { id: source.id });
                }
            }
        }

        function changeLocation(selected)
        {
            vm.model.location = selected ? selected.originalObject : undefined;
        }
    }
})();

