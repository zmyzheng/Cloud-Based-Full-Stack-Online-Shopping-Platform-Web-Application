import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import {UserService} from "../user.service";
import {User} from "../User";
import {CartService} from "../cart.service";

@Component({
    selector: 'app-navigation-bar',
    templateUrl: './navigation-bar.component.html',
    styleUrls: ['./navigation-bar.component.css']
})
export class NavigationBarComponent implements OnInit {
    user: User;
    message: string;
    loginFlag: boolean = false;

    constructor(
        private router: Router,
        private userService: UserService,
        private cartService: CartService,
    ) { }

    ngOnInit() {
        this.user = new User();
        // if user log in, redirect to welcome page
        if (this.userService.getUser() === undefined){
            this.loginFlag = false;
        }else {
            this.user = this.userService.getUser();
            this.loginFlag = true;
            this.message = "welcome " + this.user.firstname
        }
    }

    onRegister():void {
        console.log('going to register');
        this.router.navigate(['/register']);
    }

    onLogIn(): void {
        console.log('going to log in');
        this.router.navigate(['/login']);
    }

    onSignOut(): void {
        console.log('going to sign out');
        this.userService.getOffUser();
        this.cartService.clearCart();
        this.loginFlag = false;
        this.message ="Success log out";
        this.router.navigate(['/login']);
    }
}
