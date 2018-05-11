import { Injectable } from '@angular/core';
import { ToastsManager } from 'ng2-toastr';
import { SessionService } from '../services/sessionService/session.service';
import { Router } from '@angular/router';

@Injectable()
export class Help {

    constructor(private sessionService: SessionService,
                private router: Router){
        
    }

    showError(ngToast: ToastsManager, objError, status){
        if(objError !== null && typeof objError === 'object')
        {
            if('error_description' in objError)
                ngToast.error(objError.error_description, 'Ops!');
            else if('message' in objError){
                if(status == 401 || status === undefined){
                    if(objError.message !== 'Authorization has been denied for this request.')
                        ngToast.error(objError.message, 'Ops!');
                    else{
                        ngToast.error('Sua sessão expirou ou o acesso foi negado. Faça o login novamente.', 'Ops!');
                        this.sessionService.logout();
                        this.router.navigate(['/login']);
                    }
                }
                else
                    ngToast.error(objError.message, 'Ops!');
            }
            else
                ngToast.error(objError, 'Ops!');
        }
        else{
            if(typeof objError === 'string')
                ngToast.error(objError, 'Ops!');
            else if(objError === null && status == -1)
                ngToast.error('Não foi possível conectar-se ao servidor.', 'Ops!');
            else
                ngToast.error('Ocorreu um erro ao realizar esta operação. Contate o suporte.', 'Ops!');
        }
    }
}
