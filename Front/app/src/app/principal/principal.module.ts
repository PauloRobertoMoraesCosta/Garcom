import { PrincipalRouting } from './principal.routing';
import { SharedModule } from './../shared/shared.module';
import { CoreModule } from './../core/core.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { PrincipalComponent } from './principal.component';
import { CadastrosModule } from './cadastros/cadastros.module';

//Services
import { MenuService } from './services/menu.service';

//Components
import { InicioComponent } from './inicio/inicio.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        CoreModule,
        SharedModule,
        PrincipalRouting,
        CadastrosModule,
    ],
    declarations: [
        PrincipalComponent,
        InicioComponent,
    ],
    providers:[
        MenuService
    ],
    bootstrap: [PrincipalComponent]
})
export class PrincipalModule { }
