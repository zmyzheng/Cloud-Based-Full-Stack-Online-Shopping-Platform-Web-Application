import { Injectable } from '@angular/core';
import {User} from "./User";
import { Headers, RequestOptions } from '@angular/http';
import { Http, Response }          from '@angular/http';
import 'rxjs/add/operator/toPromise';
import {Item_Response1} from "./mock-data/mock-movies";

@Injectable()
export class ItemService {

    constructor (private http: Http) {}

    private Urli = 'http://ec2-54-165-183-168.compute-1.amazonaws.com:3000/product';

    async getItemsRemote(): Promise<any> {
        console.log("item fetch");
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        try {
            let res = await this.http.get(this.Urli, options).toPromise();

            console.log(res.json());
            return res.json();

        } catch (ex) {
            console.log(ex);
            this.handleError(ex);
        }
    }

    private UrlToken = 'http://ec2-54-165-183-168.compute-1.amazonaws.com:3000/payment';

    async sendTokenToServer(token : string, JWT : string, id : string, price : number) : Promise<any> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        try {
            let res = await this.http.post(this.UrlToken, { Token : token, JWT : JWT, Id : id, stripeMoney : price }, options).toPromise();
            console.log(res);
            return res.json();
        } catch (ex) {
            console.log(ex);
            this.handleError(ex);
        }
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}

