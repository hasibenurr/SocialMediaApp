import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from '../models/post.model';
import { User } from '../models/user.model';

@Injectable()
export class HomeService {
  baseApiUrl: string = 'https://localhost:7245/';

  constructor(private http: HttpClient) {}

  getAllPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(this.baseApiUrl + 'api/Post');
  }

  getByUserId(id:string): Observable<User> {
    return this.http.get<User>(this.baseApiUrl + 'api/User/' + id);
  }

  createPost(newPost: Post, headers:HttpHeaders): Observable<Post> {
    return this.http.post<Post>(this.baseApiUrl + 'api/Post', newPost, {headers: headers});
  }

  updateUser(id:string, user: User, headers:HttpHeaders): Observable<User> {
    return this.http.put<User>(this.baseApiUrl + 'api/User/' + id, user, {headers: headers});
  }

  deleteUser(id:string, headers:HttpHeaders): Observable<User>  {
    return this.http.delete<User>(this.baseApiUrl + 'api/User/' + id, {headers: headers});
  }

  deletePost(id:string, headers:HttpHeaders): Observable<Post>  {
    return this.http.delete<Post>(this.baseApiUrl + 'api/Post/' + id, {headers: headers});
  }
}
