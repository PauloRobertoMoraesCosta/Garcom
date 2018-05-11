export class Usuario{
    id: number;
    login: String;
    nome: String;
    perfilId: number;
    senha: String;
    confirmacaoSenha: String;
    dataCadastro: Date;
    ativo: Boolean;

    constructor(init?: Partial<Usuario>) {
        if(init != null) Object.assign(this, init);
        else{
            this.id = undefined;
            this.login = undefined;
            this.nome = undefined;
            this.perfilId= undefined;
            this.senha = undefined;
            this.confirmacaoSenha = undefined;
            this.dataCadastro = undefined;
            this.ativo = undefined;
        }
    }
}
