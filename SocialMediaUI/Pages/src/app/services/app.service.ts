import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class AppService {
  public userId = new BehaviorSubject('');

  constructor(private http: HttpClient) {}
}
