angular.module('cadastros').controller('mesasCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'mesaWebApi',
    function ($rootScope, $scope, $state, $stateParams, mesaWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
        };

        vm.defaults = function(){
        };

        vm.contructor();
    }
]);