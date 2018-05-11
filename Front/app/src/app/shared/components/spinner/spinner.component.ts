import { Component, OnInit, Input, HostListener } from '@angular/core';

@Component({
    selector: 'spinner',
    templateUrl: './spinner.component.html'
})
export class Spinner {
    private css;

    @Input()
    white: boolean;

    @Input('padding')
    padding: String;

    constructor(){
        if(this.padding != '' && this.padding != null)
            this.css['padding'] = this.padding;
    }

    ngOnInit(){
    }

    @HostListener('click')
    onClick()
    {

    }
}
