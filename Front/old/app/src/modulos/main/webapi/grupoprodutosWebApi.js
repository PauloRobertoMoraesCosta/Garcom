angular.module('mainApp').factory('grupoprodutosWebApi', ['$http', '$q', '$rootScope', 'session', 'httpService',function ($http, $q, $rootScope, session, httpService) {
    var prefixo = 'grupoproduto/';

    function alterar(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.put(prefixo, objeto, sucessoCallback, erroCallback);
    };

    function cadastrar(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.post(prefixo, objeto, sucessoCallback, erroCallback);
    };

    function getGrupoProduto(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + id, sucessoCallback, erroCallback);
    };

    function getGrupoProdutos(sucessoCallback, erroCallback) {
        return httpService.get(prefixo, sucessoCallback, erroCallback);
    };

    function getGrupoProdutosComTamanhos(sucessoCallback, erroCallback) {
        return httpService.get(prefixo + 'ComTamanhos', sucessoCallback, erroCallback);
    };

    function validaExcluir(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + 'Valida/Exclusao/' + id, sucessoCallback, erroCallback);
    };

    function excluir(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.post(prefixo + 'Excluir/', objeto, sucessoCallback, erroCallback);
    };

    function desfazer(id, sucessoCallback, erroCallback){
        var objeto = { id: id, usuarioLogado: session.getUsuario().login };
        return httpService.put(prefixo + 'desfazer', objeto, sucessoCallback, erroCallback);
    };

    return {
        getGrupoProdutos: getGrupoProdutos,
        getGrupoProdutosComTamanhos: getGrupoProdutosComTamanhos,
        getGrupoProduto: getGrupoProduto,
        cadastrar: cadastrar,
        alterar: alterar,
        validaExcluir: validaExcluir,
        excluir: excluir,
        desfazer: desfazer
    };
}]);