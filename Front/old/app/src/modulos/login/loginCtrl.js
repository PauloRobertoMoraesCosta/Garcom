angular.module('login').controller('loginCtrl', ['$rootScope', '$scope', '$state', '$sce', '$location', 'ngToast', 'session', 'usuarioWebApi',
    function ($rootScope, $scope, $state, $sce, $location, ngToast, session, usuarioWebApi) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
        };

        vm.defaults = function(){
            var loginCookie = session.getLoginCookie();

            if(loginCookie !== null){
                vm.usuario = loginCookie.usuario;
                vm.senha = loginCookie.senha;
                vm.manterLogin = true;
            }
            else{
                vm.usuario = 'usuario';
                vm.senha = 'Usuario1';
                vm.manterLogin = false;
            }

            vm.tempoManterLogin = session.getTempoManterLogin();
            $scope.loading = false;
        };
        vm.login = function(){
            if(vm.validate()){
                $scope.loading = true;
                ngToast.dismiss();
                usuarioWebApi.autenticar({login: vm.usuario, senha: vm.senha}, vm.manterLogin,
                    function(data){
                        $state.go('principal.inicio');
                    },
                    function(data, status){
                        $scope.loading = false;
                        $rootScope.showError(data, status);
                    }
                );
            }
        };

        vm.validate = function(){
            if(!vm.usuario) return false;
            if(!vm.senha) return false;

            return true;
        };

        vm.contructor();
    }
]);
