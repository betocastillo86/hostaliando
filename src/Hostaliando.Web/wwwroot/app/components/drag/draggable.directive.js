(function () {
    'use strict';

    angular
        .module('hostaliando')
        .directive('hostDraggable', hostDraggable);

    hostDraggable.$inject = ['$window'];

    function hostDraggable($window) {
        var directive = {
            link: link,
            restrict: 'A',
            scope: {
                dragElement: '@',
                dragCallback: '&',
                dragForbidden: '@'
            }
        };

        return directive;

        function link(scope, element, attrs) {

            element.on('dragstart', function (ev) {
                $window.selectedObject = {};
                $window.selectedObject = scope.$eval(scope.dragElement);
                console.log('selectedObject', $window.selectedObject);
            });

            element.on('dragend', function(ev) {
                $window.selectedObject = undefined;
            });

            element.on('dragover', function (ev) {
                ev.preventDefault();

                if (element[0].className.indexOf('drag-over') === -1 &&
                    $window.selectedObject &&
                    $window.selectedObject !== scope.$eval(scope.dragElement)) {

                    if (!scope.$eval(scope.dragForbidden)) {
                        element.addClass('drag-over');
                    }
                    //else {
                    //    console.log("Es prohibido");
                    //    element.addClass('drag-forbidden');
                    //}
                }
            });

            element.on('dragleave', function (ev) {
                ev.preventDefault();
                angular.element(document.getElementsByClassName('drag-over')).removeClass('drag-over');
                //angular.element(document.getElementsByClassName('drag-forbidden')).removeClass('drag-forbidden');
            });

            element.on('drop', function (ev) {
                ev.preventDefault();
                angular.element(document.getElementsByClassName('drag-over')).removeClass('drag-over');

                var toElement = scope.$eval(scope.dragElement);

                if (scope.dragCallback)
                {
                    scope.dragCallback({ from: $window.selectedObject, to: toElement });
                }

                ////if (toId) {
                ////    var fromId = $window.dragObject.elementid;


                ////    var fromElement = scope.elements.firstOrDefault(scope.searchattribute, parseInt(fromId));
                ////    var toElement = scope.elements.firstOrDefault(scope.searchattribute, parseInt(toId));

                ////    var indexTo = scope.elements.indexOf(toElement);
                ////    var indexFrom = scope.elements.indexOf(fromElement);

                ////    scope.$apply(move.bind(null, indexFrom, indexTo, fromElement));

                ////    //function errorMoving() {
                ////    //    console.log('Error moviendo');
                ////    //}
                ////}
            });
        }        
    }
})();

