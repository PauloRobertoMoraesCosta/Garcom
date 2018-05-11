/*!
 * ngSelectPicker v1.0.2
 * Copyright 2016 Eric Ferreira
 * Contato: ericferreira1992@gmail.com
 */

(function() {
    'use strict';

    angular.module('ngSelectPicker', [], function(){});
    angular.module('ngSelectPicker').directive('selectpicker', function ($location, $timeout, $compile) {
        return {
            restrict: 'EA',

            template: '' +
                '<div class="selectpicker">'+
                    '<div class="selectpicker-input" ng-click="toggleCombo()">'+
                        '<div ng-if="todosChecked">Todos</div>'+
                        '<div ng-if="!todosChecked">{{ngModel|joinSelectPicker}}</div>'+
                        '<div><i class="fa fa-caret-down"></i></div>'+
                    '</div>'+
                    '<div class="selectpicker-combo" ng-class="{\'selectpicker-combo-in\': showCombo == 1, \'selectpicker-combo-out\': showCombo == 0, \'top\': position == \'top\'}">'+
                        '<div class="selectpicker-combo-search" ng-style="!filtro && {\'display\': \'none\'}">'+
                            '<form novalidate name="frm">' +
                                '<input type="text" class="form-control" autofocus ng-model="filtroInput" />'+
                            '</form>'+
                        '</div>'+
                        '<div class="selectpicker-combo-itens">'+
                            '<div class="selectpicker-combo-item" ng-click="toggleTodos()">'+
                                '<div><strong>{{todosNome}}</strong></div>'+
                                '<div><i class="fa" ng-class="{\'fa-check\': todosChecked}"></i></div>'+
                            '</div>'+
                            '<div ng-transclude style="display: none"></div>'+
                        '</div>'+
                    '</div>'+
                '</div>',

            transclude: true,
            require: '^ngModel',
            scope: {
                ngModel:'=',
                options:'=',
                position:'@'
            },
            link: function(scope, element, attrs){
                /* --- LISTNERS --- */
                angular.element(document).bind('click', function(event){
                    //Verifica se clicou num elemento dentro do combo
                    var selectpicker = angular.element(element.children()[0])[0];
                    if (isDescendant(selectpicker, event.target)) {
                        scope.$apply(function(){
                            scope.clicouDentro = true;
                        });
                    }
                    else{
                        scope.$apply(function(){
                            scope.clicouDentro = false;
                            scope.showCombo = 0;
                        });
                    }
                });
                angular.element(document).bind('keypress', function(event){
                    if(scope.clicouDentro && scope.showCombo == 1){
                        element.find('input')[0].focus();
                    }
                });
                angular.element(document).bind('keyup', function(event){
                    if(scope.clicouDentro){
                        if(event.keyCode == 27 && scope.showCombo == 1)
                            scope.$apply(function(){
                                scope.showCombo = 0;
                            });
                        else if(event.keyCode == 46 && scope.showCombo != 1)
                            scope.$apply(function(){
                                scope.ngModel = [];
                                scope.todosChecked = false;
                            });
                        if(event.keyCode == 8)
                            element.find('input')[0].focus();
                    }
                });

                scope.inicializa = function(){
                    scope.ngModel = !scope.ngModel ? [] : scope.ngModel;
                    scope.filtroInput = '';
                    scope.todosChecked = false;
                    scope.showCombo = -1;
                    scope.clicouDentro = true;
                    scope.position = !scope.position ? '' : scope.position;

                    console.log(scope.position);

                    if(!scope.options)
                        scope.itens = [];
                    else
                        scope.$watch('options', function(){
                            scope.defineItens();
                        });


                    scope.$watch('showCombo', function() {
                        if(scope.showCombo == 1){
                            element.find('input')[0].focus();
                        }
                    });
                    scope.$watch('ngModel', function(){
                        if(scope.itens.length > 0 && scope.ngModel.length == scope.itens.length) scope.todosChecked = true;
                        else scope.todosChecked = false;
                    });

                    if(!scope.options) {
                        $timeout(function(){
                            var optionsDiv = element[0].children[0].children[1].children[1].children[1];
                            var optionsArray = optionsDiv.children;

                            angular.forEach(optionsArray, function(opt){
                                var el = angular.element(opt)[0];
                                scope.itens.push({
                                    valor: el.getAttribute('value'),
                                    texto: el.innerHTML
                                });
                            });

                            angular.element(optionsDiv)[0].remove();
                            optionsDiv = element[0].children[0].children[1].children[1];

                            var repeatElement = angular.element(
                                '<div ng-repeat="i in itens | filter: filtroInput" class="selectpicker-combo-item" ng-class="{\'disabled\': todosChecked}" ng-click="toggleCheck(i)">'+
                                    '<div>{{i.texto}}</div>'+
                                    '<div><i class="fa" ng-class="{\'fa-check\': todosChecked || verificaSeCheckado(i.valor)}"></i></div>'+
                                '</div>'
                            );
                            angular.element(optionsDiv).append(repeatElement);
                            $compile(repeatElement)(scope);

                            if(attrs.selectallinit == '' || attrs.selectallinit == 'true') scope.toggleTodos(true);
                            if(scope.itens.length > 0 && scope.ngModel.length == scope.itens.length) scope.todosChecked = true;
                            else scope.todosChecked = false;
                        }, 1);
                    }
                    else{
                        $timeout(function(){
                            var optionsDiv = element[0].children[0].children[1].children[1];//.children[1];
                            angular.element(optionsDiv.children[1])[0].remove();
                            var repeatElement = angular.element(
                                '<div ng-repeat="i in itens | filter: filtroInput" class="selectpicker-combo-item" ng-class="{\'disabled\': todosChecked}" ng-click="toggleCheck(i)">'+
                                    '<div>{{i.texto}}</div>'+
                                    '<div><i class="fa" ng-class="{\'fa-check\': todosChecked || verificaSeCheckado(i.valor)}"></i></div>'+
                                '</div>'
                            );
                            angular.element(optionsDiv).append(repeatElement);
                            $compile(repeatElement)(scope);
                        }, 1);
                    }

                    if(attrs.filtro == undefined || attrs.filtro == 'true')
                        scope.filtro = true;
                    else
                        scope.filtro = false;

                    if(attrs.todosNome != undefined && attrs.todosNome != '')
                        scope.todosNome = attrs.todosNome;
                    else
                        scope.todosNome = 'Todos';

                };

                scope.defineItens = function(){
                    scope.itens = [];
                    scope.options.forEach(function(opt){
                        var valorProp = attrs.valorProp ? attrs.valorProp : 'id';
                        var textoProp = attrs.textoProp ? attrs.textoProp : 'nome';
                        scope.itens.push({
                            valor: opt[valorProp],
                            texto: opt[textoProp]
                        });
                    });

                    if(scope.itens.length == 0) scope.ngModel = [];
                    else if(attrs.selectallinit == '' || attrs.selectallinit == 'true') scope.toggleTodos(true);
                };

                scope.toggleCombo = function(){
                    scope.showCombo = scope.showCombo <= 0 ? 1 : 0;
                };

                scope.verificaSeCheckado = function(valor){
                    for(var i = 0; i < scope.ngModel.length; i++){
                        if(scope.ngModel[i].valor == valor)
                            return true;
                    }
                    return false;
                };

                scope.toggleCheck = function(item){
                    if(!scope.todosChecked){
                        if(scope.verificaSeCheckado(item.valor)){
                            var newItens = [];
                            scope.ngModel.forEach(function(i){
                                if(i.valor != item.valor)
                                    newItens.push(i);
                            });

                            scope.ngModel = newItens;
                        }
                        else {
                            var newItens = [];
                            newItens.push(item);
                            scope.ngModel.forEach(function(i){
                                newItens.push(i);
                            });

                            scope.ngModel = newItens;
                        }
                    }
                };

                scope.toggleTodos = function(bool){
                    if(typeof bool != 'undefined')
                        scope.todosChecked = bool;
                    else
                        scope.todosChecked = !scope.todosChecked;

                    if(scope.todosChecked){
                        var newList = [];
                        scope.itens.forEach(function(i){
                            newList.push(i);
                        });
                        scope.ngModel = newList;
                    }
                    else
                        scope.ngModel = [];
                };

                scope.inicializa();

                var isDescendant = function(parent, child) {
                    var node = angular.element(child.parentNode)[0];
                    while (node != null) {
                        if (node == parent)
                            return true;
                        if(node.parentNode) node = angular.element(node.parentNode)[0];
                        else return false;
                    }
                    return false;
                }
            }
        }
    })
    .filter('joinSelectPicker', function () {
        return function (input)
        {
            if(input != undefined && input != null)
            {
                var listaJoined = '';
                for(var i = 0; i < input.length; i++){
                    listaJoined += (listaJoined == '' ? '' : ', ') + input[i].texto;
                };

                return listaJoined;
            }
            return '';
        }
    });
})();