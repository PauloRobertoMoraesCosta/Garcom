import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/filter';

@Injectable()
export class Global {

    public routeLoading: boolean;
    public onRequest: boolean;

    public widthContent: number;
    public mobileWidth: number;
    public minMobileWidth: number;
    public mobile: boolean ;
    public minMobile: boolean;
    public window;
    public heightBody: number;
    public widthBody: number;
    public styleCardBody;

    public semAcentos(texto) {
        if (!texto) return texto;
        return texto.toString()
                        .replace(/[á|ã|â]/g, 'a')
                        .replace(/[é|ê]/g, 'e')
                        .replace(/í/g, 'i')
                        .replace(/[ó|õ|ô]/g, 'o')
                        .replace(/ú/g, 'u')
                        .replace(/ñ/g, 'n')
                        .replace(/ç/g, 'c');
    };

    constructor(){
        this.setDefaults();
        this.setValues(window);
        this.setWatchers();
    }

    setDefaults(){
        this.routeLoading = false;

        this.widthContent = 700;
        this.mobileWidth = 900;
        this.minMobileWidth = 575;
        this.mobile = false;
        this.minMobile = null;
        this.window = {
            width: null,
            height: null,
            xs: null,
            sm: null,
            md: null,
            lg: null,
            xl: null
        };
        this.heightBody = null;
        this.widthBody = null;
        this.styleCardBody = {
            'height': null,
            'width': null,
            'border-right':null,
            'border-bottom':null
        };
    }

    setValues(window){
        this.window.width = window.innerWidth;
        this.window.height = window.innerHeight;
        this.window.xs = this.window.innerWidth <= 575;
        this.window.sm = this.window.innerWidth > 575 && this.window.innerWidth > 768;
        this.window.md = this.window.innerWidth > 767 && this.window.innerWidth < 992;
        this.window.lg = this.window.innerWidth > 991 && this.window.innerWidth < 1200;
        this.window.xl = this.window.innerWidth > 1199;

        this.mobile = this.window.width <= this.mobileWidth;
        this.minMobile = this.window.width <= this.minMobileWidth;

        this.heightBody = this.window.height - 106;
        this.widthBody = this.window.width - (this.mobile ? 0 : 220);

        this.styleCardBody['height'] = this.heightBody+'px';
        this.styleCardBody['width'] = this.widthBody+'px';
        this.styleCardBody['border-right'] = 'none';
        this.styleCardBody['border-bottom'] = 'none';
    }

    setWatchers(){
        const $resizeEvent = Observable.fromEvent(window, 'resize').map(() => {
            return window;
        }).debounceTime(100);
        
        $resizeEvent.subscribe(window => {
            this.setValues(window);
        });
    }
}
