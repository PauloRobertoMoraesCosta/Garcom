angular.module('cadastros').controller('ingredientesCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'ingredienteWebApi',
    function ($rootScope, $scope, $state, $stateParams, ingredienteWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
        };

        vm.defaults = function(){
        };

        vm.contructor();
    }
]);