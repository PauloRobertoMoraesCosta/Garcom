import { RouterModule } from '@angular/router';
import { Routes } from "@angular/router/src/config";
import { NgModule } from "@angular/core";
import { AuthGuard } from './../shared/guards/auth.guard';
import { CadastrosModule } from "./cadastros/cadastros.module";
import { InicioComponent } from "./inicio/inicio.component";
import { PrincipalComponent } from "./principal.component";

const rotasPrincipal : Routes = [
    {
        path: 'principal',
        component: PrincipalComponent,
        canActivate: [AuthGuard],
        children:
        [
            //{ path: '**', redirectTo: 'inicio', pathMatch: 'full' },
            { path : 'inicio', component: InicioComponent },
            { path : 'cadastros', loadChildren: () => CadastrosModule }
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(rotasPrincipal)
    ],
    exports: [RouterModule]
})

export class PrincipalRouting{

}
