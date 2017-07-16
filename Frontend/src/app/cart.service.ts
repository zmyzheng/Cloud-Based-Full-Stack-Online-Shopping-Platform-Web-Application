import { Injectable } from '@angular/core';
import { Cart } from "./Cart";
import {Item} from "./Item";

@Injectable()
export class CartService {

    myCart: Cart;

    constructor() {
        console.log("initial cart");
        this.initialCart();
    }

    initialCart():void {
        // todo get cart from server
        this.myCart = new Cart();
    }

    addToCart(item:Item, num:number): void {
        // todo send to server
        this.myCart.addToLocalCart(item, num);
        console.log(this.myCart)
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


    getCartTotalPrice():number  {
        return this.myCart.getTotalPrice();
    }


    clearCart() {
        this.myCart = new Cart();
    }

    checkoutCart() {
        // send order to server
        this.clearCart();
    }
}
