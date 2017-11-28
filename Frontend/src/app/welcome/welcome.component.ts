import { Component, OnInit } from '@angular/core';
import {UserService} from "../user.service";
import {User} from "../User";
import {Router} from "@angular/router";
import {Item} from "../Item";
import {ItemService} from "../item.service";

@Component({
    selector: 'app-welcome',
    templateUrl: './welcome.component.html',
    styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {
    items: Item[];
    message: string;

    constructor(
        private itemService: ItemService,
    ) { }

    async ngOnInit(): Promise<any> {
        // get items from server
        try {
            let itemResult = await this.itemService.getItemsRemote();
            this.items = itemResult;
            console.log(itemResult);
            console.log('get items success');
        } catch (ex) {
            console.error('An error occurred', ex);
        }

    }
}
