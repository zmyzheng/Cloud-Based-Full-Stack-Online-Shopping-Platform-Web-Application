import { Injectable } from '@angular/core';
import {User} from './User';
import { Headers, RequestOptions, URLSearchParams} from '@angular/http';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class ItemService {

    private Urli = 'https://6k1n8i5jx5.execute-api.us-east-1.amazonaws.com/prod/products';

    constructor (private http: Http) {}



    async getItemsRemote(): Promise<any> {
        console.log('item fetch');
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });
        let params: URLSearchParams = new URLSearchParams();
        params.set('operator', 'LT');
        params.set('field', 'id');
        params.set('start', '0');
        params.set('count', '10');
        params.set('value', '10');

        options.search = params;

        try {
            const res = await this.http.get(this.Urli, options).toPromise();

            console.log(res.json());
            return res.json();

        } catch (ex) {
            console.log(ex);
            this.handleError(ex);
        }
    }

    // private UrlToken = 'https://6k1n8i5jx5.execute-api.us-east-1.amazonaws.com/prod/payments/';
    //
    // async sendTokenToServer(token : string, JWT : string, id : string, price : number) : Promise<any> {
    //     let headers = new Headers({ 'Content-Type': 'application/json' });
    //     let options = new RequestOptions({ headers: headers });
    //
    //     try {
    //         let res = await this.http.post(this.UrlToken, { Token : token, JWT : JWT, Id : id, stripeMoney : price }, options).toPromise();
    //         console.log(res);
    //         return res.json();
    //     } catch (ex) {
    //         console.log(ex);
    //         this.handleError(ex);
    //     }
    // }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}

