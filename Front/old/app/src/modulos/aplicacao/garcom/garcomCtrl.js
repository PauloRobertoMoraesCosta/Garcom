angular.module('garcom').controller('garcomCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'session', function ($rootScope, $scope, $state, $stateParams, session) {
    var vm = this;

    vm.contructor = function(){
        vm.defaults();
    };

    vm.logout = session.logout;

    vm.defaults = function(){
    };

    vm.contructor();
}]);