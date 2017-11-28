import { Injectable } from '@angular/core';
import { Cart } from "./Cart";
import {Item} from "./Item";
import { Headers, RequestOptions, URLSearchParams} from '@angular/http';
import { Http, Response } from '@angular/http';



@Injectable()
export class CartService {

    myCart: Cart;

    constructor(private http: Http) {
        this.initialCart();
    }

    initialCart():void {
        // todo get cart from server
        this.myCart = new Cart();
    }

    addToCart(item: Item, num: number): void {
        // todo send to server
        this.myCart.addToLocalCart(item, num);
        console.log(this.myCart);
    }

    updateCart(keys: string[], items: Item[], nums: number[]) {
        // todo send to server
        this.myCart.updateLocalCart(keys, items, nums);
    }


    getCartContent(items: Item[], nums: number[], keys: string[]) {
        for (let key of this.myCart.count.keys()){
            keys.push(key);
            nums.push(this.myCart.count.get(key));
            items.push(this.myCart.content.get(key));
        }
    }


    getCartTotalPrice(): number  {
        return this.myCart.getTotalPrice();
    }


    clearCart() {
        this.myCart = new Cart();
    }

    private UrlToken = 'https://6k1n8i5jx5.execute-api.us-east-1.amazonaws.com/prod/payments/';

    async checkoutOrder(JWT: string, Uid: string, StripToken: string, Charge: number, OrderId: string): Promise<any> {
        let headers = new Headers({ 'Content-Type': 'application/json',
            'Authentication': JWT});
        let options = new RequestOptions({ headers: headers });

        try {
            let res = await this.http.post(this.UrlToken,  { StripeToken : StripToken, Charge : Charge, UserId : Uid,  OrderId: OrderId}, options).toPromise();
            console.log(res);
            return res.json();
        } catch (ex) {
            console.log(ex);
            this.handleError(ex);
        }

        // send order to server
        this.clearCart();
    }

    private  UrlQueue = 'https://6k1n8i5jx5.execute-api.us-east-1.amazonaws.com/prod/queue/';

    async checkoutQueue(queueId: String): Promise<any> {
        let headers = new Headers({'Content-Type': 'application/json'});
        let options = new RequestOptions({headers: headers});
        try {
            let res = await this.http.get(this.UrlQueue+queueId, options).toPromise();
            console.log(res);
            return res.json();
        } catch (ex) {
            // console.log(ex);
            // this.handleError(ex);
            return false;
            // let res = await this.http.get(this.UrlQueue+queueId, options).toPromise();
            // console.log(res);
            // return res.json();
        }
    }

    private UrlOrder = 'https://6k1n8i5jx5.execute-api.us-east-1.amazonaws.com/prod/orders';
    async sendOrderToServer(JWT: string, uid: string, Charge: number): Promise<any> {
        const headers = new Headers({ 'Content-Type': 'application/json',
            'Authentication': JWT});
        const options = new RequestOptions({ headers: headers });
        try{
            let payl = { Products : this.myCart.getInfo(), UserId: uid, TotalCharge : Charge };
            console.log(payl);
            const res = await this.http.post(this.UrlOrder,
                payl, options).toPromise();
            // console.log(res);
            this.clearCart();
            return true;
        } catch (ex) {
            console.log(ex);
            this.handleError(ex);
            return false;
        }
    }

    async getOrdersFromServer(JWT: string, uid: string): Promise<any> {
        const headers = new Headers({ 'Content-Type': 'application/json',
            'Authentication': JWT});
        const options = new RequestOptions({ headers: headers });

        let params: URLSearchParams = new URLSearchParams();
        params.set('operator', 'LT');
        params.set('field', 'id');
        params.set('start', '0');
        params.set('count', '100');
        params.set('value', '1000');

        // options.search = params;

        try {
            const res = await this.http.get(this.UrlOrder, options).toPromise();
            console.log(res)
            return res.json().Payload;
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
