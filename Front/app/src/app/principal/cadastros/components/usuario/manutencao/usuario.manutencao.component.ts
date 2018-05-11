import { Component, OnInit, ViewContainerRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute, Params } from "@angular/router";
import { Confirm }  from './../../../../../shared/services/confirm/confirm.service';
import { Breadcrumb } from './../../../../../shared/components/breadcrumb/breadcrumb.component';
import { Constants }  from './../../../../../core/constants';
import { Global }  from './../../../../../core/global';
import { Usuario } from './../../../../../entities/usuario';
import { Perfil } from './../../../../../entities/perfil';
import { NgForm } from '@angular/forms';
import { UsuarioWebApi } from '../../../../../shared/services/webapi/usuario-webapi.service';
import { ToastsManager } from 'ng2-toastr';
import { Help } from '../../../../../shared/classes/help';
import { Utils } from '../../../../../core/services/utils';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-usuario-manutencao',
  templateUrl: './usuario.manutencao.component.html'
})
export class UsuarioManutencaoComponent implements OnInit {
    public breadcrumb: Array<Breadcrumb>;
    private params: Params;

    private perfis: Perfil[] = [];

    private usuario: Usuario = new Usuario();
    private loading: Boolean = false;
    private initOk: Boolean = false;

    private validacaoSenha: any = {
        maiusculo: false,
        numero: false,
        seis: false
    };

    @ViewChild('frm')
    public frm: NgForm;

    public get frmCtrls(): any { return this.frm ? this.frm.controls : {}; };

    constructor(private route: ActivatedRoute,
                private router: Router,
                private g: Global,
                private confirm: Confirm,
                private help: Help,
                private utils: Utils,
                private toast: ToastsManager, 
                private vcr: ViewContainerRef,
                private usuarioWebApi: UsuarioWebApi)
    {
        this.toast.setRootViewContainerRef(vcr);
        this.confirm.setRootVcr(this.vcr);

        this.perfis = Constants.perfis;
    }

    ngOnInit() {
        this.params = this.route.snapshot.params;
        this.breadcrumb = [
            new Breadcrumb('Início', '/principal/inicio'),
            new Breadcrumb('Usuários', 'principal/cadastros/usuarios/listar'),
            new Breadcrumb((this.params.id && this.params.id > 0) ? 'Alterar' : 'Cadastrar')
        ];

        if(this.params.id && this.params.id > 0) this.get();
        else{
            this.usuario = new Usuario();
            this.usuario.id = 0;
        }
    }
    
    validaConfirmaSenha(): Boolean{
        if((this.usuario.id <= 0 && !this.usuario.confirmacaoSenha) || (this.usuario.id > 0 && (this.usuario.confirmacaoSenha || this.usuario.senha))) 
        {
            if(!this.usuario.confirmacaoSenha)
                return false;
            if(this.usuario.senha != this.usuario.confirmacaoSenha)
                return false;
        }

        return true;
    }

    validaSenha(): Boolean{
        var sucesso = true;
        this.validacaoSenha = {
            maiusculo: false,
            numero: false,
            seis: false
        };

        if(this.utils.possuiCaracterMaiusculo(this.usuario.senha))
            this.validacaoSenha.maiusculo = true;
        else sucesso = false;
        
        if(this.utils.possuiNumero(this.usuario.senha))
            this.validacaoSenha.numero = true;
        else sucesso = false;

        if(this.usuario.senha && this.usuario.senha.length >= 6)
            this.validacaoSenha.seis = true;
        else sucesso = false;

        if(this.usuario.id > 0 && !this.usuario.senha)
            return true;
        else
            return sucesso;
    }

    validar(): Boolean{
        if(!this.usuario.login) return false;
        if(!this.usuario.nome) return false;
        if(!this.usuario.perfilId) return false;
        if(!this.usuario.dataCadastro) return false;

        if((this.usuario.id == 0 && !this.validaSenha()) || (this.usuario.id > 0 && this.usuario.senha && this.usuario.senha != '' && !this.validaSenha())) return false;
        if(this.usuario.id > 0 && (this.usuario.senha && this.usuario.senha != '') && !this.validaConfirmaSenha()) return false;

        return true;
    };

    get(){
        this.loading = true;
        this.usuarioWebApi.getUsuario(this.params.id,
            (data: Usuario) => {
                this.usuario = data;
                this.loading = false;
            },
            (error, status) => {
                this.help.showError(this.toast, error.error, error.status);
                this.loading = false;
            }
        );
    }

    salvar(){
        if(this.validar()){
            this.loading = true;
            if(this.usuario.id > 0){
                this.usuarioWebApi.alterar(this.usuario,
                    (data) => {
                        this.toast.success('Alterações realizadas com sucesso.');
                        this.router.navigate(['principal/cadastros/usuarios/listar']);
                    },
                    (error, status) => {
                        this.help.showError(this.toast, error.error, error.status);
                        this.loading = false;
                    }
                );
            }
            else{
                this.usuarioWebApi.cadastrar(this.usuario,
                    (data) => {
                        this.toast.success('Cadastro realizado com sucesso.');
                        this.router.navigate(['principal/cadastros/usuarios/listar']);
                    },
                    (error, status) => {
                        this.help.showError(this.toast, error.error, error.status);
                        this.loading = false;
                    }
                );
            }
        }
    }
}
