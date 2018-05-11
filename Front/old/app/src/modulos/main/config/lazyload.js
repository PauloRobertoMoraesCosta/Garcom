angular.module('mainApp').config(['$ocLazyLoadProvider', function($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        modules: [

            //INICIAL
            {
                name: 'inicial',
                files: [

                    //PROVIDERS
                    'src/modulos/main/config/providers/ngToast.js',

                    //DIRETIVAS
                    'src/modulos/main/diretivas/ng-confirm/ngConfirm.js', 'src/modulos/main/diretivas/ng-confirm/ngConfirm.css',
                    'src/modulos/main/diretivas/spinner/spinner.js',
                    'src/modulos/main/diretivas/breadcrumb/breadcrumb.js',
                    'src/modulos/main/diretivas/checkbox/checkbox.js',
                    'src/modulos/main/diretivas/radio/radio.js',
                    'src/modulos/main/diretivas/btn-select/btnSelect.js',
                    'src/modulos/main/diretivas/focus.js',
                    'src/modulos/main/diretivas/nextEnter.js',

                    'src/modulos/main/config/filters.js',
                    'src/modulos/main/services/utils.js',

                    //API
                    'src/modulos/main/webapi/usuarioWebApi.js',
                    'src/modulos/main/webapi/ingredienteWebApi.js',
                    'src/modulos/main/webapi/grupoprodutosWebApi.js',
                    'src/modulos/main/webapi/tamanhoprodutosWebApi.js',
                    'src/modulos/main/webapi/produtoWebApi.js',
                    'src/modulos/main/webapi/mesaWebApi.js',

                    //LIBS
                    'lib/js/angular-img-cropper.min.js'

                ],
                serie: true
            },

            //LOGIN
            {
                name: 'login',
                files: [
                    'src/modulos/main/webapi/usuarioWebApi.js',
                    'src/modulos/login/loginModule.js',
                    'src/modulos/login/loginCtrl.js'
                ],
                serie: true
            },

            //PRINCIPAL
            {
                name: 'principal',
                files: [
                    'src/modulos/aplicacao/principalModule.js',
                    'src/modulos/aplicacao/principalCtrl.js',
                    'src/modulos/aplicacao/menubarCtrl.js'
                ],
                serie: true
            },

                //INICIO
                {
                    name: 'inicio',
                    files: [
                        'src/modulos/aplicacao/inicio/inicioCtrl.js'
                    ],
                    serie: true
                },

                //CADASTOS
                {
                    name: 'cadastros',
                    files: [
                        'src/modulos/aplicacao/cadastros/cadastrosModule.js',
                        'src/modulos/aplicacao/cadastros/cadastrosCtrl.js'
                    ],
                    serie: true
                },
                    //USUARIOS
                    {
                        name: 'usuarios',
                        files: [
                            'src/modulos/aplicacao/cadastros/usuarios/usuariosCtrl.js',
                            'src/modulos/aplicacao/cadastros/usuarios/listar/usuariosListarCtrl.js',
                            'src/modulos/aplicacao/cadastros/usuarios/manutencao/usuariosManutencaoCtrl.js',
                        ],
                        serie: true
                    },
                    //INGREDIENTES
                    {
                        name: 'ingredientes',
                        files: [
                            'src/modulos/aplicacao/cadastros/ingredientes/ingredientesCtrl.js',
                            'src/modulos/aplicacao/cadastros/ingredientes/listar/ingredientesListarCtrl.js',
                            'src/modulos/aplicacao/cadastros/ingredientes/manutencao/ingredientesManutencaoCtrl.js',
                        ],
                        serie: true
                    },
                    //GRUPO PRODUTOS
                    {
                        name: 'grupoprodutos',
                        files: [
                            'src/modulos/aplicacao/cadastros/grupoprodutos/grupoprodutosCtrl.js',
                            'src/modulos/aplicacao/cadastros/grupoprodutos/listar/grupoprodutosListarCtrl.js',
                            'src/modulos/aplicacao/cadastros/grupoprodutos/manutencao/grupoprodutosManutencaoCtrl.js',
                        ],
                        serie: true
                    },
                    //TAMANHO PRODUTOS
                    {
                        name: 'tamanhoprodutos',
                        files: [
                            'src/modulos/aplicacao/cadastros/tamanhoprodutos/tamanhoprodutosCtrl.js',
                            'src/modulos/aplicacao/cadastros/tamanhoprodutos/listar/tamanhoprodutosListarCtrl.js',
                            'src/modulos/aplicacao/cadastros/tamanhoprodutos/manutencao/tamanhoprodutosManutencaoCtrl.js',
                        ],
                        serie: true
                    },
                    //PRODUTOS
                    {
                        name: 'produtos',
                        files: [
                            'src/modulos/aplicacao/cadastros/produtos/produtosCtrl.js',
                            'src/modulos/aplicacao/cadastros/produtos/listar/produtosListarCtrl.js',
                            'src/modulos/aplicacao/cadastros/produtos/manutencao/produtosManutencaoCtrl.js',
                        ],
                        serie: true
                    },
                    //MESAS
                    {
                        name: 'mesas',
                        files: [
                            'src/modulos/aplicacao/cadastros/mesas/mesasCtrl.js',
                            'src/modulos/aplicacao/cadastros/mesas/listar/mesasListarCtrl.js',
                            'src/modulos/aplicacao/cadastros/mesas/manutencao/mesasManutencaoCtrl.js',
                        ],
                        serie: true
                    },

                //GARCOM
                {
                    name: 'garcom',
                    files: [
                        'src/modulos/aplicacao/garcom/garcomModule.js',
                        'src/modulos/aplicacao/garcom/garcomCtrl.js'
                    ],
                    serie: true
                },
                    //RELACAO_MESAS
                    {
                        name: 'garcom.relacaomesas',
                        files: [
                            'src/modulos/aplicacao/garcom/relacaoMesas/relacaoMesasCtrl.js'
                        ],
                        serie: true
                    },

                //TESTE
                {
                    name: 'teste',
                    files: [
                        'src/modulos/aplicacao/teste/testeCtrl.js'
                    ],
                    serie: true
                },
        ]
    });
}]);
