angular.module('cadastros').controller('cadastrosCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'session', function ($rootScope, $scope, $state, $stateParams, session) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
    };

    vm.defaults = function(){
    };

    vm.contructor();
}]);