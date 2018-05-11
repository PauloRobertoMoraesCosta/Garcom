angular.module('principal').controller('inicioCtrl', ['$rootScope', '$scope', '$state', '$stateParams', function ($rootScope, $scope, $state, $stateParams) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
        vm.configBreadcrumb();
    };

    vm.configBreadcrumb = function(){
        $scope.breadcrumb = [
            { nome: 'Início', state: 'principal.inicio' },
            { nome: 'Bem vindo' },
        ];
    };

    vm.defaults = function(){
    };

    vm.contructor();
}]);