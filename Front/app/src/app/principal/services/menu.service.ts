import { Injectable } from '@angular/core';
import { SessionService } from './../../shared/services/sessionService/session.service';
import { Global } from './../../core/global';
import { Router } from "@angular/router";

@Injectable()
export class MenuService {

    showMenu: boolean;
    widthAnterior: number;
    itens;
    firstItemGo: Object;

    constructor(private sessionService: SessionService,
                private g: Global,
                private router: Router)
    {
        this.defaults();
        //this.setWatchers();
        this.setItens();
    };

    defaults(){
        this.showMenu = false;
        this.widthAnterior = this.g.window.width;
        this.itens = [];
        this.firstItemGo = null;
    };

    setItens(){
        this.itens = [
            {
                id:1, nome: 'Cadastros', iconeClass: 'note_add', idPerfis: [1],
                submenu:
                {
                    show: false,
                    itens: [
                        {id: 1, nome: 'Usuários', url:'/principal/cadastros/usuarios/listar'},
                        {id: 2, nome: 'Igredientes', url:'/principal/cadastros/ingredientes/listar'},
                        {id: 3, nome: 'Grupo de produtos', url:'/principal/cadastros/grupoprodutos/listar'},
                        {id: 4, nome: 'Tamanho do produtos', url:'/principal/cadastros/tamanhoprodutos/listar'},
                        {id: 5, nome: 'Produtos', url:'/principal/cadastros/produtos/listar'},
                        {id: 6, nome: 'Mesas', url:'/principal/cadastros/mesas/listar'}
                    ]
                }
            },
            {
                id:2, nome: 'Garçom', idPerfis: [1,4],
                iconeSvg: { disabled: 'assets/img/icones/garcom0.svg', enabled: 'assets/img/icones/garcom1.svg', width: 23 },
                state: 'principal/garcom/relacaomesas',
                submenu: {
                    show: false,
                    itens: []
                }
            },
            {
                id:3, nome: 'Cozinha',
                iconeSvg: { disabled: 'assets/img/icones/cozinha0.svg', enabled: 'assets/img/icones/cozinha1.svg', width: 23 },
                state: 'principal/teste2', idPerfis: [1,3], inativo: true,
                submenu: {
                    show: false,
                    itens: []
                }
            },
            {
                id:4, nome: 'Caixa',
                iconeSvg: { disabled: 'assets/img/icones/caixa0.svg', enabled: 'assets/img/icones/caixa1.svg', width: 23 },
                state: 'principal/teste3', idPerfis: [1,2], inativo: true,
                submenu: {
                    show: false,
                    itens: []
                }
            }
        ];

        this.checkItensPermissao();
    };

    toggleMenu(){
        this.showMenu = !this.showMenu;
    }

    toggleSubmenu(item){
        if(!this.isAtualSubmenu(item.submenu))
            item.submenu.show = !item.submenu.show;
    }

    goState(item){
        if(item != null){
            //console.log(item.url);
            this.router.navigate([item.url]);
            if(this.g.mobile) this.showMenu = false;
        }
    }

    isAtualSubmenu = function(submenu){
        let atualSubmenu = null;
        for(let item of this.itens){
            if(this.router.url == item.url){
                atualSubmenu = item.submenu;
                return;
            }
            else if(item.submenu && item.submenu.itens.length > 0){
                for(let subItem of item.submenu.itens){
                    if(this.router.url == subItem.url){
                        atualSubmenu = item.submenu;
                        return;
                    }
                }
            }
        }
        return atualSubmenu != null && atualSubmenu == submenu;
    };

    checkItensPermissao(){
        var perfilId = this.sessionService.getUsuario().perfilId;
        var newList = [];
        this.firstItemGo = null;
        this.itens.forEach(item => {
            var temPermissao = false;
            item.idPerfis.forEach(_perfilId => {
                if(perfilId == _perfilId){
                    temPermissao = true;
                    return;
                }
            });
            if(temPermissao) newList.push(item);
        });
        this.itens = newList;
    };
}
