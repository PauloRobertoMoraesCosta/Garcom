import { UsuarioRouting } from './usuario.routing';
import { SharedModule } from './../../../../shared/shared.module';
import { CoreModule } from './../../../../core/core.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { PerfilPipe } from './../../pipes/perfil.pipe';

import { UsuarioComponent } from './usuario.component';
import { UsuarioListarComponent } from './listar/usuario.listar.component';
import { UsuarioManutencaoComponent } from './manutencao/usuario.manutencao.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CoreModule,
    UsuarioRouting,
    SharedModule
  ],
  declarations: [
      UsuarioComponent,
      UsuarioListarComponent,
      UsuarioManutencaoComponent,
      PerfilPipe
  ],
  bootstrap: [UsuarioComponent]
})
export class UsuarioModule { }
