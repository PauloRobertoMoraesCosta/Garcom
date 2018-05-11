angular.module('cadastros').controller('mesasListarCtrl', ['$rootScope', '$scope', '$state', '$stateParams', '$uibModal', '$filter', 'ngConfirm', 'ngToast', 'mesaWebApi',
function ($rootScope, $scope, $state, $stateParams, $uibModal, $filter, ngConfirm, ngToast, mesaWebApi) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
        vm.configBreadcrumb();
        vm.getMesas();
    };

    vm.configBreadcrumb = function(){
        $scope.breadcrumb = [
            { nome: 'Início', state: 'principal.inicio' },
            { nome: 'Mesas' }
        ];
    };

    vm.defaults = function(){
        vm.mesas = [];

        $scope.filtro = {
            descricao: undefined,
            ativo: undefined
        };

        $scope.ordenacao = {
            coluna: 'descricao',
            asc: false
        };
    };

    vm.ordenar = function(coluna){
        if(coluna){
            if(coluna === $scope.ordenacao.coluna) $scope.ordenacao.asc = !$scope.ordenacao.asc;
            else {
                $scope.ordenacao.coluna = coluna;
                $scope.ordenacao.asc = true;
            }
        }

        if(vm.mesas.length > 0){
            vm.mesas = $filter('orderBy')(vm.mesas, $scope.ordenacao.coluna, ($scope.ordenacao.asc ? true : false));
        }
    };

    vm.goManutencao = function(mesa){
        if(mesa) $state.go('principal.cadastros.mesas.manutencao', {id: mesa.id});
        else $state.go('principal.cadastros.mesas.manutencao');
    };

    vm.excluir = function(mesa){
        mesaWebApi.excluir(mesa,
                            function(){
                                $rootScope.sucessoMsg('Exclusão realizada com sucesso. <a ng-click="desfazer(\'mesaWebApi\', ' + mesa.id + ', [TOAST_ID], ' + $scope.controllerName + '.ctrl.getMesas)">Desfazer</a>');
                                vm.getMesas();
                            },
                            function(erro){
                                $rootScope.showError(erro);
                            }
         );
    };

    vm.getMesas = function(){
        mesaWebApi.getMesas(
            function(data){
                vm.mesas = data;
                vm.ordenar();
            },
            function(erro){
                $rootScope.showError(erro);
            }
        );
    };

    vm.contructor();
}]);