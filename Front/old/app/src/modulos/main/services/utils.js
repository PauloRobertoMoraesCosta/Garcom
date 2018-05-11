angular.module('mainApp').factory('utils', ['$filter', function ($filter) {
    var vm = this;

    vm.possuiCaracterMaiusculo = function (texto) {
        if(typeof texto == 'string')
            for(var i = 0; i < texto.length; i++){
                if(isNaN(parseInt(texto.charAt(i))) && texto.charAt(i).toUpperCase() == texto.charAt(i)) return true;
            }
        return false;
    };
    vm.possuiCaracterMinusculo = function (texto) {
        if(typeof texto == 'string')
            for(var i = 0; i < texto.length; i++){
                if(isNaN(parseInt(texto.charAt(i))) && texto.charAt(i).toLowerCase() == texto.charAt(i)) return true;
            }
        return false;
    };
    vm.possuiNumero = function (texto) {
        if(typeof texto == 'string')
            for(var i = 0; i < texto.length; i++){
                if(!isNaN(parseInt(texto.charAt(i)))) return true;
            }
        return false;
    };

    return {
        possuiCaracterMaiusculo: vm.possuiCaracterMaiusculo,
        possuiCaracterMinusculo: vm.possuiCaracterMinusculo,
        possuiNumero: vm.possuiNumero
    };
}]);