/*!
 * ngConfirm v1.0.2
 * Copyright 2016 Eric Ferreira
 * Contato: ericferreira1992@gmail.com
 */

(function() {
    'use strict';

    angular.module('ngConfirm', [], function(){});
    angular.module('ngConfirm').factory('ngConfirmFactory', function ($rootScope, $timeout, $compile) {
        var getTemplate = function(){
            return ''+
                '<div class="ng-confirm-geral" ng-class="{\'ng-confirm-in\': modalShow, \'ng-confirm-out\': !modalShow}" id="ng-confirm-1">'+
                    '<div class="ng-confirm-area">'+
                        '<div class="ng-confirm-container">'+
                            '<header class="primary" ng-bind-html="titulo"></header>'+
                            '<section ng-bind-html="msgConfirmacao"></section>'+
                            '<div>'+
                                '<button class="btn btn-primary" ng-click="okBtn()" id="ng-confirm-ok" ng-bind-html="okBtnLabel"></button>'+
                                '<button class="btn btn-none" ng-click="cancelarBtn()" id="ng-confirm-cancel" ng-bind-html="cancelarBtnLabel"></button>'+
                            '</div>'+
                        '</div>'+
                    '</div>'+
                '</div>';
        };

        var showConfirmacao = function(scope){
            var modal = getTemplate();
            angular.element(document.body).prepend(modal);

            $compile(angular.element(document.querySelector('#ng-confirm-1')))(scope);

            if(!scope instanceof $rootScope.constructor) scope.$apply();

            $timeout(function(){
                document.querySelector('#ng-confirm-ok').focus();
            },100);
        };

        var closeConfirmacao = function (scope) {
            scope.modalShow = false;
            $timeout(function () {
                angular.element(document.querySelector('#ng-confirm-1')).remove();
                if(!scope instanceof $rootScope.constructor) scope.$apply();
            }, 200);
        };

        return {
            startFromService: function (msg, acaoSim, acaoNao) {
                var scope = $rootScope.$new();

                /* ------ Funcionalidades ------ */
                scope.okBtn = function () {
                    acaoSim();
                    closeConfirmacao(scope);
                };
                scope.cancelarBtn = function () {
                    if(typeof acaoNao == 'function') acaoNao();
                    closeConfirmacao(scope);
                };

                scope.inicializa = function () {
                    scope.modalShow = true;

                    if(typeof msg == 'object'){
                        scope.titulo = msg.title != undefined ? msg.title : 'Confirmação';
                        scope.msgConfirmacao = msg.msg != undefined ? msg.msg : 'Tem certeza que deseja prosseguir?';
                        scope.okBtnLabel = msg.okBtn != undefined ? msg.okBtn : 'Sim';
                        scope.cancelarBtnLabel = msg.cancelBtn != undefined ? msg.cancelBtn : 'Não';
                    }
                    else{
                        scope.msgConfirmacao = msg;
                        scope.titulo = 'Confirmação';
                        scope.okBtnLabel = 'Sim';
                        scope.cancelarBtnLabel = 'Não';
                    }

                    showConfirmacao(scope);
                };

                scope.inicializa();
            },

            startFromDirective: function (scope, element, attrs) {
                element.bind('click', function (event) {
                    event.stopImmediatePropagation();
                    scope.inicializa();
                });

                /* ------ Funcionalidades ------ */
                scope.okBtn = function () {
                    scope.ngClick();
                    closeConfirmacao(scope);
                };
                scope.cancelarBtn = function () {
                    closeConfirmacao(scope);
                };

                scope.inicializa = function () {
                    scope.modalShow = true;
                    scope.titulo = attrs.title != undefined ? attrs.title : 'Confirmação';
                    scope.msgConfirmacao = attrs.ngConfirm != undefined ? attrs.ngConfirm : 'Tem certeza que deseja prosseguir?';
                    scope.okBtnLabel = attrs.okBtn != undefined ? attrs.okBtn : 'Sim';
                    scope.cancelarBtnLabel = attrs.cancelBtn != undefined ? attrs.cancelBtn : 'Não';

                    showConfirmacao(scope);
                };
            }
        };
    });
    angular.module('ngConfirm').directive('ngConfirm', function (ngConfirmFactory) {
        return {
            restrict: 'A',
            scope: {
                ngClick: '&ngClick'
            },
            link: ngConfirmFactory.startFromDirective
        }
    });
    angular.module('ngConfirm').service('ngConfirm', function(ngConfirmFactory){
        return ngConfirmFactory.startFromService
    });
})();