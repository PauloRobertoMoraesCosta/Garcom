angular.module('cadastros').controller('tamanhoprodutosListarCtrl', ['$rootScope', '$scope', '$state', '$stateParams', '$filter', 'ngConfirm', '$uibModal', 'ngToast', 'tamanhoprodutosWebApi',
function ($rootScope, $scope, $state, $stateParams, $filter, ngConfirm, $uibModal, ngToast, tamanhoprodutosWebApi) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
        vm.configBreadcrumb();
        vm.getTamanhoProdutos();
    };

    vm.configBreadcrumb = function(){
        $scope.breadcrumb = [
            { nome: 'Início', state: 'principal.inicio' },
            { nome: 'Tamanhos dos produtos' }
        ];
    };

    vm.resetTableFilters = function(){
        $scope.filtro = {
            nome: undefined,
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

        if(vm.tamanhoprodutos.length > 0){
            vm.tamanhoprodutos = $filter('orderBy')(vm.tamanhoprodutos, $scope.ordenacao.coluna, ($scope.ordenacao.asc ? true : false));
        }
    };

    vm.defaults = function(){
        vm.tamanhoprodutos = [];

        //$scope.$parent.$watch('grupoprodutos', function(){ $scope.retratamento(); });

        $scope.flags = {
            formExpanded: true,
            showTableFilters: false,
            mobile: window.innerWidth < 992 ? true : false,
            tablet: (window.innerWidth >= 768 && window.innerWidth < 1200) ? true : false
        };

        $scope.filtro = {
            nome: undefined,
            ativo: undefined
        };

        $scope.ordenacao = {
            coluna: 'nome',
            asc: false
        };
    };

    vm.goManutencao = function(id){
        if(id) $state.go('principal.cadastros.tamanhoprodutos.manutencao', {id: id});
        else $state.go('principal.cadastros.tamanhoprodutos.manutencao');
    };

    vm.getTamanhoProdutos = function(){
        tamanhoprodutosWebApi.getTamanhoProdutos(
            function(data){
                vm.tamanhoprodutos = data;
                //$scope.retratamento();
                vm.ordenar();
            },
            function(erro){
                $rootScope.showError(erro);
            }
        );
    };

    /*$scope.retratamento = function(){
        angular.forEach(vm.tamanhoprodutos, function(d){
            if($scope.$parent.grupoprodutos.length > 0)
                d['grupoProduto'] = ($scope.$parent.grupoprodutos) ? $filter('nomegrupoproduto')(d.grupoProdutoId, $scope.$parent.grupoprodutos) : null;
        });
    };*/

    vm.remove = function(tamanhoproduto){
        //ngConfirm('Deseja realmente excluir este tamanho?', function(){
            tamanhoprodutosWebApi.validaExcluir(tamanhoproduto.id,
                function(data){
                    if(data.length > 0)
                        $uibModal.open({
                            size: 'md',
                            templateUrl : 'src/modulos/aplicacao/cadastros/tamanhoprodutos/listar/emUso.html',
                            controller : ['$scope', '$uibModalInstance', 'produtos', 'tamanhoproduto', function($scope, $uibModalInstance, produtos, tamanhoproduto) {
                                $scope.produtos = produtos;
                                $scope.tamanhoproduto = tamanhoproduto;
                                $scope.cancelar = function(){
                                    $uibModalInstance.dismiss();
                                };
                                $scope.ok = function(){
                                    $uibModalInstance.close();
                                };
                            }],
                            resolve: {
                                tamanhoproduto: function(){ return tamanhoproduto },
                                produtos: function(){ return data }
                            }
                        }).result.then(function(){
                            tamanhoprodutosWebApi.excluir(tamanhoproduto,
                                function(){
                                    $rootScope.sucessoMsg('Exclusão realizada com sucesso. <a ng-click="desfazer(\'tamanhoprodutosWebApi\', ' + tamanhoproduto.id + ', [TOAST_ID], ' + $scope.controllerName + '.ctrl.getTamanhoProdutos)">Desfazer</a>');
                                    vm.getTamanhoProdutos();
                                },
                                function(erro){
                                    $rootScope.showError(erro);
                                }
                            );
                        });
                    else
                    {
                        tamanhoprodutosWebApi.excluir(tamanhoproduto,
                            function(){
                                $rootScope.sucessoMsg('Exclusão realizada com sucesso. <a ng-click="desfazer(\'tamanhoprodutosWebApi\', ' + tamanhoproduto.id + ', [TOAST_ID], ' + $scope.controllerName + '.ctrl.getTamanhoProdutos)">Desfazer</a>');
                                vm.getTamanhoProdutos();
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
/*.filter('nomegrupoproduto', function(){
    return function(i, grupoprodutos){
        var nome = "";
        grupoprodutos.forEach(function(element) {
            if (i == element.id)
            {
                nome = element.nome;
                return;
            }
        });

        return nome;
    }
});*/