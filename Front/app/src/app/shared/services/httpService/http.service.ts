import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { SessionService } from './../sessionService/session.service';
import { Global } from './../../../core/global';

@Injectable()
export class HttpService {
    private serverApi: String = 'http://localhost:50964/';

    constructor(private http: HttpClient,
                private sessionService: SessionService,
                private global: Global) { }

    private generateHeaders(){
        var headers = {
            'Content-Type': 'application/json',
            'Authorization': this.sessionService.getToken().tokenType + ' ' + this.sessionService.getToken().accessToken
        }

        return new HttpHeaders(headers);
    }

    private defaultSubscribe(promise, success?, error?){
        promise.subscribe(
            (data) => { 
                this.global.onRequest = false; 
                if(success) success(data);
            },
            (data) => {
                this.global.onRequest = false; 
                if(error) error(data);
            });
    }

    autenticar(usuario, senha, success?, error?) {
        var obj = 'grant_type=password&username=' + usuario + '&password=' + senha;
        var opt = { headers: new HttpHeaders({'Content-type': "application/x-www-form-urlencoded"}) };

        this.global.onRequest = true;

        let promise = this.http.post(this.serverApi + 'Autenticar', obj, opt);
        this.defaultSubscribe(promise, success, error);
    }

    get(url: String, success?, error?, opts: Object = null) {
        this.global.onRequest = true;

        let promise = this.http.get(this.serverApi +'api/'+ url, { headers: this.generateHeaders() });
        this.defaultSubscribe(promise, success, error);
    }

    post(url: String, obj: Object, success?, error?, opts: Object = null){
        this.global.onRequest = true;

        let promise = this.http.post(this.serverApi +'api/'+ url, obj, { headers: this.generateHeaders() });
        promise.map(res => { this.global.onRequest = false; console.log(res); });
        this.defaultSubscribe(promise, success, error);
    }

    put(url: String, obj: Object, success?, error?, opts: Object = null){
        this.global.onRequest = true;

        let promise = this.http.put(this.serverApi +'api/'+ url, obj, { headers: this.generateHeaders() });
        this.defaultSubscribe(promise, success, error);
    }

    del(url: String, success?, error?, opts: Object = null){
        this.global.onRequest = true;

        let promise = this.http.delete(this.serverApi +'api/'+ url, { headers: this.generateHeaders() });
        this.defaultSubscribe(promise, success, error);
    }
}
