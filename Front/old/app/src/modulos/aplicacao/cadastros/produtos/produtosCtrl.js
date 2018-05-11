angular.module('cadastros').controller('produtosCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'tamanhoprodutosWebApi', 'grupoprodutosWebApi', 'ingredienteWebApi',
    function ($rootScope, $scope, $state, $stateParams, tamanhoprodutosWebApi, grupoprodutosWebApi, ingredienteWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.getGrupoProdutos();
            vm.getIngredientes();
        };

        vm.defaults = function(){
            $scope.grupoprodutos = [];
            $scope.ingredientes = [];
        };

        vm.getGrupoProdutos = function(){
            grupoprodutosWebApi.getGrupoProdutosComTamanhos(
                function(data){
                    $scope.grupoprodutos = [ {id: undefined, nome: ''} ];
                    data.forEach(function(gp){ $scope.grupoprodutos.push(gp); });
                },
                function(erro){
                    $rootScope.showError(erro);
                }
            );
        };

        vm.getIngredientes = function(){
            ingredienteWebApi.getIngredientes(
                function(data){
                    $scope.ingredientes = data;
                },
                function(erro){
                    $rootScope.showError(erro);
                }
            );
        };

        vm.contructor();
    }
]);
