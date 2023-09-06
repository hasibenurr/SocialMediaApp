import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { Token } from '../models/token.model';

@Injectable()
export class AppService {
  baseApiUrl: string = 'https://localhost:7245/';

  constructor(private http: HttpClient) {}

  getAccessToken(){
    return this.http.get<Token>(this.baseApiUrl + 'api/Identity');
  }

}
