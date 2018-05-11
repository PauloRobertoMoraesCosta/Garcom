angular.module('mainApp').config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider)
{
    $urlRouterProvider.otherwise('/login');

    $stateProvider

        //Garçom (Login)
        .state('login', {
            url: '/login',
            controller: 'loginCtrl',
            controllerAs: 'ctrl',
            templateUrl: 'src/modulos/login/login.html',
            resolve: {
                load: ['$rootScope', '$ocLazyLoad', function($rootScope, $ocLazyLoad) {
                    $rootScope.routeLoading = true;
                    return $ocLazyLoad.load(['inicial', 'login'], {serie: true}).then(function(){ $rootScope.routeLoading = false; });
                }]
            }
        })

        //Garçom (Principal - ABSTRATO)
        .state('principal', {
            abstract: true,
            controller: 'principalCtrl',
            controllerAs: 'ctrl',
            templateUrl: 'src/modulos/aplicacao/principal.html',
            resolve: {
                load: ['$rootScope', '$ocLazyLoad', function($rootScope, $ocLazyLoad) {
                    $rootScope.routeLoading = true;
                    return $ocLazyLoad.load('principal').then(function(){ $rootScope.routeLoading = false; });
                }]
            }
        })
            //Início
            .state('principal.inicio', {
                url: '/',
                parent: 'principal',
                controller: 'inicioCtrl',
                controllerAs: 'ctrl',
                templateUrl: 'src/modulos/aplicacao/inicio/inicio.html',
            resolve: {
                load: ['$rootScope', '$ocLazyLoad', function($rootScope, $ocLazyLoad) {
                    $rootScope.routeLoading = true;
                    return $ocLazyLoad.load('inicio').then(function(){ $rootScope.routeLoading = false; });
                }]
            }
            })

            //Cadastros (ABSTRATO)
            .state('principal.cadastros', {
                abstract: true,
                controller: 'cadastrosCtrl',
                controllerAs: 'ctrl',
                templateUrl: 'src/modulos/aplicacao/cadastros/cadastros.html',
                resolve: {
                    load: ['$rootScope', '$ocLazyLoad', function($rootScope, $ocLazyLoad) {
                        $rootScope.routeLoading = true;
                        return $ocLazyLoad.load('cadastros');
                    }]
                }
            })
                //Usuarios (ABSTRATO)
                .state('principal.cadastros.usuarios', {
                    abstract: true,
                    controller: 'usuariosCtrl',
                    controllerAs: 'ctrl',
                    templateUrl: 'src/modulos/aplicacao/cadastros/usuarios/usuarios.html',
                    resolve: {
                        load: ['$rootScope', '$ocLazyLoad', '$timeout', function($rootScope, $ocLazyLoad, $timeout) {
                            $rootScope.routeLoading = true;
                            return $ocLazyLoad.load('usuarios').then(function(){ $rootScope.routeLoading = false; });
                        }]
                    }
                })
                    //Listar
                    .state('principal.cadastros.usuarios.listar', {
                        url: '/cadastros/usuarios',
                        templateUrl: 'src/modulos/aplicacao/cadastros/usuarios/listar/listar.html',
                        controller: 'usuariosListarCtrl',
                        controllerAs: 'ctrl'
                    })
                    //Manutencao
                    .state('principal.cadastros.usuarios.manutencao', {
                        url: '/cadastros/usuarios/:id?',
                        templateUrl: 'src/modulos/aplicacao/cadastros/usuarios/manutencao/manutencao.html',
                        controller: 'usuariosManutencaoCtrl',
                        controllerAs: 'ctrl'
                    })

                //Ingredientes (ABSTRATO)
                .state('principal.cadastros.ingredientes', {
                    abstract: true,
                    controller: 'ingredientesCtrl',
                    controllerAs: 'ctrl',
                    templateUrl: 'src/modulos/aplicacao/cadastros/ingredientes/ingredientes.html',
                    resolve: {
                        load: ['$rootScope', '$ocLazyLoad', '$timeout', function($rootScope, $ocLazyLoad, $timeout) {
                            $rootScope.routeLoading = true;
                            return $ocLazyLoad.load('ingredientes').then(function(){ $rootScope.routeLoading = false; });
                        }]
                    }
                })
                    //Listar
                    .state('principal.cadastros.ingredientes.listar', {
                        url: '/cadastros/ingredientes',
                        templateUrl: 'src/modulos/aplicacao/cadastros/ingredientes/listar/listar.html',
                        controller: 'ingredientesListarCtrl',
                        controllerAs: 'ctrl'
                    })
                    //Manutencao
                    .state('principal.cadastros.ingredientes.manutencao', {
                        url: '/cadastros/ingredientes/:id?',
                        templateUrl: 'src/modulos/aplicacao/cadastros/ingredientes/manutencao/manutencao.html',
                        controller: 'ingredientesManutencaoCtrl',
                        controllerAs: 'ctrl'
                    })

               //Grupo Produtos (ABSTRATO)
                .state('principal.cadastros.grupoprodutos', {
                    abstract: true,
                    controller: 'grupoprodutosCtrl',
                    controllerAs: 'ctrl',
                    templateUrl: 'src/modulos/aplicacao/cadastros/grupoprodutos/grupoprodutos.html',
                    resolve: {
                        load: ['$rootScope', '$ocLazyLoad', '$timeout', function($rootScope, $ocLazyLoad, $timeout) {
                            $rootScope.routeLoading = true;
                            return $ocLazyLoad.load('grupoprodutos').then(function(){ $rootScope.routeLoading = false; });
                        }]
                    }
                })
                    //Listar
                    .state('principal.cadastros.grupoprodutos.listar', {
                        url: '/cadastros/grupoprodutos',
                        templateUrl: 'src/modulos/aplicacao/cadastros/grupoprodutos/listar/listar.html',
                        controller: 'grupoprodutosListarCtrl',
                        controllerAs: 'ctrl'
                    })
                    //Manutencao
                    .state('principal.cadastros.grupoprodutos.manutencao', {
                        url: '/cadastros/grupoprodutos/:id?',
                        templateUrl: 'src/modulos/aplicacao/cadastros/grupoprodutos/manutencao/manutencao.html',
                        controller: 'grupoprodutosManutencaoCtrl',
                        controllerAs: 'ctrl'
                    })

                //Tamanho Produtos (ABSTRATO)
                .state('principal.cadastros.tamanhoprodutos', {
                    abstract: true,
                    controller: 'tamanhoprodutosCtrl',
                    controllerAs: 'ctrl',
                    templateUrl: 'src/modulos/aplicacao/cadastros/tamanhoprodutos/tamanhoprodutos.html',
                    resolve: {
                        load: ['$rootScope', '$ocLazyLoad', '$timeout', function($rootScope, $ocLazyLoad, $timeout) {
                            $rootScope.routeLoading = true;
                            return $ocLazyLoad.load('tamanhoprodutos').then(function(){ $rootScope.routeLoading = false; });
                        }]
                    }
                })
                    //Listar
                    .state('principal.cadastros.tamanhoprodutos.listar', {
                        url: '/cadastros/tamanhoprodutos',
                        templateUrl: 'src/modulos/aplicacao/cadastros/tamanhoprodutos/listar/listar.html',
                        controller: 'tamanhoprodutosListarCtrl',
                        controllerAs: 'ctrl'
                    })
                    //Manutencao
                    .state('principal.cadastros.tamanhoprodutos.manutencao', {
                        url: '/cadastros/tamanhoprodutos/:id?',
                        templateUrl: 'src/modulos/aplicacao/cadastros/tamanhoprodutos/manutencao/manutencao.html',
                        controller: 'tamanhoprodutosManutencaoCtrl',
                        controllerAs: 'ctrl'
                    })
    
                //Mesas (ABSTRATO)                
                .state('principal.cadastros.mesas', {
                    abstract: true,
                    controller: 'mesasCtrl',
                    controllerAs: 'ctrl',
                    templateUrl: 'src/modulos/aplicacao/cadastros/mesas/mesas.html',
                    resolve: {
                        load: ['$rootScope', '$ocLazyLoad', '$timeout', function($rootScope, $ocLazyLoad, $timeout) {
                            $rootScope.routeLoading = true;
                            return $ocLazyLoad.load('mesas').then(function(){ $rootScope.routeLoading = false; });
                        }]
                    }
                })
                    //Listar
                    .state('principal.cadastros.mesas.listar', {
                        url: '/cadastros/mesas',
                        templateUrl: 'src/modulos/aplicacao/cadastros/mesas/listar/listar.html',
                        controller: 'mesasListarCtrl',
                        controllerAs: 'ctrl'
                    })
                    //Manutencao
                    .state('principal.cadastros.mesas.manutencao', {
                        url: '/cadastros/mesas/:id?',
                        templateUrl: 'src/modulos/aplicacao/cadastros/mesas/manutencao/manutencao.html',
                        controller: 'mesasManutencaoCtrl',
                        controllerAs: 'ctrl'
                    })

                //Produtos (ABSTRATO)
                .state('principal.cadastros.produtos', {
                    abstract: true,
                    controller: 'produtosCtrl',
                    controllerAs: 'ctrl',
                    templateUrl: 'src/modulos/aplicacao/cadastros/produtos/produtos.html',
                    resolve: {
                        load: ['$rootScope', '$ocLazyLoad', '$timeout', function($rootScope, $ocLazyLoad, $timeout) {
                            $rootScope.routeLoading = true;
                            return $ocLazyLoad.load('produtos').then(function(){ $rootScope.routeLoading = false; });
                        }]
                    }
                })
                    //Listar
                    .state('principal.cadastros.produtos.listar', {
                        url: '/cadastros/produtos',
                        templateUrl: 'src/modulos/aplicacao/cadastros/produtos/listar/listar.html',
                        controller: 'produtosListarCtrl',
                        controllerAs: 'ctrl'
                    })
                    //Manutencao
                    .state('principal.cadastros.produtos.manutencao', {
                        url: '/cadastros/produtos/:id?',
                        templateUrl: 'src/modulos/aplicacao/cadastros/produtos/manutencao/manutencao.html',
                        controller: 'produtosManutencaoCtrl',
                        controllerAs: 'ctrl'
                    })



            //Garcom (ABSTRATO)
            .state('principal.garcom', {
                abstract: true,
                controller: 'garcomCtrl',
                controllerAs: 'ctrl',
                templateUrl: 'src/modulos/aplicacao/garcom/garcom.html',
            })
                //Relação de mesas
                .state('principal.garcom.relacaomesas', {
                    url: '/garcom/relacao-mesas',
                    templateUrl: 'src/modulos/aplicacao/garcom/relacaoMesas/relacaoMesas.html',
                    controller: 'relacaoMesasCtrl',
                    controllerAs: 'ctrl',
                    resolve: {
                        load: ['$rootScope', '$ocLazyLoad', function($rootScope, $ocLazyLoad) {
                            $rootScope.routeLoading = true;
                            return $ocLazyLoad.load(['garcom','garcom.relacaomesas']).then(function(){ $rootScope.routeLoading = false; });
                        }]
                    }
                })

        ;
}]);