angular.module('mainApp').directive('breadcrumb', [
    function($rootScope){
        return {
            restrict: 'EA',
            templateUrl: 'src/modulos/main/diretivas/breadcrumb/breadcrumb.html',
            scope:{
                ngModel: '='
            },
            link: function(scope, element, attrs){

            }
        };
    }
]);