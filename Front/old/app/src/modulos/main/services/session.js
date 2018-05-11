angular.module('mainApp').factory('session', ['$state', '$cookies', '$injector', function ($state, $cookies, $injector) {
    var vm = this;

    vm.status = {};
    vm.usuario = {};
    vm.tempoManterLogin = 12;//Em horas

    vm.getTempoManterLogin = function () {
        return vm.tempoManterLogin;
    };

    vm.getSession = function () {
        return vm.status;
    };

    vm.setStatus = function (data) {
        vm.status = {
            login: true,
            token_type: data.token_type,
            access_token: data.access_token,
            config: {
                headers: { 'Authorization': data.token_type + ' ' + data.access_token }
            }
        }
    };

    vm.setUsuario = function (usuario) {
        vm.usuario = usuario;
    };

    vm.getUsuario = function () {
        return vm.usuario;
    };

    vm.getLoginCookie = function () {
        var loginCookie = $cookies.getObject('garcom_login');
        if(loginCookie){
            var diff = Math.abs(new Date() - new Date(loginCookie.dateTime));
            var minutos = Math.floor((diff/1000)/60);
            if(minutos > 720){
                vm.clearLoginCookie();
                return null;
            }
            else
                return loginCookie;
        }

        return null;
    };

    vm.clearLoginCookie = function () {
        if($cookies.getObject('garcom_login'))
            $cookies.remove('garcom_login');
    };

    vm.setLoginInCookie = function (usuario, senha) {
        $cookies.putObject('garcom_login', { usuario: usuario, senha: senha, dateTime: new Date() });
    };

    vm.logout = function(forceLogout){
        var ngConfirm = $injector.get('ngConfirm');
        if(!forceLogout && ngConfirm)
            ngConfirm('Deseja sair do aplicativo?<br/>É possível que as alterações feitas não sejam salvas.', function(){
                vm.setUsuario({});
                $state.go('login');
            });
        else{
            vm.setUsuario({});
            $state.go('login');
        }
    };

    return {
        getSession: vm.getSession,
        setSession: vm.setSession,
        setStatus: vm.setStatus,
        setUsuario: vm.setUsuario,
        getUsuario: vm.getUsuario,
        getTempoManterLogin: vm.getTempoManterLogin,
        getLoginCookie: vm.getLoginCookie,
        setLoginInCookie: vm.setLoginInCookie,
        clearLoginCookie: vm.clearLoginCookie,
        logout: vm.logout
    };
}]);