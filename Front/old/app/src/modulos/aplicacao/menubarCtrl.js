angular.module('principal').controller('menubarCtrl', ['$rootScope', '$scope', '$state', '$stateParams', 'itensmenu', 'session',
    function ($rootScope, $scope, $state, $stateParams, itensmenu, session) {
        var vm = this;

        vm.contructor = function(){
            vm.defaults();
            vm.setWatchers();
            vm.setItens();
        };

        vm.defaults = function(){
            vm.showMenu = false;
            vm.mobile = $rootScope.window.width <= $rootScope.mobileWidth;
            vm.widthAnterior = $rootScope.window.width;
            vm.itens = [];
            vm.firstItemGo = null;
            $scope.state = $state;
        };

        vm.setWatchers = function(){
            $rootScope.$watch('window.width', function(width){
                vm.mobile = $rootScope.window.width <= $rootScope.mobileWidth;

                if(!vm.mobile)
                    vm.showMenu = false;

                vm.widthAnterior = width;
            });

            $scope.$watch('state.current', function(){
                vm.itens.forEach(function(item){
                    var breaked = false;
                    if(item.submenu !== null && item.submenu.show && !item.state && item.submenu.itens.length > 0){
                        var show = false;
                        item.submenu.itens.forEach(function(subitem){
                            var splited = subitem.state.split('.');
                            var parentState = splited.slice(0, splited.length-1).join('.');
                            if($state.current.name.indexOf(parentState) >= 0){
                                show = true;
                                breaked = true;
                                return;
                            }
                        });
                        item.submenu.show = show;
                    }
                    if(breaked)
                        return;
                });
            });
        };

        vm.setItens = function(){
            vm.itens = itensmenu;

            vm.checkItensPermissao();
        };

        vm.checkItensPermissao = function(){
            var newList = [];
            vm.firstItemGo = null;
            vm.itens.forEach(function(item){
                var temPermissao = false;
                item.idPerfis.forEach(function(perfilId){
                    if(session.getUsuario().perfilId == perfilId){
                        temPermissao = true;
                        return;
                    }
                });
                if(temPermissao){
                    newList.push(item);
                    /*if(vm.firstItemGo == null){
                        if(item.state)
                            vm.firstItemGo = item;
                        else if(item.submenu && item.submenu.itens.length > 0){
                            item.submenu.show = true;
                            vm.firstItemGo = item.submenu.itens[0];
                        }
                    }*/
                }
            });
            vm.itens = newList;
            //if(vm.firstItemGo && !vm.firstItemGo.inativo) vm.goState(vm.firstItemGo);
        };

        vm.toggleMenu = function(){
            vm.showMenu = !vm.showMenu;
        };

        vm.toggleSubmenu = function(item){
            if(!vm.isAtualSubmenu(item.submenu))
                item.submenu.show = !item.submenu.show;
        };

        vm.goState = function(item){
            if(item != null){
                $state.go(item.state);
                if(vm.mobile) vm.showMenu = false;
            }
        };

        vm.isAtualSubmenu = function(submenu){
            var atualSubmenu = null;
            vm.itens.forEach(function(i){
                if($state.current.name == i.state){
                    atualSubmenu = i.submenu;
                    return;
                }
                else if(i.submenu && i.submenu.itens.length > 0){
                    i.submenu.itens.forEach(function(s){
                        if($state.current.name == s.state){
                            atualSubmenu = i.submenu;
                            return;
                        }
                    });
                }
            });

            return atualSubmenu != null && atualSubmenu == submenu;
        };

        vm.contructor();
    }
]);
