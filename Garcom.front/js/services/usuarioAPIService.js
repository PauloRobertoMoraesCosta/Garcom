/* Usando factory

angular.module("garcomApp").factory("logarUsuarioApi", function ($http) {
    var _logarUsuario = function (login, senha) {
        return $http.get("http://localhost/usuario/logar/" + login + "/" + senha);
    };

    return {
        logarUsuario: _logarUsuario
    };
});
*/

// usando service
angular.module("garcomApp").service("logarUsuarioApi", function ($http, config) {
    this.logarUsuario = function(login, senha) {
        return $http.get(config.baseUrl + "/usuario/logar/" + login + "/" + senha);
    };
});