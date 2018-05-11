angular.module('mainApp').factory('langService', ['$filter', function ($filter) {
    var vm = this;

    vm.langList = [
        { id: 'pt', nome: 'Português' }
    ];
    vm.lang = vm.langList[0];

    return {
        getLang: function() { return vm.lang; },
        setLang: function(lang){ vm.lang = lang },
        setLangById: function(langId){
            vm.langList.forEach(function(l){
                if(l.id === langId){
                    vm.lang = l;
                    return;
                }
            });
        }
    };
}]);