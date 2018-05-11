import { Injectable } from '@angular/core';
import { Usuario } from './../../../entities/usuario';
import { Token } from './../../../entities/token';

@Injectable()
export class SessionService {
    private usuario: Usuario = null;
    private token: Token = null;

    constructor() { }

    setToken(dados){
        this.token = new Token();
        this.token.tokenType = dados.token_type;
        this.token.accessToken = dados.access_token;
    }

    getToken(){
        return this.token;
    }

    setUsuario(usr){
        this.usuario = new Usuario();
        this.usuario.id = usr.id;
        this.usuario.login = usr.login;
        this.usuario.nome = usr.nome;
        this.usuario.perfilId = usr.perfilId;
        this.usuario.senha = usr.senha;
        this.usuario.dataCadastro = usr.dataCadastro;
        this.usuario.ativo = usr.ativo;
    }

    getUsuario(){
        return this.usuario;
    }

    isLogado(){
        return this.usuario != null;
    }

    logout(){
        this.usuario = null;
        this.token = null;
    }
}
