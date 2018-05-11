angular.module('cadastros').controller('tamanhoprodutosManutencaoCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'ngToast', 'utils', 'perfis', 'ngConfirm', 'tamanhoprodutosWebApi',
    function ($rootScope, $scope, $state, $stateParams, ngToast, utils, perfis, ngConfirm, tamanhoprodutosWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.setWatchers();
            vm.configBreadcrumb();
            vm.getTamanhoProduto();

            $scope.$on('destroy', vm.onDestroy);
        };

        vm.onDestroy = function(){
            if(vm.watchOrdem) vm.watchOrdem();
        };

        vm.configBreadcrumb = function(){
            $scope.breadcrumb = [
                { nome: 'Início', state: 'principal.inicio' },
                { nome: 'Tamanhos dos produtos', state: 'principal.cadastros.tamanhoprodutos.listar' },
                { nome: ($stateParams.id && $stateParams.id > 0) ? 'Alterar' : 'Cadastrar' }
            ];
        };

        vm.defaults = function(){
            vm.perfis = perfis;
            vm.tamanhoproduto = {
                id: 0,
                nome: null,
                dataCadastro: new Date()
            };
        };

        vm.setWatchers = function(){
            vm.watchOrdem = $scope.$watch('ctrl.tamanhoproduto.ordem', function(newValue, oldValue){
                if(newValue !== oldValue && vm.tamanhoproduto.ordem && typeof vm.tamanhoproduto.ordem !== 'number')
                    vm.tamanhoproduto.ordem = vm.tamanhoproduto.ordem.replace(/[^0-9]/gi, '');
            });
        };

        vm.getTamanhoProduto = function(){
            if($stateParams.id && $stateParams.id > 0){
                tamanhoprodutosWebApi.getTamanhoProduto($stateParams.id,
                    function(data){
                        vm.tamanhoproduto = data;
                    },
                    function(erro){
                        $rootScope.showError(erro);
                    }
                );
            }
        };

        vm.salvar = function(){
            if (vm.validar())
            {
                $scope.$parent.loading = true
                if(vm.tamanhoproduto.id == 0)
                    tamanhoprodutosWebApi.cadastrar(vm.tamanhoproduto,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.tamanhoprodutos.listar');
                            ngToast.success('Tamanho cadastrado com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
                else
                    tamanhoprodutosWebApi.alterar(vm.tamanhoproduto,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.tamanhoprodutos.listar');
                            ngToast.success('Tamanho alterado com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
            }
        };

        vm.validar = function(){
            if(!vm.tamanhoproduto.nome) return false;
            return true;
        };

        vm.contructor();
}]);
