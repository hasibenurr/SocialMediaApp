import { Component, Input, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { Post } from '../models/post.model';
import { HomeService } from '../services/home.service';
import { AppService } from '../services/app.service';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css'],
})
export class PostComponent implements OnInit {
  @Input() users: User[];
  public username?: string;
  public post: Post;
  public categoryName: string = '';
  accessToken: string = '';

  constructor(
    private homeService: HomeService,
    private appService: AppService,
    private router: Router
  ) {
    this.post = new Post();
    this.users = [];
  }

  ngOnInit(): void {
    debugger;
    this.authentication();

    this.post.userId = this.appService.userId.getValue();
    this.username = this.users.find((x) => x.id == this.post.userId)?.username;

    if (this.post.category == 1) {
      //daily
      this.post.style = 'text-secondary';
    } else if (this.post.category == 2) {
      // scientific, academic
      this.post.style = 'text-success';
    } else if (this.post.category == 3) {
      // general culture
      this.post.style = 'text-info';
    } else if (this.post.category == 4) {
      // political agenda
      this.post.style = 'text-danger';
    }
  }

  redirectMainPage() {
    this.router.navigate(['']);
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

  createNewPost() {
    debugger;
    if (
      this.post.userId &&
      this.post.title &&
      this.post.message &&
      this.post.category
    ) {
      let header = new HttpHeaders({
        Authorization: 'Bearer ' + this.accessToken,
      });
      this.homeService.createPost(this.post, header).subscribe({
        next: (result) => {
          this.router.navigate(['']);
          console.log('Post created!');
        },
        error: (response) => {
          console.log('Error is' + response);
        },
      });
    } else {
      alert('Please fill in the required information!');
    }
  }
}
