angular.module("garcomApp").controller("usuarioController", function ($scope, logarUsuarioApi) {
    $scope.usuarios = [];

    $scope.logarUsuario = function (usuario) {
        logarUsuarioApi.logarUsuario(usuario.login, usuario.senha).success(function (data) {
            $scope.usuarios = data;
            console.log(data);
        });
    };
});
