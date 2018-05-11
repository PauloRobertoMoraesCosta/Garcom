angular.module('mainApp').directive('radio', function () {
    return{
        restrict: 'E',
        require: [
            '^ngModel',
            '^ngValue'
        ],
        scope: {
            ngModel: '=',
            ngValue: '=',
            ngClass: '=',
            disabled: '=',
            multiselect: '@',
            left: '@',
            right: '@',
            class: '@'
        },
        transclude: true,
        templateUrl: 'src/modulos/main/diretivas/radio/radio.html',
        controllerAs: 'ctrl',
        controller: ['$scope', '$element', function($scope, $element){
            var vm = this;

            vm.contextualiza = function(){
                vm.defaults();

                $scope.$watch('ngModel', function(){
                    if(!vm.multiSelect()) $scope.checked = $scope.ngModel === $scope.ngValue;
                    else $scope.checked = vm.jaExisteNaLista($scope.ngValue);
                });
            };

            vm.defaults = function(){
                $scope.checked = false;
                $scope.element = $element[0];
                $scope.css = vm.getCss();
            };

            vm.toggleCheck = function(){
                if(vm.multiSelect()) {
                    if ($scope.ngModel == null) $scope.ngModel = [];

                    if (vm.jaExisteNaLista($scope.ngValue)) {
                        var newList = [];
                        $scope.ngModel.forEach(function (val) {
                            if (val != $scope.ngValue)
                                newList.push(val);
                        });

                        $scope.ngModel = newList;
                        $scope.checked = false;
                    }
                    else {
                        $scope.ngModel.push($scope.ngValue);
                        $scope.checked = true;
                    }
                }
                else{
                    $scope.checked = !$scope.checked;
                    if($scope.checked)
                        $scope.ngModel = $scope.ngValue;
                    else
                        $scope.ngModel = null;
                }
            };

            vm.jaExisteNaLista = function(value){
                var existe = false;
                $scope.ngModel.forEach(function(val){
                    if(val == value){
                        existe = true;
                        return;
                    }
                });

                return existe;
            };

            vm.multiSelect = function(){
                if($scope.multiselect != null && typeof $scope.multiselect == 'string'){
                    return $scope.multiselect == 'true';
                }
                else if($scope.multiselect == null)
                    return false;
                else
                    return $scope.multiselect;
            };

            vm.getCss = function(){
                var css = {'display': 'table', 'width': '100%', 'height': 'inherit'};
                if($scope.left != null && typeof $scope.left == 'string'){
                    if($scope.left == 'true' || $scope.left == '') {
                        css.width = 'auto';
                        css['padding-right'] = '7px';
                        return css;
                    }
                }
                else if($scope.right != null && typeof $scope.right == 'string'){
                    if($scope.right == 'true' || $scope.right == '') {
                        css.width = 'auto';
                        css['padding-right'] = '7px';
                        return css;
                    }
                }

                if($scope.element.style.float == 'left' || $scope.element.style.float == 'right'){
                    css.width = 'auto';
                    css['padding-right'] = '7px';
                }

                return css;
            };

            vm.contextualiza();
        }]
    };
});