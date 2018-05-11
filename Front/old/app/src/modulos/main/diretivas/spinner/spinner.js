angular.module('mainApp').directive('spinner', ['$rootScope', '$window',
    function($rootScope, $window){
        return {
            restrict: 'EA',
            templateUrl: 'src/modulos/main/diretivas/spinner/spinner.html',
            scope:{
                width: '@',
                padding: '@'
            },
            link: function(scope, element, attrs){
                this.construtor = function(){
                    scope.css = {};
                    scope.whiteClass = attrs.white == 'true' ? true : false;
                    scope.buildCss();

                    scope.$watch('padding', function(){ scope.buildCss(); });
                };

                scope.buildCss = function(){
                    scope.css = {
                        'height': ($rootScope.heightBody-100) + 'px'
                    };

                    if(scope.width && typeof scope.width === 'number')
                        scope.css['width'] = scope.width + 'px';

                    if(scope.padding && typeof scope.padding === 'string')
                        scope.css['padding'] = scope.padding;
                };

                angular.element($window).bind('resize', function(){
                    scope.buildCss();
                });

                this.construtor();
            }
        };
    }
]);