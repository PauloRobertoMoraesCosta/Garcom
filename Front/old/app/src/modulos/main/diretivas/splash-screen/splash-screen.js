angular.module('mainApp').directive('splashScreen', ['$rootScope', '$window',
    function($rootScope, $window){
        return {
            restrict: 'E',
            templateUrl: 'src/modulos/main/diretivas/splash-screen/splash-screen.html',
            scope:{
                show: '='
            },
            link: function(scope, element, attrs){
                this.construtor = function(){
                };

                this.construtor();
            }
        };
    }
]);
