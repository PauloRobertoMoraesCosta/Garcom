angular.module('mainApp').factory('ingredienteWebApi', ['$http', '$q', '$rootScope', 'session', 'httpService',function ($http, $q, $rootScope, session, httpService) {
    var prefixo = 'ingredientes/';

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


    function validaExcluir(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + 'validaExcluir/' + id, sucessoCallback, erroCallback);
    };

    function getIngrediente(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + id, sucessoCallback, erroCallback);
    };

    function getIngredientes(sucessoCallback, erroCallback) {
        return httpService.get(prefixo, sucessoCallback, erroCallback);
    };

    return {
        getIngredientes: getIngredientes,
        getIngrediente: getIngrediente,
        validaExcluir: validaExcluir,
        cadastrar: cadastrar,
        excluir: excluir,
        alterar: alterar,
        desfazer: desfazer
    };
}]);