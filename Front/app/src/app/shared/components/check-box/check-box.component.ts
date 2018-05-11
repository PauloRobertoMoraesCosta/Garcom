import { Component, OnInit, OnChanges, SimpleChanges, Input, ElementRef, Renderer } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'checkbox',
  templateUrl: './check-box.component.html'
})
export class CheckBox implements OnInit, OnChanges {
    @Input('model')
    model: Object = true;

    @Input('checked-value')
    checkedValue: Object = true;

    @Input('unchecked-value')
    uncheckedValue: Object = false;

    @Input()
    multiselect: boolean = false;

    @Input()
    disabled: boolean = false;

    @Input()
    left: boolean = false;

    @Input()
    right: boolean = false;

    @Input()
    center: boolean = false;

    @Input()
    class: String;

    @Input()
    ngClass: Object;

    private checked: boolean;
    private css: Object;

    constructor(public el: ElementRef, public renderer: Renderer)
    {

    }

    ngOnInit() {
        this.css = this.generateCss();
    }

    ngOnChanges(changes: SimpleChanges) {
        this.checked = this.model == this.checkedValue;
    }

    onToggle(){
        if(this.model == this.checkedValue)
            this.model = this.uncheckedValue;
        else
            this.model = this.checkedValue;

        this.checked = this.model == this.checkedValue;
    }

    private generateCss(){
        let css = {
            'display': 'table',
            'width': '100%',
            'height': 'inherit'
        };

        if(this.left){
            css.width = 'auto';
            css['padding-right'] = '7px';

            this.renderer.setElementStyle(this.el.nativeElement, 'display', 'table');
            this.renderer.setElementStyle(this.el.nativeElement, 'float', 'left');
        }
        else if(this.right){
            css.width = 'auto';
            css['padding-right'] = '7px';
            this.renderer.setElementStyle(this.el.nativeElement, 'display', 'table');
            this.renderer.setElementStyle(this.el.nativeElement, 'float', 'right');
        }

        return css;
    }
}
