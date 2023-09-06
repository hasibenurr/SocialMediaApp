import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';
import { HttpHeaders } from '@angular/common/http';
import { AppService } from '../services/app.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  public loginUser: User;
  accessToken: string = '';
  allUser!: User[];

  constructor(
    private loginService: LoginService,
    private appService: AppService,
    private router: Router
  ) {
    this.loginUser = new User();
  }

  ngOnInit(): void {
    this.authentication();

    this.loginService.getAllUsers().subscribe({
      next: (users) => {
        this.allUser = users;
        console.log('All users have been gotten!', this.allUser);
      },
      error: (response) => {
        console.log('Error is' + response);
      },
    });
  }

  authentication() {
    this.appService.getAccessToken().subscribe({
      next: (token) => {
        this.accessToken = token.accessToken;
        console.log('Token: ', token);
      },
      error: (response) => {
        console.log('Error is' + response);
      },
    });
  }

  createUser() {
    //debugger;
    if (
      this.loginUser.name &&
      this.loginUser.surname &&
      this.loginUser.username &&
      this.loginUser.email &&
      this.loginUser.password
    ) {
      var checkUserExist = this.allUser.find(
        (x) =>
          x.username == this.loginUser.username &&
          x.password == this.loginUser.password
      );
      var checkUserNameExist = this.allUser.find(
        (x) => x.username == this.loginUser.username
      );
      if (checkUserExist) {
        alert('This user already exist!');
      } else if (checkUserNameExist) {
        alert('This Username is already used!');
      } else {
        // Create an user account
        let header = new HttpHeaders({
          Authorization: 'Bearer ' + this.accessToken,
        });
        this.loginService.createUser(this.loginUser, header).subscribe({
          next: (result) => {
            this.router.navigate(['']);
            alert('User created successfully!');
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

  redirectMainPage() {
    this.router.navigate(['']);
  }
}
