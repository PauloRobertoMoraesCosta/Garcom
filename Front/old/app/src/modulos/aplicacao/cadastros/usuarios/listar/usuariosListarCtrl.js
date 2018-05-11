angular.module('cadastros').controller('usuariosListarCtrl', ['$rootScope', '$scope', '$state', '$sce', '$filter', '$stateParams', 'usuarioWebApi', 'ngConfirm', 'ngToast', 'perfis',
function ($rootScope, $scope, $state, $sce, $filter, $stateParams, usuarioWebApi, ngConfirm, ngToast, perfis) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
        vm.configBreadcrumb();
        vm.getUsuarios();
    };

    vm.configBreadcrumb = function(){
        $scope.breadcrumb = [
            { nome: 'Início', state: 'principal.inicio' },
            { nome: 'Usuarios' }
        ];
    };

    vm.resetTableFilters = function(){
        $scope.filtro = {
            nome: undefined,
            email: undefined,
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

        if(vm.usuarios.length > 0){
            vm.usuarios = $filter('orderBy')(vm.usuarios, ($scope.ordenacao.asc ? '' : '-') + $scope.ordenacao.coluna);
        }
    };

    vm.defaults = function(){
        vm.usuarios = [];

        $scope.perfis = [
            {id: undefined, descricao: ''}
        ];
        perfis.forEach(function(p){ $scope.perfis.push(p); });

        $scope.filtro = {
            nome: undefined,
            login: undefined,
            perfilId: undefined,
            ativo: undefined
        };

        $scope.ordenacao = {
            coluna: 'nome',
            asc: true
        };
    };

    vm.goManutencao = function(id){
        if(id) $state.go('principal.cadastros.usuarios.manutencao', {id: id});
        else $state.go('principal.cadastros.usuarios.manutencao');
    };

    vm.getUsuarios = function(){
        usuarioWebApi.getUsuarios(
            function(data){
                vm.usuarios = data;
                vm.ordenar();
            },
            function(erro){
                $rootScope.showError(erro);
            }
        );
    };

    vm.contructor();
}]);