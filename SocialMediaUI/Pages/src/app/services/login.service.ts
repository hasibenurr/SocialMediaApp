import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable()
export class LoginService {
  baseApiUrl: string = 'https://localhost:7245/';

  constructor(private http: HttpClient) {}

  getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseApiUrl + 'api/User');
  }

  createUser(newUser: User): Observable<User> {
    return this.http.post<User>(this.baseApiUrl + 'api/User', newUser);
  }

}
