import { Component, OnInit } from '@angular/core';
import {User} from '../User';
import {UserService} from '../user.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})


export class UserLoginComponent implements OnInit {
    user: User;
    message: string;
    loading: boolean;

    constructor(
        private userService: UserService,
        private router: Router,
    ) { }


    ngOnInit() {
        this.user = new User();
        this.loading = false;
        // if usr log in, redirect to welcome page
        if (this.userService.getUser() === undefined) {
            console.log('please log in');
        }else {
            this.forward();
        }
    }

    async onSubmit(): Promise<any> {
        console.log('going to log in');
        this.loading = true;
        try {
            const loginResult = await this.userService.loginUserRemote(this.user);
            if (loginResult) {
                this.user = new User();
                this.message = 'login success';
                console.log('login success');
                this.forward();
            } else {
                console.log(loginResult);
                this.message = 'login fail';
                console.log('login fail');
            }
        } catch (ex) {
            console.error('An error occurred', ex);
        }
        this.loading = false;

    }

    forward() {
        this.router.navigate(['/welcome']);
    }


    private handleError(error) {
        console.error('Error processing action', error);
    }

}
