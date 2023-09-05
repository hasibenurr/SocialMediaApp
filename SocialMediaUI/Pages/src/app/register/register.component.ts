import { Component, Input, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  public loginUser: User;
  @Input() users: User[];

  constructor(private loginService: LoginService, private router: Router) {
    this.loginUser = new User();
    this.users = [];
  }

  createUser() {
    debugger;
    if (
      this.loginUser.name &&
      this.loginUser.surname &&
      this.loginUser.username &&
      this.loginUser.email &&
      this.loginUser.password
    ) {
      var checkUserExist = this.users.find(
        (x) =>
          x.username == this.loginUser.username &&
          x.password == this.loginUser.password
      );
      var checkUserNameExist = this.users.find(
        (x) => x.username == this.loginUser.username
      );
      if (checkUserExist) {
        alert('This user already exist!');
      } else if (checkUserNameExist) {
        alert('This Username is already used!');
      } else {
        // Create an user account
        this.loginService.createUser(this.loginUser).subscribe({
          next: (result) => {
            this.router.navigate(['home']);
            console.log('User created!');
          },
          error: (response) => {
            console.log('Error is' + response);
          },
        });
      }
    } else {
      alert('Please enter complete information!');
    }
  }
}
