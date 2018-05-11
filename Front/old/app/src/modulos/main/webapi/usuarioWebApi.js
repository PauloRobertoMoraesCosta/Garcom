angular.module('mainApp').factory('usuarioWebApi', ['$http', '$q', '$rootScope', 'session', 'httpService',function ($http, $q, $rootScope, session, httpService) {
    function autenticar(login, manterLogin, sucessoCallback, erroCallback) {
        $rootScope.onRequest = true;
        httpService.autenticar(login).then(
            function(data){
                session.setStatus(data.data);
                if(manterLogin) session.setLoginInCookie(login.login, login.senha);
                else session.clearLoginCookie();

                getUsuario(login.login,
                    function(data, status){
                        session.setUsuario(data);
                        sucessoCallback(data);
                    },
                    function(erro, status){
                        $rootScope.showError(erro, status);
                    }
                );
            },
            function(data){
                $rootScope.onRequest = false;
                erroCallback(data.data, data.status);
            });
    };

    function getUsuario(loginOuId, sucessoCallback, erroCallback) {
        return httpService.get('usuarios/'+loginOuId, sucessoCallback, erroCallback);
    };

    function alterar(usuario, sucessoCallback, erroCallback) {
        usuario['usuarioLogado'] = session.getUsuario().login;
        $rootScope.onRequest = true;
        return httpService.put('usuarios', usuario, sucessoCallback, erroCallback);
    };

    function cadastrar(usuario, sucessoCallback, erroCallback) {
        usuario['usuarioLogado'] = session.getUsuario().login;
        $rootScope.onRequest = true;
        return httpService.post('usuarios', usuario, sucessoCallback, erroCallback);
    };

    function getUsuarios(sucessoCallback, erroCallback) {
        return httpService.get('usuarios', sucessoCallback, erroCallback);
    };

    return {
        autenticar: autenticar,
        getUsuario: getUsuario,
        getUsuarios: getUsuarios,
        cadastrar: cadastrar,
        alterar: alterar
    };
}]);