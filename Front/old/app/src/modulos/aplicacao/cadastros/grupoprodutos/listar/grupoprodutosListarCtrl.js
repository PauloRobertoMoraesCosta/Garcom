angular.module('cadastros').controller('grupoprodutosListarCtrl', ['$rootScope', '$scope', '$state', '$stateParams', '$filter', 'ngConfirm', '$uibModal', 'ngToast', 'grupoprodutosWebApi',
function ($rootScope, $scope, $state, $stateParams, $filter, ngConfirm, $uibModal, ngToast, grupoprodutosWebApi) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
        vm.configBreadcrumb();
        vm.getGrupoProdutos();
    };

    vm.configBreadcrumb = function(){
        $scope.breadcrumb = [
            { nome: 'Início', state: 'principal.inicio' },
            { nome: 'Grupo de produtos' }
        ];
    };

    vm.toggleTableFilters = function(){
        $scope.flags.showTableFilters = !$scope.flags.showTableFilters;
        if($scope.flags.showTableFilters === false) vm.resetTableFilters();
    };

    vm.resetTableFilters = function(){
        $scope.filtro = {
            nome: undefined,
            permiteDividir: undefined
        };
    };

    vm.defaults = function(){
        vm.grupoprodutos = [];

        $scope.flags = {
            formExpanded: true,
            showTableFilters: false,
            mobile: window.innerWidth < 992 ? true : false,
            tablet: (window.innerWidth >= 768 && window.innerWidth < 1200) ? true : false
        };

        $scope.filtro = {
            nome: undefined,
            permiteDividir: undefined
        };

        $scope.ordenacao = {
            coluna: 'nome',
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

        if(vm.grupoprodutos.length > 0){
            vm.grupoprodutos = $filter('orderBy')(vm.grupoprodutos, $scope.ordenacao.coluna, ($scope.ordenacao.asc ? true : false));
        }
    };

    vm.goManutencao = function(id){
        if(id) $state.go('principal.cadastros.grupoprodutos.manutencao', {id: id});
        else $state.go('principal.cadastros.grupoprodutos.manutencao');
    };

    vm.getGrupoProdutos = function(){
        grupoprodutosWebApi.getGrupoProdutos(
            function(data){
                vm.grupoprodutos = data;
                vm.ordenar();
            },
            function(erro){
                $rootScope.showError(erro);
            }
        );
    };

    vm.remove = function(grupoproduto){
        //ngConfirm('Deseja realmente excluir este grupo de produtos?', function(){
            grupoprodutosWebApi.validaExcluir(grupoproduto.id,
                function(data){
                    if(data.length > 0)
                        $uibModal.open({
                            size: 'md',
                            templateUrl : 'src/modulos/aplicacao/cadastros/grupoprodutos/listar/emUso.html',
                            controller : ['$scope', '$uibModalInstance', 'produtos', 'grupoproduto', function($scope, $uibModalInstance, produtos, grupoproduto) {
                                $scope.produtos = produtos;
                                $scope.grupoproduto = grupoproduto;
                                $scope.cancelar = function(){
                                    $uibModalInstance.dismiss();
                                };
                                $scope.ok = function(){
                                    $uibModalInstance.close();
                                };
                            }],
                            resolve: {
                                grupoproduto: function(){ return grupoproduto },
                                produtos: function(){ return data }
                            }
                        }).result.then(function(){
                            grupoprodutosWebApi.excluir(grupoproduto,
                                function(){
                                    $rootScope.sucessoMsg('Exclusão realizada com sucesso. <a ng-click="desfazer(\'grupoprodutosWebApi\', ' + grupoproduto.id + ', [TOAST_ID], ' + $scope.controllerName + '.ctrl.getGrupoProdutos)">Desfazer</a>');
                                    vm.getGrupoProdutos();
                                },
                                function(erro){
                                    $rootScope.showError(erro);
                                }
                            );
                        });
                    else
                    {
                        grupoprodutosWebApi.excluir(grupoproduto,
                            function(){
                                $rootScope.sucessoMsg('Exclusão realizada com sucesso. <a ng-click="desfazer(\'grupoprodutosWebApi\', ' + grupoproduto.id + ', [TOAST_ID], ' + $scope.controllerName + '.ctrl.getGrupoProdutos)">Desfazer</a>');
                                vm.getGrupoProdutos();
                            },
                            function(erro){
                                $rootScope.showError(erro);
                            }
                        );
                    } 
                },
                $rootScope.showError
            );
        //});
    };

    vm.contructor();
}]);