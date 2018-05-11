import { Perfil } from "../entities/perfil";

export class Constants {
    public static get perfis(): Perfil[]
    { 
        return [
            new Perfil({ id: 1, descricao: 'Administrador' }),
            new Perfil({ id: 2, descricao: 'Caixa' }),
            new Perfil({ id: 3, descricao: 'Cozinha' }),
            new Perfil({ id: 4, descricao: 'Garçom' })
        ];
    }
}
