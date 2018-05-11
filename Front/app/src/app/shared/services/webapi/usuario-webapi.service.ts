import { Injectable } from '@angular/core';
import { HttpService } from './../httpService/http.service';
import { SessionService } from './../sessionService/session.service';
import { Usuario } from './../../../entities/usuario';

@Injectable()
export class UsuarioWebApi {
    private prefixo: String = 'usuarios/';

    constructor(private httpService: HttpService,
                private sessionService: SessionService) { }

    autenticar(usuario, senha, success?, error?){
        this.httpService.autenticar(usuario, senha,
            data => {
                this.sessionService.setToken(data);

                this.getUsuario(usuario, 
                    usuario => {
                        this.sessionService.setUsuario(usuario);
                        if(success) success(usuario);
                    },
                    data => {
                        if(error) error(data);
                    }
                );
            },
            data => { if(error) error(data); }
        );
    }

    getUsuario(usuario: String, sucess?, error?) {
        return this.httpService.get(this.prefixo + '' + usuario, sucess, error);
    };

    alterar(usuario, sucess?, error?) {
        usuario['usuarioLogado'] = this.sessionService.getUsuario().login;
        return this.httpService.put(this.prefixo, usuario, sucess, error);
    };

    cadastrar(usuario, sucess?, error?) {
        usuario['usuarioLogado'] = this.sessionService.getUsuario().login;
        return this.httpService.post(this.prefixo, usuario, sucess, error);
    };

    getUsuarios(sucess?, error?) {
        return this.httpService.get(this.prefixo, sucess, error);
    };
}
