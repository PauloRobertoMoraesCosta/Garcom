import { CoreModule } from './core/core.module';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastModule } from 'ng2-toastr/ng2-toastr';
import { SharedModule } from './shared/shared.module';
import { AppRouting } from './app.routing';

//Services
import { HttpService } from './shared/services/httpService/http.service';
import { SessionService } from './shared/services/sessionService/session.service';
import { Confirm } from './shared/services/confirm/confirm.service';
import { Global } from './core/global';

//Guards
import { AuthGuard } from './shared/guards/auth.guard';

//Components
//import { Spinner } from './shared/components/spinner/spinner.component';

//Tela/Components
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { PrincipalModule } from './principal/principal.module';

//WebApi
import { UsuarioWebApi } from './shared/services/webapi/usuario-webapi.service';
import { Help } from './shared/classes/help';
import { Utils } from './core/services/utils';


@NgModule({
    declarations: [
        AppComponent,
        LoginComponent
    ],
    imports: [
        FormsModule,
        BrowserModule,
        BrowserAnimationsModule,
        ToastModule.forRoot(),
        HttpClientModule,
        PrincipalModule,
        AppRouting,
        SharedModule
    ],
    providers: [
        Global,
        Confirm,
        HttpService,
        SessionService,
        AuthGuard,
        Help,
        Utils,

        //WebApi
        UsuarioWebApi
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
