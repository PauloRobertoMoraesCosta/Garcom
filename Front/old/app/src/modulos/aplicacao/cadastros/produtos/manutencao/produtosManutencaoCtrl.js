angular.module('cadastros').controller('produtosManutencaoCtrl', ['$rootScope', '$scope', '$timeout', '$state', '$stateParams', '$uibModal', '$filter', 'ngToast', 'utils', 'ngConfirm', 'produtoWebApi',
    function ($rootScope, $scope, $timeout, $state, $stateParams, $uibModal, $filter, ngToast, utils, ngConfirm, produtoWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.configBreadcrumb();
            vm.getProduto();
        };

        vm.configBreadcrumb = function(){
            $scope.breadcrumb = [
                { nome: 'Início', state: 'principal.inicio' },
                { nome: 'Produtos', state: 'principal.cadastros.produtos.listar' },
                { nome: ($stateParams.id && $stateParams.id > 0) ? 'Alterar' : 'Cadastrar' }
            ];
        };

        vm.defaults = function(){
            vm.ingredientesExpandido = false;
            vm.grupoProdutoSelecionado = null;

            vm.setWatchers();
        };

        vm.setWatchers = function(){
            vm.watch = $scope.$watch('ctrl.produto.grupoProdutoId', function(newValue, oldValue){
                if(vm.produto && vm.produto.grupoProdutoId && vm.produto.grupoProdutoId !== oldValue) {
                    if(!vm.aposGetProduto && vm.produto.produtosTamanhosValor !== null && vm.produto.produtosTamanhosValor.length > 0 && (vm.possuiValoresTamanhosDefinidos() || vm.possuiValoresIngredientesDefinidos(null, true)))
                        ngConfirm('Ao trocar de grupo você perderá os valores já definidos para tamanhos e ingredientes. Deseja continuar?',
                            //Sim
                            vm.redefineGrupo,
                            //Não
                            function(){ vm.produto.grupoProdutoId = oldValue; }
                        );
                    else{
                        vm.redefineGrupo();
                    }

                    if(oldValue === null) vm.ingredientesExpandido = true;
                }
            });

            vm.watchIngredientes = $scope.$watch('ctrl.produto.produtoIngredientes[ctrl.produto.produtoIngredientes.length-1].ingredienteId', function(newValue, oldValue){
                if(!vm.aposGetProduto && vm.produto && vm.produto.produtoIngredientes.length > 0){
                    var ultimoIngrediente = vm.produto.produtoIngredientes[vm.produto.produtoIngredientes.length-1];
                    if(ultimoIngrediente.ingredienteId > 0){
                        var copiaTamanhos = angular.copy(ultimoIngrediente.produtosIngredientesTamanhosValor);
                        copiaTamanhos.forEach(function(t){ t.valor = null; });
                        vm.produto.produtoIngredientes.push({
                            id: 0,
                            produtoId: ($stateParams.id && $stateParams.id > 0) ? $stateParams.id : 0,
                            ingredienteId: 0,
                            opcional: false,
                            adicional: false,
                            valorAdicional: null,
                            produtosIngredientesTamanhosValor: copiaTamanhos
                        });
                    }
                }
            });
        };

        vm.redefineGrupo = function(){
            if(!vm.aposGetProduto) {
                vm.produto.valor = null;

                vm.grupoProdutoSelecionado = vm.getGrupoProdutoById(vm.produto.grupoProdutoId);
                if (vm.grupoProdutoSelecionado.tamanhos && vm.grupoProdutoSelecionado.tamanhos.length > 0) {

                    //Limpa tamanhos do produto
                    //vm.produto.produtosTamanhosValor = [];

                    //Limpa tamanhos dos ingredientes
                    //vm.produto.produtoIngredientes.forEach(function (ingrediente) {
                        //ingrediente.produtosIngredientesTamanhosValor = [];
                    //});

                    vm.grupoProdutoSelecionado.tamanhos.forEach(function (t, index) {

                        //Redefine os novos tamanhos do produto
                        var qtdTamanhosProduto = vm.produto.produtosTamanhosValor.length;
                        if(qtdTamanhosProduto > 0 && (index+1) <= qtdTamanhosProduto){
                            vm.produto.produtosTamanhosValor[index].tamanhoProdutoId = t.id;
                            vm.produto.produtosTamanhosValor[index].valor = null;
                        }
                        else
                            vm.produto.produtosTamanhosValor.push({
                                id: 0,
                                produtoId: ($stateParams.id && $stateParams.id > 0) ? $stateParams.id : 0,
                                tamanhoProdutoId: t.id,
                                valor: null
                            });

                        //Redefine os novos tamanhos dos ingredientes
                        vm.produto.produtoIngredientes.forEach(function (ingrediente) {
                            ingrediente.produtoId = ($stateParams.id && $stateParams.id > 0) ? $stateParams.id : 0;

                            var qtdTamanhosIngrediente = ingrediente.produtosIngredientesTamanhosValor.length;
                            if(qtdTamanhosIngrediente > 0 && (index+1) <= qtdTamanhosIngrediente){
                                ingrediente.produtosIngredientesTamanhosValor[index].tamanhoProdutoId = t.id;
                                ingrediente.produtosIngredientesTamanhosValor[index].valor = null;
                            }
                            else
                                ingrediente.produtosIngredientesTamanhosValor.push({
                                    id: 0,
                                    produtoIngredienteId: 0,
                                    tamanhoProdutoId: t.id,
                                    valor: null
                                });
                        });
                    });

                    //Verifica se o produto contem tamanhos a mais que o esperado
                    if(vm.produto.produtosTamanhosValor.length > vm.grupoProdutoSelecionado.tamanhos.length)
                        vm.produto.produtosTamanhosValor = vm.produto.produtosTamanhosValor.slice(0,vm.grupoProdutoSelecionado.tamanhos.length);

                    //Verifica se os ingredientes contem tamanhos a mais que o esperado
                    vm.produto.produtoIngredientes.forEach(function (ingrediente) {
                        if(ingrediente.produtosIngredientesTamanhosValor.length > vm.grupoProdutoSelecionado.tamanhos.length)
                            ingrediente.produtosIngredientesTamanhosValor = ingrediente.produtosIngredientesTamanhosValor.slice(0,vm.grupoProdutoSelecionado.tamanhos.length);
                    });
                }
                else {
                    vm.produto.produtoIngredientes.forEach(function (ingrediente) {
                        ingrediente.produtoId = ($stateParams.id && $stateParams.id > 0) ? $stateParams.id : 0;
                        ingrediente.produtosIngredientesTamanhosValor = [];
                    });

                    vm.produto.produtosTamanhosValor = [];
                    vm.grupoProdutoSelecionado = null;
                }
            }
            else{
                vm.grupoProdutoSelecionado = vm.getGrupoProdutoById(vm.produto.grupoProdutoId);

                var pIngrediente = {
                    id: 0,
                    produtoId: ($stateParams.id && $stateParams.id > 0) ? $stateParams.id : 0,
                    ingredienteId: 0,
                    opcional: false,
                    adicional: false,
                    valorAdicional: null,
                    produtosIngredientesTamanhosValor: []
                };

                if (vm.grupoProdutoSelecionado.tamanhos && vm.grupoProdutoSelecionado.tamanhos.length > 0) {
                    vm.grupoProdutoSelecionado.tamanhos.forEach(function (t) {
                        pIngrediente.produtosIngredientesTamanhosValor.push({
                            id: 0,
                            produtoIngredienteId: 0,
                            tamanhoProdutoId: t.id,
                            valor: null
                        });
                    });
                }
                else {
                    vm.grupoProdutoSelecionado = null;
                }

                vm.produto.produtoIngredientes.push(pIngrediente);
            }
        };

        vm.possuiValoresIngredientesDefinidos = function(ingrediente, sePossuiPeloMenosUmValorDefinido){
            var possuiValorDefinido = false;

            //Analisar todos os ingredientes da lista
            if(!ingrediente) {
                var todosIngredValoresDefinidos = true;
                //Possui varios tamanhos
                if(vm.grupoProdutoSelecionado && vm.grupoProdutoSelecionado.tamanhos.length > 0 && vm.produto.produtoIngredientes && vm.produto.produtoIngredientes.length > 0){
                    var algumAdicionalMarcado = false;
                    var possuiPeloMenosUmValorDefinido = false;
                    if(vm.produto.produtoIngredientes.length > 0){
                        vm.produto.produtoIngredientes.forEach(function(i){
                            possuiValorDefinido = false;
                            if(i.ingredienteId !== 0){
                                if(i.adicional) {
                                    algumAdicionalMarcado = true;
                                    i.produtosIngredientesTamanhosValor.forEach(function (tamanho) {
                                        if (tamanho.valor > 0) {
                                            possuiValorDefinido = true;
                                        }
                                    });

                                    if(!sePossuiPeloMenosUmValorDefinido && !possuiValorDefinido){
                                        todosIngredValoresDefinidos = false;
                                        return;
                                    }
                                    else if(sePossuiPeloMenosUmValorDefinido && possuiValorDefinido){
                                        possuiPeloMenosUmValorDefinido = true;
                                        return;
                                    }

                                }
                            }
                        });
                        if(sePossuiPeloMenosUmValorDefinido)
                            return possuiPeloMenosUmValorDefinido;
                        if(!algumAdicionalMarcado) return true;
                    }
                    //else if(vm.produto.produtoIngredientes[0].ingredienteId === 0) return todosIngredValoresDefinidos;

                    return todosIngredValoresDefinidos;
                }
                //Possui somente um tamanho
                else if(vm.produto.produtoIngredientes && vm.produto.produtoIngredientes.length > 0){
                    var algumAdicionalMarcado = false;
                    if(vm.produto.produtoIngredientes.length > 0){
                        vm.produto.produtoIngredientes.forEach(function(i){
                            possuiValorDefinido = false;
                            if(i.ingredienteId !== 0){
                                if(i.adicional) {
                                    algumAdicionalMarcado = true;
                                    if(i.valorAdicional > 0) possuiValorDefinido = true;

                                    if(!possuiValorDefinido){
                                        todosIngredValoresDefinidos = false;
                                        return;
                                    }
                                }
                            }
                        });

                        if(!algumAdicionalMarcado) return true;
                    }

                    return todosIngredValoresDefinidos;
                }
                else
                    return true;
            }
            //Analisar ingrediente em específico
            else{
                if(ingrediente.adicional)
                    ingrediente.produtosIngredientesTamanhosValor.forEach(function(tamanho){
                        if(tamanho.valor > 0) possuiValorDefinido = true;
                    });

                return possuiValorDefinido;
            }
        };

        vm.possuiValoresTamanhosDefinidos = function(){
            if(vm.produto.produtosTamanhosValor && vm.produto.produtosTamanhosValor.length > 0){
                var possuiValorDefinido = false;
                vm.produto.produtosTamanhosValor.forEach(function(v){
                    if(v.valor > 0){
                        possuiValorDefinido = true;
                        return;
                    }
                });
                return possuiValorDefinido;
            }
            return false;
        };

        vm.cleanWatchers = function(){
            if(vm.watch) vm.watch();
        };

        vm.getGrupoProdutoById = function(id){
            var gProduto = null;
            $scope.$parent.grupoprodutos.forEach(function(g){
                if(g.id == id){
                    gProduto = g;
                    return;
                }
            });
            return gProduto;
        };

        vm.getIngredienteById = function(id){
            var ingrediente = null;
            $scope.$parent.ingredientes.forEach(function(i){
                if(i.id == id){
                    ingrediente = i;
                    return;
                }
            });
            return ingrediente;
        };

        vm.getProduto = function(){
            if($stateParams.id && $stateParams.id > 0)
                produtoWebApi.selecionar($stateParams.id,
                    function(data){
                        vm.aposGetProduto = true;
                        vm.produto = data;

                        if(vm.produto.produtoIngredientes.length > 0)
                            vm.ingredientesExpandido = true;

                        if(vm.produto.nomeImagem && vm.produto.nomeImagem != '') vm.imgUrl = vm.produto.nomeImagem;

                        $timeout(function(){ vm.aposGetProduto = false }, 500);
                    },
                    function(erro){
                        $rootScope.showError(erro);
                    }
                );
            else
                vm.produto = {
                    id: 0,
                    valor: null,
                    imagem: null,
                    produtosTamanhosValor: [],
                    produtoIngredientes: [
                        {
                            id: 0,
                            produtoId: ($stateParams.id && $stateParams.id > 0) ? $stateParams.id : 0,
                            ingredienteId: 0,
                            opcional: false,
                            adicional: false,
                            valorAdicional: null,
                            produtosIngredientesTamanhosValor: [
                                {
                                    id: 0,
                                    produtoIngredienteId: 0,
                                    tamanhoProdutoId: 0,
                                    valor: null
                                }
                            ]
                        }
                    ],
                    grupoProdutoId: null,
                    preparadoCozinha: true,
                    ativo: true
                };
        };

        vm.jaPossuiTamanhoNoIgrediente = function(ingrediente, tamanho){
            var jaPossui = false;
            ingrediente.produtosIngredientesTamanhosValor.forEach(function(tamanhoIgre){
                if(tamanhoIgre.tamanhoProdutoId == tamanho.id){
                    jaPossui = true;
                    return;
                }
            });
            return jaPossui;
        };

        vm.jaPossuiTamanhoNoProduto = function(tamanho){
            var jaPossui = false;
            vm.produto.produtosTamanhosValor.forEach(function(ptv){
                if(ptv.tamanhoProdutoId == tamanho.id){
                    jaPossui = true;
                    return;
                }
            });
            return jaPossui;
        };

        vm.definirValorTamanhos = function(){
            if(vm.produto.produtosTamanhosValor.length != vm.grupoProdutoSelecionado.tamanhos.length){
                vm.grupoProdutoSelecionado.tamanhos.forEach(function (t) {
                    if(!vm.jaPossuiTamanhoNoProduto(t))
                        vm.produto.produtosTamanhosValor.push({
                            id: 0,
                            produtoId: ($stateParams.id && $stateParams.id > 0) ? $stateParams.id : 0,
                            tamanhoProdutoId: t.id,
                            valor: null
                        });
                });

            }

            var backupTamanhos = angular.copy(vm.produto.produtosTamanhosValor);
            $uibModal.open({
                size: 'md',
                templateUrl : 'src/modulos/aplicacao/cadastros/produtos/manutencao/definirValorTamanhos.html',
                controller : ['$scope', '$uibModalInstance', 'tamanhos', 'produto', function($scope, $uibModalInstance, tamanhos, produto) {
                    $scope.tamanhos = tamanhos;
                    $scope.produto = produto;

                    $scope.cancelar = function(){
                        $uibModalInstance.dismiss();
                    };
                    $scope.ok = function(){
                        $uibModalInstance.close();
                    };
                }],
                resolve: {
                    tamanhos: function(){ return vm.grupoProdutoSelecionado.tamanhos },
                    produto: function(){ return vm.produto }
                }
            }).result.then(
                function(){
                },
                function(){
                    vm.produto.produtosTamanhosValor = backupTamanhos;
                });
        };

        vm.definirValorIngredientes = function(ingrediente){
            if(ingrediente.produtosIngredientesTamanhosValor.length != vm.grupoProdutoSelecionado.tamanhos.length){
                vm.grupoProdutoSelecionado.tamanhos.forEach(function (t) {
                    if(!vm.jaPossuiTamanhoNoIgrediente(ingrediente, t))
                        ingrediente.produtosIngredientesTamanhosValor.push({
                            id: 0,
                            produtoIngredienteId: ingrediente.id,
                            tamanhoProdutoId: t.id,
                            valor: null
                        });
                });
                //ORDENAR BASE NO TAMANHO DO PRODUTO
            }
            var backupTamanhos = angular.copy(ingrediente.produtosIngredientesTamanhosValor);
            var nomeIngrediente = vm.getIngredienteById(ingrediente.ingredienteId).descricao;
            $uibModal.open({
                size: 'md',
                templateUrl : 'src/modulos/aplicacao/cadastros/produtos/manutencao/definirValorIngredientes.html',
                controller : ['$scope', '$uibModalInstance', 'ingrediente', 'tamanhos', 'nomeIngrediente', function($scope, $uibModalInstance, ingrediente, tamanhos, nomeIngrediente) {
                    $scope.ingrediente = ingrediente;
                    $scope.tamanhos = tamanhos;
                    $scope.nomeIngrediente = nomeIngrediente;

                    $scope.cancelar = function(){
                        $uibModalInstance.dismiss();
                    };
                    $scope.ok = function(){
                        $uibModalInstance.close();
                    };
                }],
                resolve: {
                    ingrediente: function(){ return ingrediente },
                    tamanhos: function(){ return vm.grupoProdutoSelecionado.tamanhos },
                    nomeIngrediente: function(){ return nomeIngrediente }
                }
            }).result.then(
                //OK
                function(){
                },
                //CANCELAR
                function(){
                    ingrediente.produtosIngredientesTamanhosValor = backupTamanhos;
                });
        };

        vm.excluirIngrediente = function(ingrediente){
            var newList = [];
            vm.produto.produtoIngredientes.forEach(function(i){
                if(i.$$hashKey !== ingrediente.$$hashKey) newList.push(i);
            });
            vm.produto.produtoIngredientes = newList;
        };

        vm.removerImg = function() {
            vm.produto.imagem = null;
            vm.produto.nomeImagem = null;
            vm.imgUrl = null;
        };

        vm.definirImagem = function(){
            var modal = $uibModal.open({
                size: 'md',
                templateUrl: 'templateCache/myContent.htm',
                controllerAs: 'ctrl',
                controller: ['$scope', '$timeout', '$uibModalInstance', 'imagemAtual',
                    function($scope, $timeout, $uibModalInstance, imagemAtual) {
                        $scope.titulo = 'Selecionar imagem';
                        $scope.okMsg = 'OK';
                        $scope.cancelarMsg = 'Cancelar';

                        $scope.imagemAtual = imagemAtual;
                        $scope.ext = imagemAtual;

                        $scope.cropper = {
                            sourceImage: null,
                            croppedImage: null,
                            types: '.jpeg,.jpeg,.png'
                        };

                        $scope.widthCrop = 190;
                        $scope.heightCrop = 135;

                        $scope.width = 570;
                        $scope.height = 340;

                        $timeout(function() {
                            angular.element(document.querySelector('#fileInput')).on('change', function(evt) {
                                var file = evt.currentTarget.files[0];

                                if(file.type && file.type.indexOf('/') > -1)
                                    $scope.ext = file.type.split('/')[1];
                                else if(file.type)
                                    $scope.ext = file.type;
                            });

                            //$scope.openDialog();
                        }, 200, false);

                        $scope.openDialog = function() {
                            angular.element(document.querySelector('#fileInput')).click();
                        };

                        $scope.cancelar = function(){
                            $uibModalInstance.dismiss();
                        };
                        $scope.ok = function(){
                            $uibModalInstance.close({
                                b64: $scope.cropper.croppedImage,
                                extensao: $scope.ext//'png'
                            });
                        };
                    }
                ],
                resolve: {
                    imagemAtual: function(){ return vm.produto.imagem; }
                }
            });
            modal.result.then(
                //OK
                function(imagem){
                    vm.produto.imagem = imagem.b64.split(',')[1] + '#' + imagem.extensao;
                    vm.imgUrl = imagem.b64;
                },
                //CANCELAR
                function(){}
            );
        };

        vm.salvar = function(){
            if(vm.validar()){
                $scope.$parent.loading = true;

                var produto = angular.copy(vm.produto);

                vm.removeIngredientesNaoDefinidos(produto);

                if(vm.produto.id == 0)
                    produtoWebApi.cadastrar(produto,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.produtos.listar');
                            ngToast.success('Produto cadastrado com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );
                else
                    produtoWebApi.alterar(produto,
                        function(data){
                            $scope.$parent.loading = false;
                            $state.go('principal.cadastros.produtos.listar');
                            ngToast.success('Produto alterado com sucesso.');
                        },
                        function(erro){
                            $scope.$parent.loading = false;
                            $rootScope.showError(erro);
                        }
                    );

            }
        };

        vm.removeIngredientesNaoDefinidos = function(prod){
            var produto = prod ? prod : vm.produto;

            if(produto.produtoIngredientes && produto.produtoIngredientes.length > 0) {
                var newArray = [];
                produto.produtoIngredientes.forEach(function (pi) {
                    if(pi.ingredienteId !== 0 && pi.ingredienteId > 0)
                        newArray.push(pi);
                });

                if(newArray.length !== produto.produtoIngredientes.length)
                    produto.produtoIngredientes = newArray;
            }
        };

        vm.validar = function(){
            //Nome
            if(!vm.produto.nome) return false;

            //Grupo do produto
            if(!vm.produto.grupoProdutoId) return false;

            //Valor ou valores
            if((!vm.grupoProdutoSelecionado || vm.grupoProdutoSelecionado.tamanhos.length < 1) && (!vm.produto.valor || vm.produto.valor == 0)) return false;
            else if(vm.grupoProdutoSelecionado && vm.grupoProdutoSelecionado.tamanhos.length > 0 && !vm.possuiValoresTamanhosDefinidos()) return false;

            //Definição de ingredientes
            if(!vm.possuiValoresIngredientesDefinidos()) return false;

            return true;
        };

        vm.contructor();
}])
.filter('nometamanhoproduto', function(){
    return function(i, tamanhosProduto){
        var nome = "";
        tamanhosProduto.forEach(function(element) {
            if (i == element.id)
            {
                nome = element.nome;
                return;
            }
        });

        return nome;
    }
});
