angular.module('mainApp')
    .directive('ngData', ['$http', function($http) {
        return {
            restrict: 'A',
            link: function(scope, element, attrs) {
                var urlSvg = attrs.ngData;
                element.attr('data', urlSvg);
            }
        };
    }]);