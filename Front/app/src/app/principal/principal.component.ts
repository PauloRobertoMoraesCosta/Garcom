import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Global } from './../core/global';
import { MenuService } from './services/menu.service';
import { SessionService } from './../shared/services/sessionService/session.service';
import { Router } from '@angular/router';
import { Confirm } from './../shared/services/confirm/confirm.service';

@Component({
  selector: 'app-principal',
  templateUrl: './principal.component.html'
})
export class PrincipalComponent implements OnInit {
    constructor(private g: Global,
              private menu: MenuService,
              private sessionService: SessionService,
              private router: Router,
              private confirm: Confirm,
              private vcr: ViewContainerRef)
    {
        this.confirm.setRootVcr(vcr);
    }

    ngOnInit() {
    }

    private logout(){
      this.confirm.show('Deseja sair do aplicativo?<br/>É possível que as alterações feitas não sejam salvas.',
          () => {
              this.sessionService.logout();
              this.router.navigate(['/login']);
          }
      );
    }
}
