angular.module('mainApp').factory('produtoWebApi', ['$http', '$q', '$rootScope', 'session', 'httpService',function ($http, $q, $rootScope, session, httpService) {
    var prefixo = 'produto/';

    function alterar(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.put(prefixo, objeto, sucessoCallback, erroCallback);
    };

    function cadastrar(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.post(prefixo, objeto, sucessoCallback, erroCallback);
    };

    function selecionar(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + id, sucessoCallback, erroCallback);
    };

    function listar(sucessoCallback, erroCallback) {
        return httpService.get(prefixo, sucessoCallback, erroCallback);
    };

    function validaExcluir(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + 'valida/exclusao/' + id, sucessoCallback, erroCallback);
    };

    function desfazer(id, sucessoCallback, erroCallback){
        var objeto = { id: id, usuarioLogado: session.getUsuario().login };
        return httpService.put(prefixo + 'desfazer', objeto, sucessoCallback, erroCallback);
    };

    function excluir(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.post(prefixo + 'excluir/', objeto, sucessoCallback, erroCallback);
    };

    function jaUtilizado(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + 'JaUtilizado/' + id, sucessoCallback, erroCallback);
    };

    return {
        selecionar: selecionar,
        listar: listar,
        cadastrar: cadastrar,
        alterar: alterar,
        validaExcluir: validaExcluir,
        desfazer: desfazer,
        excluir: excluir,
        jaUtilizado: jaUtilizado
    };
}]);