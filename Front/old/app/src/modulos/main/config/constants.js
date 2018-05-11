angular.module('mainApp')

    //ITENS_MENU
    .constant('itensmenu', [
        {
            id:1, nome: 'Cadastros', iconeClass: 'note_add', idPerfis: [1],
            submenu:
            {
                show: false,
                itens: [
                    {id: 1, nome: 'Usuários', state: 'principal.cadastros.usuarios.listar'},
                    {id: 2, nome: 'Igredientes', state: 'principal.cadastros.ingredientes.listar'},
                    {id: 3, nome: 'Grupo de produtos', state: 'principal.cadastros.grupoprodutos.listar'},
                    {id: 4, nome: 'Tamanho do produtos', state: 'principal.cadastros.tamanhoprodutos.listar'},
                    {id: 5, nome: 'Produtos', state: 'principal.cadastros.produtos.listar'},
                    {id: 6, nome: 'Mesas', state: 'principal.cadastros.mesas.listar'}
                ]
            }
        },
        {
            id:2, nome: 'Garçom', idPerfis: [1,4],
            iconeSvg: { disabled: 'src/img/icones/garcom0.svg', enabled: 'src/img/icones/garcom1.svg', width: 23 },
            state: 'principal.garcom.relacaomesas',
            submenu: {
                show: false,
                itens: []
            }
        },
        {
            id:3, nome: 'Cozinha',
            iconeSvg: { disabled: 'src/img/icones/cozinha0.svg', enabled: 'src/img/icones/cozinha1.svg', width: 23 },
            state: 'principal.teste2', idPerfis: [1,3], inativo: true,
            submenu: {
                show: false,
                itens: []
            }
        },
        {
            id:4, nome: 'Caixa',
            iconeSvg: { disabled: 'src/img/icones/caixa0.svg', enabled: 'src/img/icones/caixa1.svg', width: 23 },
            state: 'principal.teste3', idPerfis: [1,2], inativo: true,
            submenu: {
                show: false,
                itens: []
            }
        }
    ])

    //PERFIS
    .constant('perfis', [
        {id: 1, descricao: 'Administrador'},
        {id: 2, descricao: 'Caixa'},
        {id: 3, descricao: 'Cozinha'},
        {id: 4, descricao: 'Garçom'},
    ]);