angular.module('cadastros').controller('usuariosManutencaoCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'ngToast', 'utils', 'perfis', 'usuarioWebApi',
    function ($rootScope, $scope, $state, $stateParams, ngToast, utils, perfis, usuarioWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.configBreadcrumb();
            vm.getUsuario();
        };

        vm.configBreadcrumb = function(){
            $scope.breadcrumb = [
                { nome: 'Início', state: 'principal.inicio' },
                { nome: 'Usuários', state: 'principal.cadastros.usuarios.listar' },
                { nome: ($stateParams.id && $stateParams.id > 0) ? 'Alterar' : 'Cadastrar' }
            ];
        };

        vm.defaults = function(){
            vm.perfis = perfis;
            vm.usuario = {
                id: 0,
                login: '',
                nome: '',
                senha: '',
                confirmacaoSenha: '',
                perfilId: '',
                dataCadastro: new Date()
            };

            vm.validacaoSenha = {
                maiusculo: false,
                numero: false,
                seis: false
            };

            $scope.$watch('[ctrl.usuario.senha, ctrl.usuario.confirmacaoSenha]', function(){
                vm.validaSenha();
            });
        };

        vm.getUsuario = function(){
            if($stateParams.id && $stateParams.id > 0){
                usuarioWebApi.getUsuario($stateParams.id,
                    function(data){
                        vm.usuario = data;
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
                if(vm.usuario.id == 0)
                    usuarioWebApi.cadastrar(vm.usuario,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.usuarios.listar');
                            ngToast.success('Cadastro realizado com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
                else
                    usuarioWebApi.alterar(vm.usuario,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.usuarios.listar');
                            ngToast.success('Alterações realizadas com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
            }
        };

        vm.validar = function(){
            if(!vm.usuario.login) return false;
            if(!vm.usuario.nome) return false;
            if(!vm.usuario.perfilId) return false;
            if(!vm.usuario.dataCadastro) return false;

            if((vm.usuario.id == 0 && !vm.validaSenha()) || (vm.usuario.id > 0 && vm.usuario.senha && vm.usuario.senha != '' && !vm.validaSenha())) return false;
            if(vm.usuario.id > 0 && (vm.usuario.senha && vm.usuario.senha != '') && !vm.validaConfirmacaoSenha()) return false;

            return true;
        };

        vm.validaSenha = function(){
            var sucesso = true;
            vm.validacaoSenha = {
                maiusculo: false,
                numero: false,
                seis: false
            };

            if(utils.possuiCaracterMaiusculo(vm.usuario.senha)){
                vm.validacaoSenha.maiusculo = true;
            }else{ sucesso = false;}
            if(utils.possuiNumero(vm.usuario.senha)){
                vm.validacaoSenha.numero = true;
            }else{ sucesso = false;}
            if(vm.usuario.senha && vm.usuario.senha.length >= 6){
                vm.validacaoSenha.seis = true;
            }else{ sucesso = false;}

            return sucesso;
        };
        vm.validaConfirmacaoSenha = function(){
            if(vm.usuario.confirmacaoSenha == '' || vm.usuario.senha != vm.usuario.confirmacaoSenha)
                return false;
            else if(!vm.usuario.senha || vm.usuario.senha == '')
                return false;

            return true;
        };

        vm.contructor();
    }]);