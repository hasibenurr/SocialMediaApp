import { Component, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from '../models/post.model';
import { HomeService } from '../services/home.service';
import { HttpHeaders } from '@angular/common/http';
import { AppService } from '../services/app.service';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  public loggedUser: User;
  public posts: Post[] = [];
  public allUser: User[] = [];
  accessToken: string = '';
  loggedUsername: string = '';

  constructor(
    private homeService: HomeService,
    private appService: AppService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    this.loggedUser = new User();
  }

  ngOnInit(): void {
    this.authentication();

    // Get User Id from URL
    this.activatedRoute.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          this.homeService.getByUserId(id).subscribe({
            next: (user) => {
              this.loggedUser = user;
              this.posts = user.posts;
              this.setCategoryInfos(user.posts);
            },
          });
        }
      },
    });
  }

  redirectMainPage() {
    this.router.navigate(['']);
  }

  setCategoryInfos(usersPosts: Post[]) {
    usersPosts.forEach((post) => {
      if (post.category == 1) {
        //daily
        post.style = 'text-secondary';
      } else if (post.category == 2) {
        // scientific, academic
        post.style = 'text-success';
      } else if (post.category == 3) {
        // general culture
        post.style = 'text-info';
      } else if (post.category == 4) {
        // political agenda
        post.style = 'text-danger';
      }
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

  updateUser() {
    //debugger;
    if (
      this.loggedUser.name &&
      this.loggedUser.surname &&
      this.loggedUser.username &&
      this.loggedUser.email &&
      this.loggedUser.password
    ) {
      // Update an user account
      let header = new HttpHeaders({
        Authorization: 'Bearer ' + this.accessToken,
      });
      this.homeService
        .updateUser(this.loggedUser.id, this.loggedUser, header)
        .subscribe({
          next: (result) => {
            alert('User information updated!');
          },
          error: (response) => {
            console.log('Error is' + response);
          },
        });
    } else {
      alert('Please enter complete information!');
    }
  }

  deleteUser() {
    let header = new HttpHeaders({
      Authorization: 'Bearer ' + this.accessToken,
    });
    this.homeService
      .deleteUser(this.loggedUser.id, header)
      .subscribe({
        next: (result) => {
          console.log('User deleted!');
          this.router.navigate(['']);          
        },
        error: (response) => {
          console.log('Error is' + response);
        },
      });
  }
}
