import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { UserLoginComponent } from './user-login/user-login.component';
import {UserService} from './user.service';
import { WelcomeComponent } from './welcome/welcome.component';
import { RegisterComponent } from './register/register.component';
import {ItemService} from './item.service';
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { ShoppingComponent } from './shopping/shopping.component';
import { FooterComponent } from './footer/footer.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserOrdersComponent } from './user-orders/user-orders.component';
import { UserCartComponent } from './user-cart/user-cart.component';
import {CartService} from './cart.service';


@NgModule({
    declarations: [
        AppComponent,
        UserLoginComponent,
        WelcomeComponent,
        RegisterComponent,
        NavigationBarComponent,
        ShoppingComponent,
        FooterComponent,
        UserProfileComponent,
        UserOrdersComponent,
        UserCartComponent,
        UserCartComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        AppRoutingModule,
    ],
    providers: [UserService, ItemService, CartService],
    bootstrap: [AppComponent]
})


export class AppModule { }
