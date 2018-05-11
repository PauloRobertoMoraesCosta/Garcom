angular.module('cadastros').controller('ingredientesManutencaoCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'ngToast', 'utils', 'perfis', 'ingredienteWebApi',
    function ($rootScope, $scope, $state, $stateParams, ngToast, utils, perfis, ingredienteWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.configBreadcrumb();
            vm.getIngrediente();
        };

        vm.configBreadcrumb = function(){
            $scope.breadcrumb = [
                { nome: 'Início', state: 'principal.inicio' },
                { nome: 'Ingredientes', state: 'principal.cadastros.ingredientes.listar' },
                { nome: ($stateParams.id && $stateParams.id > 0) ? 'Alterar' : 'Cadastrar' }
            ];
        };

        vm.defaults = function(){
            vm.perfis = perfis;
            vm.ingrediente = {
                id: 0,
                dataCadastro: new Date()
            };
        };

        vm.getIngrediente = function(){
            if($stateParams.id && $stateParams.id > 0){
                ingredienteWebApi.getIngrediente($stateParams.id,
                    function(data){
                        vm.ingrediente = data;
                    },
                    function(erro){
                        $rootScope.showError(erro);
                    }
                );
            }
        };

        vm.salvar = function(){
            if(vm.validar()){
                $scope.$parent.loading = true;
                if(vm.ingrediente.id == 0)
                    ingredienteWebApi.cadastrar(vm.ingrediente,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.ingredientes.listar');
                            ngToast.success('Ingrediente cadastrado com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
                else
                    ingredienteWebApi.alterar(vm.ingrediente,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.ingredientes.listar');
                            ngToast.success('Ingrediente alterado com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
            }
        };

        vm.validar = function(){
            if (!vm.ingrediente.descricao) return false;

            return true;
        };

        vm.contructor();
    }]);