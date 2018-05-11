import { NgModule } from "@angular/core";
import { Routes, RouterModule, PreloadAllModules } from "@angular/router";
import { AuthGuard } from './shared/guards/auth.guard';
import { LoginComponent } from './login/login.component';
import { PrincipalModule } from './principal/principal.module';

const rotas: Routes = [
    { path : 'login', component: LoginComponent,  canActivate: [AuthGuard] },
    { path: 'principal', loadChildren: () => PrincipalModule },

    //Another pages
    { path : '**', redirectTo: '/login', pathMatch: 'full' }
];


@NgModule({
    imports: [
        RouterModule.forRoot(rotas, {preloadingStrategy: PreloadAllModules})
    ],
    exports: [RouterModule]

})
export class AppRouting{

}
