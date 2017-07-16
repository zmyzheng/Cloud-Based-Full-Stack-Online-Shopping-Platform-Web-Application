import { Component, OnInit } from '@angular/core';
import {User} from "../User";
import {Router} from "@angular/router";
import {UserService} from "../user.service";
import 'rxjs/add/operator/toPromise';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

    regUser: User;
    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
    ) { }

  ngOnInit() {
      this.regUser = new User();
      // if usr log in, redirect to welcome page
      if (this.userService.getUser() == undefined){
          this.forward('/welcome');
      }else {
          console.log("please modify");
      }
  }
    forward(dest:string) {
        this.router.navigate([dest]);
    }

    async onSubmit() : Promise<any> {
        console.log("going to register");
        this.message = 'Loading';
        try {
            let resigterResult = await this.userService.registerUserRemote(this.regUser);
            console.log(resigterResult);
            if (resigterResult.result  === 'success') {
                this.regUser = new User();
                this.message ='register success';
                console.log('register success');
                this.forward('/login');
            } else {
                console.log(resigterResult);
                //this.message = response.reason;
                console.log('register fail');
            }
        } catch (ex) {
            console.error('An error occurred', ex);
        }
    }



}
