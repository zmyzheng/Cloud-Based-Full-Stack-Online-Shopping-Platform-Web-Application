import { Component, OnInit } from '@angular/core';
import {CartService} from "../cart.service";
import {Item} from "../Item";
import {UserService} from "../user.service";
import {Router} from "@angular/router";



@Component({
  selector: 'app-user-cart',
  templateUrl: './user-cart.component.html',
  styleUrls: ['./user-cart.component.css']
})
export class UserCartComponent implements OnInit {
    items:Item[];
    nums: number[];
    keys: string[];
    myParseFloat = parseFloat;
    total:number;
    myToken:string;

    constructor(
        private userService: UserService,
        private router: Router,
        private cartService: CartService,
    ) { }

    ngOnInit() {
        // prevent unlog usr get in
        if (this.userService.getUser() == undefined){
            this.router.navigate(['/login']);
            console.log("you should not be here");
        }
        this.getCartContent();
    }

    decimalHandler(event) {
        if (event.key === '.') {
            console.log(event);
            event.preventDefault();
            return
        }
    }

    getCartContent() {
        this.items = [];
        this.nums = [];
        this.keys = [];
        this.cartService.getCartContent(this.items, this.nums, this.keys);
        this.total = this.cartService.getCartTotalPrice();
    }

    updateCartContent() {
        this.cartService.updateCart(this.keys, this.items, this.nums);
        this.total = this.cartService.getCartTotalPrice();
    }

    openCheckout(id: string): void{
        let handler = (<any>window).StripeCheckout.configure({
            key: 'pk_test_XGmc8VOUVttNbHcEyQhodzwX',
            locale: 'auto',
            token: (token: any) => {
                console.log(token);
                this.myToken = token.id;
                // todo send to server
                // this.itemService.sendTokenToServer(this.myToken, this.user.JWT, id, price);
                this.cartService.checkoutCart();
                this.getCartContent();
                console.log('pay end.');
                this.router.navigate(['/shopping']);
            }
        });
        handler.open({
            name: 'Pay',
            description: "Your Order",
            amount: Number(this.total) * 100,
        });
        console.log('pay start');
    }

    deleteAnItem(index: number): void {
        this.keys.splice(index, 1);
        this.items.splice(index, 1);
        this.nums.splice(index, 1);
        this.updateCartContent();
    }


}
