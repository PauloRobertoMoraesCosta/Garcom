angular.module('cadastros').controller('tamanhoprodutosCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'tamanhoprodutosWebApi', 'grupoprodutosWebApi',
    function ($rootScope, $scope, $state, $stateParams, tamanhoprodutosWebApi, grupoprodutosWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.getGrupoProdutos();
        };

        vm.defaults = function(){
            $scope.grupoprodutos = [];
        };

        vm.getGrupoProdutos = function(){
            grupoprodutosWebApi.getGrupoProdutos(
                function(data){
                    $scope.grupoprodutos = [ {id: undefined, nome: ''} ];
                    data.forEach(function(gp){ $scope.grupoprodutos.push(gp); });
                },
                function(erro){
                    $rootScope.showError(erro);
                }
            );
        };

        vm.contructor();
    }
]);