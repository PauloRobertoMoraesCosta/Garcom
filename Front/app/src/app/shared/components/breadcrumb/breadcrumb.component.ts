import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'breadcrumb',
    templateUrl: './breadcrumb.component.html',
    inputs: ['model']
})
export class BreadcrumbComponent implements OnInit{
    @Input()
    public model: Array<Breadcrumb> = new Array<Breadcrumb>();

    constructor(){
    }

    ngOnInit(){
    }
}

export class Breadcrumb {
    public nome: String;
    public url: String;

    constructor(nome: String, url: String = null) {
        this.nome = nome;
        this.url = url
    }
}
