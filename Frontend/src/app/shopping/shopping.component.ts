import { Component, OnInit } from '@angular/core';
import {Item} from "../Item";
import {ItemService} from "../item.service";
import {UserService} from "../user.service";
import {Router} from "@angular/router";
import {CartService} from "../cart.service";


@Component({
  selector: 'app-shopping',
  templateUrl: './shopping.component.html',
  styleUrls: ['./shopping.component.css']
})
export class ShoppingComponent implements OnInit {


    items: Item[];
    cartNums: number[];
    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
        private itemService: ItemService,
        private cartService: CartService,
    ) { }

    async ngOnInit() : Promise<any> {

        // get items from server
        try {
            let itemResult = await this.itemService.getItemsRemote();
            this.items = itemResult.Payload;

            console.log('get items success');
        } catch (ex) {
            console.error('An error occurred', ex);
        }
        // initial number to be added
        this.cartNums = [];
        for (let item in this.items) {
            this.cartNums.push(1)
        }

    }



    addToCart(item:Item, num: number): void{
        // prevent un-login user get in
        if (this.userService.getUser() == undefined){
            this.router.navigate(['/login']);
            console.log("you should not be here")
        }else {
            this.message = String(num) + " " + item.Name;
            this.cartService.addToCart(item, num);
        }
    }

    decimalHandler(event) {
        if (event.key === '.') {
            console.log(event);
            event.preventDefault();
        }

    }
}
