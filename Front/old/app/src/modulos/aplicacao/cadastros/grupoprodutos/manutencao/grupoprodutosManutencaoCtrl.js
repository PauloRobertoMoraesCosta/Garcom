angular.module('cadastros').controller('grupoprodutosManutencaoCtrl', [
    '$rootScope', '$scope', '$state', '$stateParams', 'ngToast', 'ngConfirm', 'utils', 'perfis', 'grupoprodutosWebApi', 'tamanhoprodutosWebApi',
    function ($rootScope, $scope, $state, $stateParams, ngToast, ngConfirm, utils, perfis, grupoprodutosWebApi, tamanhoprodutosWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.setWatchers();
            vm.configBreadcrumb();
            vm.getGrupoProduto();

            $scope.$on('destroy', function(){
                if(vm.watchUltimoTamanho) vm.watchUltimoTamanho();
            })
        };

        vm.configBreadcrumb = function(){
            $scope.breadcrumb = [
                { nome: 'Início', state: 'principal.inicio' },
                { nome: 'Grupo de produtos', state: 'principal.cadastros.grupoprodutos.listar' },
                { nome: ($stateParams.id && $stateParams.id > 0) ? 'Alterar' : 'Cadastrar' }
            ];
        };

        vm.defaults = function(){
            vm.perfis = perfis;
            vm.grupoproduto = {
                id: 0,
                nome: null,
                tamanhos: [],
                dataCadastro: new Date()
            };
            vm.tamanhodExpandido = true;
        };

        vm.setWatchers = function(){
            vm.watchUltimoTamanho = $scope.$watch('ctrl.grupoproduto.tamanhos[ctrl.grupoproduto.tamanhos.length-1].id', function(newValue, oldValue){
                if(vm.grupoproduto.tamanhos.length > 0){
                    var ultimoTamanho = vm.grupoproduto.tamanhos[vm.grupoproduto.tamanhos.length-1];
                    if(ultimoTamanho.id > 0){
                        vm.grupoproduto.tamanhos.push({
                            id: null,
                            ordem: ultimoTamanho.ordem+1
                        });
                    }
                }
                else
                    vm.grupoproduto.tamanhos.push({
                        id: null,
                        ordem: 1
                    });
            });
        };

        vm.getGrupoProduto = function(){
            if($stateParams.id && $stateParams.id > 0){
                grupoprodutosWebApi.getGrupoProduto($stateParams.id,
                    function(data){
                        vm.grupoproduto = data;
                    },
                    function(erro){
                        $rootScope.showError(erro);
                    }
                );
            }
        };

        vm.moverTamanho = function(indexTamanho, cima){
            if(cima){
                var aux = vm.grupoproduto.tamanhos[indexTamanho-1];

                //Subiu (clicado)
                vm.grupoproduto.tamanhos[indexTamanho-1] = vm.grupoproduto.tamanhos[indexTamanho];
                vm.grupoproduto.tamanhos[indexTamanho-1].ordem -= 1;

                //Desceu (item acima do clicado)
                vm.grupoproduto.tamanhos[indexTamanho] = aux;
                vm.grupoproduto.tamanhos[indexTamanho].ordem += 1;
            }
            else{
                var aux = vm.grupoproduto.tamanhos[indexTamanho+1];

                //Desceu (clicado)
                vm.grupoproduto.tamanhos[indexTamanho+1] = vm.grupoproduto.tamanhos[indexTamanho];
                vm.grupoproduto.tamanhos[indexTamanho+1].ordem += 1;

                //Subiu (item abaixo do clicado)
                vm.grupoproduto.tamanhos[indexTamanho] = aux;
                vm.grupoproduto.tamanhos[indexTamanho].ordem -= 1;
            }
        };

        vm.excluirTamanho = function(indexTamanho, nome){
            var tamanho = vm.grupoproduto.tamanhos[indexTamanho];
            if(tamanho.utilizado && tamanho.id > 0)
            {
                $rootScope.showSplash = true;
                tamanhoprodutosWebApi.nomeProdutosVinculados(tamanho.id, vm.grupoproduto.id,
                    function(data){
                        $rootScope.showSplash = false;
                        if(data != null)
                        {
                            ngConfirm(
                                'O tamanho "<strong>' + tamanho.nome + '</strong>" ja está vinculado ao(s) seguinte(s) um produto(s).<br/><br/>'+
                                '<strong>' + data.join('</strong>, <strong>') + '</strong>' +
                                '<br/><br/>Deseja continuar?',
                                function(){ vm.excluirTamanhoDefinitivo(indexTamanho); }
                            );
                        }
                    },
                    function(erro){
                        $rootScope.showSplash = false;
                        $scope.$parent.loading = false;
                        $rootScope.showError(erro);
                    }
                );
            }
            else
                vm.excluirTamanhoDefinitivo(indexTamanho);
        };

        vm.excluirTamanhoDefinitivo = function(indexTamanho){
            var newList = [];
            vm.grupoproduto.tamanhos.forEach(function(item, index){
                if(index != indexTamanho){
                    newList.push(item);
                    if(index > indexTamanho) item.ordem -= 1;
                }
                else delete item;
            });
            vm.grupoproduto.tamanhos = newList;
        }

        vm.salvar = function(){
            if(vm.validar()){
                $scope.$parent.loading = true;
                vm.grupoproduto.tamanhos = vm.grupoproduto.tamanhos.length > 1 ? vm.grupoproduto.tamanhos.slice(0, -1) : [];
                if(vm.grupoproduto.id == 0)
                    grupoprodutosWebApi.cadastrar(vm.grupoproduto,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.grupoprodutos.listar');
                            ngToast.success('Grupo de produtos cadastrado com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
                else
                    grupoprodutosWebApi.alterar(vm.grupoproduto,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.grupoprodutos.listar');
                            ngToast.success('Grupo de produtos alterado com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
            }
        };

        vm.validar = function(){
            if (!vm.grupoproduto.nome) return false;
            if(!vm.validarTamanhos()) return false;
            return true;
        };

        vm.validarTamanhos = function(){
            var valido = true;
            if(vm.grupoproduto.tamanhos.length > 0){
                var qtdTamanhos = vm.grupoproduto.tamanhos.length;
                vm.grupoproduto.tamanhos.forEach(function(t, index){
                    if(index < (qtdTamanhos - 1) && !t.id){
                        valido = false;
                        return;
                    }
                });
            }
            return valido;
        };

        vm.contructor();
}]);
