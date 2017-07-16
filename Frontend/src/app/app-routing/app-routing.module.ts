import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import {UserLoginComponent} from "../user-login/user-login.component";
import {WelcomeComponent} from "../welcome/welcome.component";
import {RegisterComponent} from "../register/register.component";
import {ShoppingComponent} from "../shopping/shopping.component";
import {UserProfileComponent} from "../user-profile/user-profile.component";
import {UserOrdersComponent} from "../user-orders/user-orders.component";
import {UserCartComponent} from "../user-cart/user-cart.component";


const routes: Routes = [
    { path: '', redirectTo: '/welcome', pathMatch: 'full' },
    { path: 'login',  component: UserLoginComponent },
    { path: 'welcome',  component: WelcomeComponent },
    { path: 'register',  component: RegisterComponent },
    { path: 'shopping',  component: ShoppingComponent },
    { path: 'profile',  component: UserProfileComponent },
    { path: 'orders',  component: UserOrdersComponent },
    { path: 'cart',  component: UserCartComponent },
];


@NgModule({
    imports: [
        CommonModule,
        RouterModule.forRoot(routes)
    ],
    exports:[
        RouterModule,
    ],
    declarations: []
})
export class AppRoutingModule { }
