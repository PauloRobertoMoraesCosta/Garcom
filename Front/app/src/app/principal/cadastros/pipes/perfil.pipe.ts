import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from './../../../core/constants';

@Pipe({
    name: 'perfil'
})
export class PerfilPipe implements PipeTransform {
    transform(value: number): string
    {
        var perfil = "";
        Constants.perfis.forEach((p) => {
            if(p.id == value){
                perfil = p.descricao;
                return;
            }
        });
        return perfil;
    }
}
