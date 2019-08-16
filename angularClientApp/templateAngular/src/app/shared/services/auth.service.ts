import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {appConfig} from '../config';
import {catchError, tap} from 'rxjs/operators';
import {Observable, of} from 'rxjs';
import {BaseComponent} from '../base.component';
import {Router} from '@angular/router';
import {json} from 'express';
import {ErrorResponseModel} from '../models/ErrorResponseModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseComponent {

  constructor(private _httpClient: HttpClient, private _router: Router) {
    super();
  }

  public Login(data: any) {
    return this._httpClient.post<any>(appConfig.BaseApiUri + 'token/login', {
      UserName: data.userName,
      Password: data.password,
      GrantType: "password",
    });
  }

  public Register(data: any) {
    return this._httpClient.post<any>(appConfig.BaseApiUri + 'accaunts', data);
  }

  Logout() {
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('access_token');
    this._router.navigate(['/login']);
  }

}
// public string GrantType { get; set; }
// public string ClientId { get; set; }
// public string UserName { get; set; }
// public string RefreshToken { get; set; }
// public string Password { get; set; }
