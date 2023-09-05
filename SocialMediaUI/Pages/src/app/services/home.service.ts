import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from '../models/post.model';

@Injectable()
export class HomeService {
  baseApiUrl: string = 'https://localhost:7245/';

  constructor(private http: HttpClient) {}

  getAllPosts(): Observable<Post[]> {
    return this.http.get<Post[]>(this.baseApiUrl + '/api/Post');
  }

  createPost(newPost: Post, headers:HttpHeaders): Observable<Post> {
    return this.http.post<Post>(this.baseApiUrl + 'api/Post', newPost, {headers: headers});
  }
}
