import { Component, OnInit } from '@angular/core';
import {User} from '../User';
import {Router} from '@angular/router';
import {UserService} from '../user.service';
import 'rxjs/add/operator/toPromise';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

    originalUser: User;
    user: User;
    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
    ) { }

  ngOnInit() {
      this.originalUser = this.userService.getUser();
      this.user = new User();
      this.user.uid = this.originalUser.uid;
      this.user.firstname = this.originalUser.firstname;
      this.user.lastname = this.originalUser.lastname;
      this.user.email = this.originalUser.email;
      this.user.phone = this.originalUser.phone;
      this.user.JWT = this.originalUser.JWT;

      // if usr log in, redirect to welcome page
      if (this.user === undefined) {
          this.forward('/welcome');
      }else {
          console.log('please modify');
      }
  }
    forward(dest: string) {
        this.router.navigate([dest]);
    }

    async onSubmit(): Promise<any> {
        console.log('modify submitted');
        this.message = 'Loading';
        const change = {};

        console.log(this.user);
        console.log(this.originalUser);
        if (this.user.firstname !== this.originalUser.firstname) {
            change['FirstName'] = this.user.firstname;
        }
        if (this.user.lastname !== this.originalUser.lastname) {
            change['LastName'] = this.user.lastname;
        }
        if (this.user.password !== this.originalUser.password) {
            change['Password'] = this.user.password;
        }
        if (this.user.email !== this.originalUser.email) {
            change['Email'] = this.user.email;
        }
        if (this.user.phone !== this.originalUser.phone) {
            change['PhoneNumber'] = this.user.phone;

        }
        console.log(change);
        try {
            const modifyResult = await this.userService.modifyUserInfoRemote(this.user, change);
            console.log(modifyResult);
            if (modifyResult) {

                this.message = 'modify success';
                this.userService.user.firstname = this.user.firstname;
                this.userService.user.lastname = this.user.lastname;
                this.userService.user.phone = this.user.phone;

                console.log('modify success');
            } else {
                console.log(modifyResult);
                console.log('modify fail');
            }
        } catch (ex) {
            console.error('An error occurred', ex);
        }
    }



}
