angular.module('cadastros').controller('produtosListarCtrl', ['$rootScope', '$scope', '$state', '$stateParams', '$filter', 'ngConfirm', '$uibModal', 'ngToast', 'produtoWebApi',
function ($rootScope, $scope, $state, $stateParams, $filter, ngConfirm, $uibModal, ngToast, produtoWebApi) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
        vm.configBreadcrumb();
        vm.getProdutos();
    };

    vm.configBreadcrumb = function(){
        $scope.breadcrumb = [
            { nome: 'Início', state: 'principal.inicio' },
            { nome: 'Produtos' }
        ];
    };

    vm.resetTableFilters = function(){
        $scope.filtro = {
            nome: undefined,
            grupoProdutoId: undefined,
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

        if(vm.produtos.length > 0){
            vm.produtos = $filter('orderBy')(vm.produtos, $scope.ordenacao.coluna, ($scope.ordenacao.asc ? true : false));
        }
    };

    vm.defaults = function(){
        vm.produtos = [];

        $scope.$parent.$watch('grupoprodutos', function(){ $scope.retratamento(); });

        $scope.flags = {
            formExpanded: true,
            showTableFilters: false,
            mobile: window.innerWidth < 992 ? true : false,
            tablet: (window.innerWidth >= 768 && window.innerWidth < 1200) ? true : false
        };

        $scope.filtro = {
            nome: undefined,
            grupoProdutoId: undefined,
            ativo: undefined,
            codigoRapido: undefined
        };

        $scope.ordenacao = {
            coluna: 'nome',
            asc: true
        };
    };

    vm.goManutencao = function(id){
        if(id) $state.go('principal.cadastros.produtos.manutencao', {id: id});
        else $state.go('principal.cadastros.produtos.manutencao');
    };

    vm.getProdutos = function(){
        produtoWebApi.listar(
            function(data){
                vm.produtos = data;
                $scope.retratamento();
                vm.ordenar();
            },
            $rootScope.showError
        );
    };

    $scope.retratamento = function(){
        angular.forEach(vm.produtos, function(p){
            if($scope.$parent.grupoprodutos.length > 0)
                p['grupoProduto'] = ($scope.$parent.grupoprodutos) ? $filter('nomegrupoproduto')(p.grupoProdutoId, $scope.$parent.grupoprodutos) : null;

            if(p.codigoRapido === null) p.codigoRapido = '';
        });
    };

    vm.remove = function(produto){
        produtoWebApi.jaUtilizado(produto.id,
            function(data){
                if(data === true)
                    ngConfirm({
                        title: 'Produto em uso',
                        msg: 'Não posso excluir o produto ' + produto.nome + ', pois ele foi utilizado em um ou mais pedidos.<br/>Posso inativa-lo?',
                        okBtn: 'Inativar',
                        cancelBtn: 'Cancelar'
                    }, function(){
                        $rootScope.sucessoMsg('Produto inativado com sucesso.');
                        vm.getProdutos();
                    });
                else
                    produtoWebApi.excluir(produto,
                        function(){
                            $rootScope.sucessoMsg('Exclusão realizada com sucesso. <a ng-click="desfazer(\'produtoWebApi\', ' + produto.id + ', [TOAST_ID], ' + $scope.controllerName + '.ctrl.getProdutos)">Desfazer</a>');
                            vm.getProdutos();
                        },
                        function(erro){
                            $rootScope.showError(erro);
                        }
                    );
            },
            $rootScope.showError
        );
    };

    vm.contructor();
}]).filter('nomegrupoproduto', function(){
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
});