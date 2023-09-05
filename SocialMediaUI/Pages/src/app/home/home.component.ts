import { Component, Input, OnInit } from '@angular/core';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { Post } from '../models/post.model';
import { HomeService } from '../services/home.service';
import { LoginService } from '../services/login.service';
import { AppService } from '../services/app.service';
//import { HomeService } from './home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  @Input() users: User[];
  public user: User;
  public username?: string;
  public posts: Post[] = [
    // {
    //   id: '',
    //   userId: '',
    //   title: 'This is a wider card',
    //   message:
    //     'This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer',
    //   category: 1,
    // },
    // {
    //   id: '',
    //   userId: '',
    //   title: 'This is a standart card',
    //   message:
    //     'This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer',
    //   category: 2,
    // },
    // {
    //   id: '',
    //   userId: '',
    //   title: 'This is a sports card',
    //   message:
    //     'This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer',
    //   category: 3,
    // },
  ];

  constructor(
    private homeService: HomeService,
    private appService: AppService,
    private router: Router
  ) {
    this.user = new User();
    this.users = [];
  }

  ngOnInit(): void {
    debugger;
    this.user.id = this.appService.userId.getValue();
    this.username = this.users.find((x) => x.id == this.user.id)?.username;

    //Post categories
    this.posts.forEach((post) => {
      if (post.category == 0) {
        post.style = 'text-secondary';
      } else if (post.category == 1) {
        post.style = 'text-warning';
      } else if (post.category == 2) {
        post.style = 'text-danger';
      } else if (post.category == 3) {
        post.style = 'text-info';
      }
    });
  }

  redirectMainPage(){
    this.router.navigate(['']);
  }
}
