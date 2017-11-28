import { Component, OnInit } from '@angular/core';
import {User} from '../User';
import {Router} from '@angular/router';
import {UserService} from '../user.service';
import 'rxjs/add/operator/toPromise';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    regUser: User;
    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
    ) {}


    ngOnInit() {
        this.regUser = new User();
        // if usr log in, redirect to welcome page
        if (this.userService.getUser() === undefined) {
            console.log('please register');
        }else {
            this.forward('/welcome');
        }

    }

    forward(dest: string) {
        this.router.navigate([dest]);
    }

    async onSubmit(): Promise<any> {
        console.log('going to register');
        this.message = 'Loading';
        try {
            const resigterResult = await this.userService.registerUserRemote(this.regUser);
            console.log(resigterResult);
            if (resigterResult) {
                this.message = 'register success, please valid your email';
                console.log('register success');
            } else {
                this.message = 'register fail';
                console.log('register fail');
            }
        } catch (ex) {
            console.error('An error occurred', ex);
        }
    }


}
