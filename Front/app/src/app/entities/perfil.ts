export class Perfil{
    id: number;
    descricao: string;
    dataCadastro: Date;
    excluido: Boolean;

    public constructor(init?: Partial<Perfil>) {
        Object.assign(this, init);
    }
}
