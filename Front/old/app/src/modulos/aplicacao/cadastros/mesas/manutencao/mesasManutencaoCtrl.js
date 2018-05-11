angular.module('cadastros').controller('mesasManutencaoCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'ngToast', 'utils', 'perfis', 'mesaWebApi',
    function ($rootScope, $scope, $state, $stateParams, ngToast, utils, perfis, mesaWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.configBreadcrumb();
            vm.getMesa();
        };

        vm.configBreadcrumb = function(){
            $scope.breadcrumb = [
                { nome: 'Início', state: 'principal.inicio' },
                { nome: 'Mesas', state: 'principal.cadastros.mesas.listar' },
                { nome: ($stateParams.id && $stateParams.id > 0) ? 'Alterar' : 'Cadastrar' }
            ];
        };

        vm.defaults = function(){
            vm.perfis = perfis;
            vm.mesa = {
                id: 0,

                dataCadastro: new Date()
            };
        };

        vm.getMesa = function(){
            if($stateParams.id && $stateParams.id > 0){
                mesaWebApi.getMesa($stateParams.id,
                    function(data){
                        vm.mesa = data;
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
                if(vm.mesa.id == 0)
                    mesaWebApi.cadastrar(vm.mesa,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.mesas.listar');
                            ngToast.success('Mesa cadastrada com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
                else
                    mesaWebApi.alterar(vm.mesa,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.mesas.listar');
                            ngToast.success('Mesa alterada com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
            }
        };

        vm.validar = function(){
            if (!vm.mesa.descricao) return false;
            return true;
        };

        vm.contructor();
    }]);