import { Component, OnInit } from '@angular/core';
import {CartService} from '../cart.service';
import {Item} from '../Item';
import {UserService} from '../user.service';
import {Router} from '@angular/router';



@Component({
  selector: 'app-user-cart',
  templateUrl: './user-cart.component.html',
  styleUrls: ['./user-cart.component.css']
})
export class UserCartComponent implements OnInit {
    items: Item[];
    nums: number[];
    keys: string[];
    myParseFloat = parseFloat;
    total: number;
    loading: boolean;

    constructor(
        private userService: UserService,
        private router: Router,
        private cartService: CartService,
    ) { }

    ngOnInit() {
        this.loading = false;
        // prevent unlog usr get in
        if (this.userService.getUser() == undefined){
            this.router.navigate(['/login']);
            console.log('you should not be here');
        }
        this.getCartContent();
    }

    decimalHandler(event) {
        if (event.key === '.') {
            console.log(event);
            event.preventDefault();
            return;
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

    async placeOrder(): Promise<any> {
        this.loading =true;
        console.log('pay start');
        try {
            const sendResult = await this.cartService.sendOrderToServer(
                this.userService.getUser().JWT,
                this.userService.getUser().uid,
                this.total * 100);
            if (sendResult) {
                this.router.navigate(['/orders']);
            }
        } catch (ex) {
            console.error('An error occurred', ex);
        }
        this.loading = false;
    }


    deleteAnItem(index: number): void {
        this.keys.splice(index, 1);
        this.items.splice(index, 1);
        this.nums.splice(index, 1);
        this.updateCartContent();
    }


}
