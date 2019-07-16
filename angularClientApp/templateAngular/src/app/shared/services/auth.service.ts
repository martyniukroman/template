import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {appConfig} from '../config';
import {catchError, tap} from 'rxjs/operators';
import {Observable, of} from 'rxjs';
import {BaseComponent} from '../base.component';
import {Router} from '@angular/router';
import {json} from 'express';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseComponent {

  constructor(private _httpClient: HttpClient, private _router: Router) {
    super();
  }

  public Login(data: any) {
    return this._httpClient.post<any>(appConfig.BaseApiUri + 'auth/login', data)
  }

  public Register(data: any): Observable<any> {
    return this._httpClient.post<any>(appConfig.BaseApiUri + 'accaunts', data);
  }

  Logout() {
    localStorage.removeItem('token');
    this._router.navigate(['/login']);
  }

}
