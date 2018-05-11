angular.module('mainApp')
    .directive('focus', ['$timeout', function($timeout) {
        return {
            restrict: 'A',
            link: function(scope, element, attrs) {
                var canFocus = scope.$eval(attrs.focus);
                var delay = attrs.delay !== undefined;
                if(canFocus || canFocus === undefined)
                {
                    if(!delay) element.focus();
                    else $timeout(function(){ element.focus(); }, 100);
                }
            }
        };
    }]);
