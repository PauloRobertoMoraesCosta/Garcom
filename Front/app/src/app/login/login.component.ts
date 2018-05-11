import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { ToastsManager } from 'ng2-toastr/ng2-toastr';
import { UsuarioWebApi } from './../shared/services/webapi/usuario-webapi.service';
import { Usuario } from './../entities/usuario';
import { Router } from '@angular/router';
import { Help } from '../shared/classes/help';

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
    private usuario: Usuario = new Usuario();

    private manterLogin: boolean = true;
    private tempoManterLogin: number = 6;

    private loading: boolean = false;

    constructor(public toast: ToastsManager, 
                public vcr: ViewContainerRef,
                private help: Help,
                private usuarioWebApi: UsuarioWebApi,
                private router: Router)
    {
        this.toast.setRootViewContainerRef(vcr);

        this.usuario.login = 'usuario';
        this.usuario.senha = 'Usuario1';
    }

    ngOnInit()
    { }

    enviar()
    {
        if(this.usuario.login && this.usuario.senha)
        {
            this.loading = true;
            this.usuarioWebApi.autenticar(this.usuario.login, this.usuario.senha,
                (data) => {
                    this.toast.success('Usuário logado :D', 'Logado!');
                    this.loading = false;
                    this.router.navigate(['/principal/inicio']);
                },
                (error, status) => {
                    this.help.showError(this.toast, error.error, error.status);
                    this.loading = false;
                });
        }
    }
}
