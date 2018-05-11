angular.module('mainApp').directive('btnSelect', ['$state',
    function ($state) {
        return{
            restrict: 'E',
            require: [
                '^ngItens',
                '^selected'
            ],
            scope: {
                ngDroped: '=',
                ngItens: '=',
                selected: '=',
                btnClass: '@'
            },
            transclude: true,
            templateUrl: 'src/modulos/main/diretivas/btn-select/btnSelect.html',
            controllerAs: 'ctrl',
            controller: ['$scope', '$element', function($scope, $element){
                var vm = this;

                vm.contextualiza = function(){
                    vm.defaults();
                    vm.setWatchers();

                    $scope.$on('destroy', function(){
                        if(vm.watchNgItens) vm.watchNgItens();
                        if(vm.watchNgDroped) vm.watchNgDroped();
                        if(vm.watchSelected) vm.watchSelected();
                        if(vm.watchButton) vm.watchButton();
                        angular.element(document).unbind('click');
                        angular.element($element).unbind('keydown');
                    });

                };

                vm.defaults = function(){
                    vm.itensMenu = [];
                    vm.droped = false;
                    vm.selected = $scope.selected ? $scope.selected : null;
                    $scope.widthButton = 130;

                    if($scope.btnClass === '' || $scope.btnClass === null || $scope.btnClass === undefined)
                        $scope.btnClass = 'btn-none';

                    $scope.element = $element;
                    vm.btnEl = $element.children()[0];
                    vm.menuEl = $element.children()[1];
                };

                vm.setWatchers = function(){
                    vm.watchNgItens = $scope.$watch('ngItens', function(){
                        vm.itensMenu = $scope.ngItens;
                    });
                    vm.watchNgDroped = $scope.$watch('ngDroped', function(){
                        vm.droped = $scope.ngDroped;
                    });
                    vm.watchSelected = $scope.$watch('selected', function(){
                        vm.selected = $scope.selected;
                    });
                    vm.watchButton = $scope.$watch(
                        function(){ return vm.btnEl.clientWidth; },
                        function(value){ $scope.widthButton = value + 10; });

                    angular.element(document).bind('click', function(e){
                        if(vm.droped && !vm.isDescendant(vm.btnEl, e.target) && !vm.isDescendant(vm.menuEl, e.target))
                            $scope.$apply(function(){ vm.droped = false; });
                    });

                    angular.element($element).bind('keydown', function(e){
                        if(e.keyCode == 27)
                            $scope.$apply(function(){ vm.droped = false; });
                    });
                };

                vm.toggleDrop = function(){
                    vm.droped = !vm.droped;
                };

                vm.click = function(item){
                    if(item.state) $state.go(item.state);
                    else if(item.fn) item.fn();

                    $scope.selected = item;
                    vm.toggleDrop();
                };

                vm.isDescendant = function(parent, child) {
                    var node = child;
                    while (node != null) {
                        if (node == parent) {
                            return true;
                        }
                        node = node.parentNode;
                    }
                    return false;
                };

                vm.contextualiza();
            }]
        };
    }
]);
