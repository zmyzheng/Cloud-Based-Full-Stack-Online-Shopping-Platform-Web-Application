import { Injectable } from '@angular/core';
import {User} from "./User";
import { Headers, RequestOptions } from '@angular/http';
import { Http, Response }          from '@angular/http';
import 'rxjs/add/operator/toPromise';
import {USER_Login_Response1, USER_Reg_Response1, USER_Reg_Response2} from './mock-data/mock-user';
import { FacebookService, FacebookLoginResponse } from 'ng2-facebook-sdk';

@Injectable()
export class UserService {

    user: User;
    // fb: FacebookService;
    getUser(): User {
        return this.user;
    }

    getOffUser(): void {
        this.user = undefined;
    }

    private Urll = 'http://ec2-54-165-183-168.compute-1.amazonaws.com:3000/login';

    async  loginUserRemote(user:User): Promise<any> {
        let headers = new Headers({'Content-Type': 'application/json'});
        let options = new RequestOptions({ headers: headers });

        try {
            let res = await this.http.post(this.Urll, { PwdHash : user.password, Email : user.email }, options).toPromise();
            console.log(res);
            this.user = new User();
            this.user.JWT = res.json().payload.JWT;
            this.user.firstname = res.json().payload.firstname;
            this.user.lastname = res.json().payload.lastname;
            console.log(res.json());
            return res.json();
        } catch (ex) {
            this.handleError(ex);
        }
    }

    private Urlr = 'http://ec2-54-165-183-168.compute-1.amazonaws.com:3000/user';

    async registerUserRemote(user:User): Promise<any> {
        console.log("enter register");
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        try {
            let res = await this.http.post(this.Urlr, { FirstName: user.firstname, LastName: user.lastname,
                PwdHash : user.password, Email : user.email }, options)
                .toPromise();
            console.log(res.json());
            // this.user = new User();
            // this.user.JWT = res.json().payload.JWT;
            // this.user.firstname = res.json().payload.firstname;
            // this.user.lastname = res.json().payload.lastname;
            return res.json();
        } catch (ex) {
            console.log(ex);
            this.handleError(ex);
        }
    }

    facebooklogin() {
        this.fb.login()
            .then((res: FacebookLoginResponse) => {
                console.log('Logged in', res);
            })
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

    constructor (private http: Http, private fb: FacebookService,) {
        console.log('Initializing Facebook');

        this.fb.init({
            appId: '1927971220769787',
            version: 'v2.8'
        });
    }

}

