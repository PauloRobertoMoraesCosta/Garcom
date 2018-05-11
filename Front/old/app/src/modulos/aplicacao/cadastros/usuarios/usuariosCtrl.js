angular.module('cadastros').controller('usuariosCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'usuarioWebApi',
    function ($rootScope, $scope, $state, $stateParams, usuarioWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
        };

        vm.defaults = function(){
        };

        vm.contructor();
    }
]);