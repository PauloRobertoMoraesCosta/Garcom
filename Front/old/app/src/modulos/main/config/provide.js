angular.module('mainApp').config(function($provide) {
    $provide.decorator('$controller', function($rootScope, $delegate) {
        return function(constructor, locals, later, indent) {
            if (typeof constructor === 'string' && locals.$scope) {
                locals.$scope.controllerName =  constructor;
                $rootScope.scope = locals.$scope;
            }
            return $delegate(constructor, locals, later, indent);
        };
    });
});