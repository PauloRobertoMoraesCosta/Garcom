import { RouterModule } from '@angular/router';
import { Routes } from "@angular/router/src/config";
import { NgModule } from "@angular/core";
import { UsuarioComponent } from './usuario.component';
import { UsuarioListarComponent } from './listar/usuario.listar.component';
import { UsuarioManutencaoComponent } from './manutencao/usuario.manutencao.component';

const rotas : Routes = [
    {
        path: 'usuarios',
        component: UsuarioComponent,
        children:
        [
            { path: 'listar', component: UsuarioListarComponent },
            { path: '', component: UsuarioManutencaoComponent },
            { path: ':id', component: UsuarioManutencaoComponent }
        ]
    },
];

@NgModule({
    imports: [
        RouterModule.forChild(rotas)
    ],
    exports: [RouterModule]
})

export class UsuarioRouting{

}
