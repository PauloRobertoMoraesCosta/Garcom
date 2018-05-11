import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import { Router, ActivatedRoute, Params } from "@angular/router";
import { OrderByPipe } from './../../../../../shared/pipes/orderby.pipe';
import { Confirm }  from './../../../../../shared/services/confirm/confirm.service';
import { Breadcrumb } from './../../../../../shared/components/breadcrumb/breadcrumb.component';
import { Constants }  from './../../../../../core/constants';
import { Global }  from './../../../../../core/global';
import { Usuario } from './../../../../../entities/usuario';
import { Perfil } from './../../../../../entities/perfil';
import { UsuarioWebApi } from './../../../../../shared/services/webapi/usuario-webapi.service';
import { Help } from '../../../../../shared/classes/help';

@Component({
    selector: 'app-usuario-listar',
    templateUrl: './usuario.listar.component.html'
})
export class UsuarioListarComponent implements OnInit {
    private breadcrumb: Array<Breadcrumb>;
    private params: Params;

    private usuarios: Usuario[] = [];
    private perfis: Perfil[] = [];

    public filtro: Usuario = new Usuario();

    private loading: Boolean = false;

    private ordenacao: { propriedade: string; asc: boolean } = {
        propriedade: 'nome',
        asc: true
    };

    constructor(public toast: ToastsManager,
                private route: ActivatedRoute,
                private router: Router,
                private g: Global,
                private help: Help,
                private confirm: Confirm,
                private vcr: ViewContainerRef,
                private orderByPipe: OrderByPipe,
                private usuarioWebApi: UsuarioWebApi)
    {
        this.confirm.setRootVcr(this.vcr);
        this.perfis = Constants.perfis;
    }

    ngOnInit() {
        this.params = this.route.snapshot.params;
        this.breadcrumb = [
            new Breadcrumb('Início', '/principal/inicio'),
            new Breadcrumb('Usuários')
        ];

        this.listar();
    }

    listar(){
        this.loading = true;
        this.usuarioWebApi.getUsuarios(
            (data: Usuario[]) => {
                this.usuarios = data;
                this.ordenar();
                this.loading = false;
            },
            error => {
                this.help.showError(this.toast, error.error, error.status);
                this.loading = false;
            }
        );
    }

    ordenar(propriedade: string = null){
        if(propriedade != null){
            if(propriedade === this.ordenacao.propriedade) this.ordenacao.asc = !this.ordenacao.asc;
            else {
                this.ordenacao.propriedade = propriedade;
                this.ordenacao.asc = true;
            }
        }

        if(this.usuarios.length > 0){
            this.usuarios = this.orderByPipe.transform(this.usuarios, this.ordenacao.propriedade, this.ordenacao.asc);
        }
    }

    goManutencao(usuarioId: number = 0){
        this.router.navigate(['principal/cadastros/usuarios/' + (usuarioId > 0 ? usuarioId : '')]);
    }
}
