import { Component, OnInit } from '@angular/core';
import { Breadcrumb, BreadcrumbComponent } from './../../shared/components/breadcrumb/breadcrumb.component';
import { Global } from './../../core/global';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html'
})
export class InicioComponent implements OnInit {
    public breadcrumb: Array<Breadcrumb> = [
        new Breadcrumb('Início', '/principal/inicio'),
        new Breadcrumb('Bem vindo')
    ];

    constructor(private g: Global) { }

    ngOnInit() {
    }
}
