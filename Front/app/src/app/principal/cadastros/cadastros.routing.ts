import { RouterModule } from '@angular/router';
import { Routes } from "@angular/router/src/config";
import { NgModule } from "@angular/core";
import { CadastrosComponent } from './cadastros.component';
import { UsuarioModule } from './components/usuario/usuario.module';

const rotas : Routes = [
    {
        path: 'cadastros',
        component: CadastrosComponent,
        children:
        [
            { path : 'usuarios', loadChildren: () => UsuarioModule }
        ]
    },
];

@NgModule({
    imports: [
        RouterModule.forChild(rotas)
    ],
    exports: [RouterModule]
})

export class CadastrosRouting{

}
