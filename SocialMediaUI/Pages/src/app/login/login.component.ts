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
  public allUser: User[] = [];

  constructor(private appService: AppService, private loginService: LoginService, private router: Router) {
    this.loginUser = new User();
  }

  ngOnInit(): void {
    this.loginService.getAllUsers().subscribe({
      next: (user) => {
        this.allUser = user;
        console.log('All users have been gotten!');
      },
      error: (response) => {
        console.log('Error is' + response);
      },
    });
  }

  validateLogin() {
    if (this.loginUser.username && this.loginUser.password) {
      var userId = this.allUser.find(
        (x) =>
          x.username == this.loginUser.username &&
          x.password == this.loginUser.password
      )?.id;
      var checkUserExist = this.allUser.find(
        (x) =>
          x.username == this.loginUser.username &&
          x.password == this.loginUser.password
      );
      debugger;
      if (userId != undefined && checkUserExist) {
        console.log("User Id: ", userId);
        this.appService.userId.next(userId);
        this.router.navigate(['/home']);
      } else if(userId != undefined && checkUserExist == undefined){
        alert('Wrong username or password!');
      }
      else{
        alert('This user does not exist!');
      }
    } else {
      alert('Please enter user name and password');
    }
  }

  redirectRegisterPage(){
    this.router.navigate(['/register']);
  }
}
