angular.module('garcom').controller('relacaoMesasCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'session', function ($rootScope, $scope, $state, $stateParams, session) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
        vm.setWatchers();
        vm.configBreadcrumb();

        vm.getMesas();

        $scope.$on('destroy', function(){
            if(vm.watchItemSelecionado) vm.watchItemSelecionado();
        });
    };

    vm.defaults = function(){
        vm.itensMenu = [
            { id:1, content: 'Todas as mesas' },
            { id:2, content: '<img width="24" height="24" src="src/img/icones/mesa_fechada_30.png"/> Mesas fechadas' },
            { id:3, content: '<i class="material-icons" style="font-size: 30px">person</i> Minhas mesas' }
        ];

        vm.itemSelecionado = vm.itensMenu[0];
    };

    vm.setWatchers = function(){
        vm.watchItemSelecionado = $scope.$watch('ctrl.itemSelecionado', function(){

        });
    };

    vm.configBreadcrumb = function(){
        $scope.breadcrumb = [
            { nome: 'Início', state: 'principal.inicio' },
            { nome: 'Garçom' }
        ];
    };

    vm.getMesas = function(){

    };

    vm.contructor();
}]);
