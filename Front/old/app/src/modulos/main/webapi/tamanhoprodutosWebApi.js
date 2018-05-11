angular.module('mainApp').factory('tamanhoprodutosWebApi', ['$http', '$q', '$rootScope', 'session', 'httpService',function ($http, $q, $rootScope, session, httpService) {
    var prefixo = 'tamanho/';

    function alterar(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.put(prefixo, objeto, sucessoCallback, erroCallback);
    };

    function cadastrar(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.post(prefixo, objeto, sucessoCallback, erroCallback);
    };

    function getTamanhoProduto(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + id, sucessoCallback, erroCallback);
    };

    function getTamanhoProdutos(sucessoCallback, erroCallback) {
        return httpService.get(prefixo, sucessoCallback, erroCallback);
    };

    function getTamanhoProdutosAtivos(sucessoCallback, erroCallback) {
        return httpService.get(prefixo+'/ativos', sucessoCallback, erroCallback);
    };

    function validaExcluir(id, sucessoCallback, erroCallback) {
        return httpService.get(prefixo + 'valida/exclusao/' + id, sucessoCallback, erroCallback);
    };

    function excluir(objeto, sucessoCallback, erroCallback) {
        objeto['usuarioLogado'] = session.getUsuario().login;
        return httpService.post(prefixo + 'excluir/', objeto, sucessoCallback, erroCallback);
    };

    function validaOrdem(objeto, sucessoCallback, erroCallback) {
        return httpService.post(prefixo + 'valida/ordenacao', objeto, sucessoCallback, erroCallback);
    };

    function nomeProdutosVinculados(id, grupoProdutoId, sucessoCallback, erroCallback){
        var objeto = { id: id, grupoProdutoId: grupoProdutoId };
        return httpService.post(prefixo + 'NomeProdutosVinculados', objeto, sucessoCallback, erroCallback);
    };

    function desfazer(id, sucessoCallback, erroCallback){
        var objeto = { id: id, usuarioLogado: session.getUsuario().login };
        return httpService.put(prefixo + 'desfazer', objeto, sucessoCallback, erroCallback);
    };

    return {
        getTamanhoProdutos: getTamanhoProdutos,
        getTamanhoProduto: getTamanhoProduto,
        getTamanhoProdutosAtivos: getTamanhoProdutosAtivos,
        cadastrar: cadastrar,
        alterar: alterar,
        validaExcluir: validaExcluir,
        excluir: excluir,
        validaOrdem : validaOrdem,
        nomeProdutosVinculados: nomeProdutosVinculados,
        desfazer: desfazer
    };
}]);
