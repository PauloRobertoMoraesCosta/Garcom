angular.module('cadastros').controller('grupoprodutosCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'grupoprodutosWebApi', 'tamanhoprodutosWebApi',
    function ($rootScope, $scope, $state, $stateParams, grupoprodutosWebApi, tamanhoprodutosWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.getTamanhosProduto();
        };

        vm.defaults = function(){
            $scope.tamanhosProduto = [];
        };

        vm.getTamanhosProduto = function(){
            tamanhoprodutosWebApi.getTamanhoProdutosAtivos(
                function(data){
                    $scope.tamanhosProduto = data;
                },
                function(erro){
                    $rootScope.showError(erro);
                }
            );
        };


        vm.contructor();
    }
]);
