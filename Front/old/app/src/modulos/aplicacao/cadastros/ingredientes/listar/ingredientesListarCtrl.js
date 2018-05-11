angular.module('cadastros').controller('ingredientesListarCtrl', ['$rootScope', '$scope', '$state', '$stateParams', '$uibModal', '$filter', 'ngToast', 'ngConfirm', 'ingredienteWebApi',
function ($rootScope, $scope, $state, $stateParams, $uibModal, $filter, ngToast, ngConfirm, ingredienteWebApi) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
        vm.configBreadcrumb();
        vm.getIngredientes();
    };

    vm.configBreadcrumb = function(){
        $scope.breadcrumb = [
            { nome: 'Início', state: 'principal.inicio' },
            { nome: 'Ingredientes' }
        ];
    };

    vm.defaults = function(){
        vm.ingredientes = [];

        $scope.flags = {
            formExpanded: true,
            showTableFilters: false,
            mobile: window.innerWidth < 992 ? true : false,
            tablet: (window.innerWidth >= 768 && window.innerWidth < 1200) ? true : false
        };

        $scope.filtro = {
            descricao: undefined,
            ativo: undefined
        };

        $scope.ordenacao = {
            coluna: 'descricao',
            asc: false
        };
    };

    vm.toggleTableFilters = function(){
        $scope.flags.showTableFilters = !$scope.flags.showTableFilters;
        if($scope.flags.showTableFilters === false) vm.resetTableFilters();
    };

    vm.resetTableFilters = function(){
        $scope.filtro = {
            descricao: undefined,
            ativo: undefined
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

        if(vm.ingredientes.length > 0){
            vm.ingredientes = $filter('orderBy')(vm.ingredientes, $scope.ordenacao.coluna, ($scope.ordenacao.asc ? true : false));
        }
    };

    vm.goManutencao = function(id){
        if(id) $state.go('principal.cadastros.ingredientes.manutencao', {id: id});
        else $state.go('principal.cadastros.ingredientes.manutencao');
    };

    vm.excluir = function(ingrediente){
        //ngConfirm('Deseja realmente excluir este ingrediente?', function(){
            ingredienteWebApi.validaExcluir(ingrediente.id,
                function(data){
                    if(data.length > 0)
                        $uibModal.open({
                            size: 'md',
                            templateUrl : 'src/modulos/aplicacao/cadastros/ingredientes/listar/emUso.html',
                            controller : ['$scope', '$uibModalInstance', 'produtos', 'ingrediente', function($scope, $uibModalInstance, produtos, ingrediente) {
                                $scope.produtos = produtos;
                                $scope.ingrediente = ingrediente;

                                $scope.cancelar = function(){
                                    $uibModalInstance.dismiss();
                                };
                                $scope.ok = function(){
                                    $uibModalInstance.close();
                                };
                            }],
                            resolve: {
                                ingrediente: function(){ return ingrediente },
                                produtos: function(){ return data }
                            }
                        }).result.then(function(){
                            ingredienteWebApi.excluir(ingrediente,
                                function(){
                                    $rootScope.sucessoMsg('Exclusão realizada com sucesso. <a ng-click="desfazer(\'ingredienteWebApi\', ' + ingrediente.id + ', [TOAST_ID], ' + $scope.controllerName + '.ctrl.getIngredientes)">Desfazer</a>');
                                    vm.getIngredientes();
                                },
                                $rootScope.showError
                            );
                        });
                    else
                         ingredienteWebApi.excluir(ingrediente,
                             function(){
                                $rootScope.sucessoMsg('Exclusão realizada com sucesso. <a ng-click="desfazer(\'ingredienteWebApi\', ' + ingrediente.id + ', [TOAST_ID], ' + $scope.controllerName + '.ctrl.getIngredientes)">Desfazer</a>');
                                vm.getIngredientes();
                            },
                             $rootScope.showError
                         );
                },
                $rootScope.showError
            );
        //});
    };

    vm.getIngredientes = function(){
        ingredienteWebApi.getIngredientes(
            function(data){
                vm.ingredientes = data;
                vm.ordenar();
            },
            function(erro){
                $rootScope.showError(erro);
            }
        );
    };

    vm.contructor();
}]);