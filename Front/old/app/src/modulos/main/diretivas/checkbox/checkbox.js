angular.module('mainApp').directive('checkbox', function () {
    return{
        restrict: 'E',
        require: [
            '^ngModel',
            '^checkedValue',
            '^uncheckedValue'
        ],
        scope: {
            ngModel: '=',
            checkedValue: '=',
            uncheckedValue: '=',
            ngClass: '=',
            multiselect: '@',
            disabled: '=',
            small: '@',
            left: '@',
            right: '@',
            center: '@',
            class: '@'
        },
        transclude: true,
        templateUrl: 'src/modulos/main/diretivas/checkbox/checkbox.html',
        controllerAs: 'ctrl',
        controller: ['$scope', '$element', function($scope, $element){
            var vm = this;

            vm.contextualiza = function(){
                vm.defaults();

                $scope.$watch('ngModel', function(){
                    if(!vm.multiSelect()) $scope.checked = $scope.ngModel === $scope.checkedValue;
                    else $scope.checked = vm.jaExisteNaLista($scope.checkedValue);
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

                    if (vm.jaExisteNaLista($scope.checkedValue)) {
                        var newList = [];
                        $scope.ngModel.forEach(function (val) {
                            if (val != $scope.checkedValue)
                                newList.push(val);
                        });

                        $scope.ngModel = newList;
                        $scope.checked = false;
                    }
                    else {
                        $scope.ngModel.push($scope.checkedValue);
                        $scope.checked = true;
                    }
                }
                else{
                    $scope.checked = !$scope.checked;
                    if($scope.checked)
                        $scope.ngModel = $scope.checkedValue;
                    else
                        $scope.ngModel = $scope.uncheckedValue;
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
                    return true;
                else
                    return $scope.multiselect;
            };

            vm.getCss = function(){
                var css = {'display': 'table', 'width': '100%', 'height': 'inherit'};
                if($scope.left != null && typeof $scope.left == 'string'){
                    if($scope.left == 'true' || $scope.left == '') {
                        css.width = 'auto';
                        css['padding-right'] = '7px';
                        $element.css('display','table');
                        $element.css('float','left');
                        return css;
                    }
                }
                else if($scope.right != null && typeof $scope.right == 'string'){
                    if($scope.right == 'true' || $scope.right == '') {
                        css.width = 'auto';
                        css['padding-right'] = '7px';
                        $element.css('display','table');
                        $element.css('float','right');
                        return css;
                    }
                }

                return css;
            };


            $scope.getCenter = function(){ return $scope.center != null && typeof $scope.center == 'string' && ($scope.center === 'true' || $scope.center === ''); };
            $scope.getSmall = function(){ return $scope.small != null && typeof $scope.small == 'string' && ($scope.small === 'true' || $scope.small === ''); };

            vm.contextualiza();
        }]
    };
});
