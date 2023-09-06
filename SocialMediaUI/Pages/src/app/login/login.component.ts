import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { AppService } from '../services/app.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [LoginService],
})
export class LoginComponent implements OnInit {
  public loginUser: User;
  public allUser!: User[];
  accessToken: string = '';

  constructor(
    private appService: AppService,
    private loginService: LoginService,
    private router: Router
  ) {
    this.loginUser = new User();
  }

  ngOnInit(): void {
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

  validateLogin() {
    if (this.loginUser.username && this.loginUser.password) {
      var user = this.allUser.find(
        (x) =>
          x.username == this.loginUser.username &&
          x.password == this.loginUser.password
      );
      var checkIsUserInfoCorrect = this.allUser.find(
        (x) =>
          x.username == this.loginUser.username ||
          x.password == this.loginUser.password
      );
      //debugger;
      if (user != undefined && user) {
        this.router.navigate(['/home', user?.id]);
      } else if (user == undefined && checkIsUserInfoCorrect) {
        alert('Wrong username or password!');
      } else {
        alert('This user does not exist!');
      }
    } else {
      alert('Please enter user name and password');
    }
  }

  redirectRegisterPage() {
    this.router.navigate(['/register']);
  }
}
