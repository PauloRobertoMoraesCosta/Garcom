angular.module('mainApp').factory('mesaWebApi', ['$http', '$q', '$rootScope', 'session', 'httpService',function ($http, $q, $rootScope, session, httpService) {
    var prefixo = 'mesa/';

    function alterar(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.put(prefixo, objeto, sucessoCallback, erroCallback);
    };

    function excluir(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.post(prefixo + 'excluir', objeto, sucessoCallback, erroCallback);
    };

    function cadastrar(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.post(prefixo, objeto, sucessoCallback, erroCallback);
    };

    function desfazer(id, sucessoCallback, erroCallback){
        var objeto = { id: id, usuarioLogado: session.getUsuario().login };
        return httpService.put(prefixo + 'desfazer', objeto, sucessoCallback, erroCallback);
    };
    
    function getMesa(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + id, sucessoCallback, erroCallback);
    };

    function getMesas(sucessoCallback, erroCallback) {
        return httpService.get(prefixo, sucessoCallback, erroCallback);
    };

    return {
        getMesas: getMesas,
        getMesa: getMesa,        
        cadastrar: cadastrar,
        excluir: excluir,
        alterar: alterar,
        desfazer: desfazer
    };
}]);