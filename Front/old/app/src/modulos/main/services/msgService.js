angular.module('mainApp').factory('msgService', ['$filter', 'langService', function ($filter, langService) {
    var vm = this;

    vm.msgs = {

        //Mensagens gerais
        gerais: {

            //Excluir
            excluir: function(){ var msgs = {
                'pt': 'Excluir',
                'en': 'Delete'
            }; return vm.getMsg(msgs); }
        }
    };

    vm.getMsg = function(msgs){ return msgs[langService.getLang().id]; };

    return vm.msgs;
}]);