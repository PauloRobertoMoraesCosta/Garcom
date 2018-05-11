import { CadastrosRouting } from './cadastros.routing';
import { SharedModule } from './../../shared/shared.module';
import { CoreModule } from './../../core/core.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CadastrosComponent } from './cadastros.component';
import { UsuarioModule } from './components/usuario/usuario.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        CadastrosRouting,
        CoreModule,
        SharedModule,
        UsuarioModule
    ],
    declarations: [
        CadastrosComponent
    ],
    bootstrap: [CadastrosComponent]
})
export class CadastrosModule { }
