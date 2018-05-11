import { Injectable } from '@angular/core';

@Injectable()
export class Utils {
    constructor(){
        
    }

    possuiCaracterMaiusculo(texto): Boolean{
        if(typeof texto == 'string')
            for(var i = 0; i < texto.length; i++){
                if(isNaN(parseInt(texto.charAt(i))) && texto.charAt(i).toUpperCase() == texto.charAt(i)) return true;
            }
        return false;
    };
    possuiCaracterMinusculo(texto): Boolean{
        if(typeof texto == 'string')
            for(var i = 0; i < texto.length; i++){
                if(isNaN(parseInt(texto.charAt(i))) && texto.charAt(i).toLowerCase() == texto.charAt(i)) return true;
            }
        return false;
    };
    possuiNumero(texto): Boolean{
        if(typeof texto == 'string')
            for(var i = 0; i < texto.length; i++){
                if(!isNaN(parseInt(texto.charAt(i)))) return true;
            }
        return false;
    };
}
