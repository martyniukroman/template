import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from "rxjs";
import {BaseService} from "./base.service";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {appConfig} from "../config";
import {UserRegistrationModel} from '../models/UserRegistrationModel';
import notify from 'devextreme/ui/notify';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService extends BaseService {

  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  private loggedIn = false;

  public baseUrl: string = 'https://localhost:5001/api/';
  public authNavStatus$ = this._authNavStatusSource.asObservable();

  private headers = appConfig.DefaultHeaders;

  constructor(private http: HttpClient) {
    super();

    this.loggedIn = !!localStorage.getItem('auth_token');
    this._authNavStatusSource.next(this.loggedIn);
    this.baseUrl = appConfig.BaseApiUri;
  }

  public Register(email, password, firstName, lastName, location) {

    let body = JSON.stringify({email, password, firstName, lastName, location});
    let options = {
      headers: this.headers
    };

    let response;
    this.http.post<UserRegistrationModel>(this.baseUrl + 'accounts', body, options).subscribe((x: any) => {
        response = x;
      },
      error => {
        console.log(error);
        notify(error.toString(), 'error', 2000);
      }
    );

    return response;
  }

  Login(userName, password) {

    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');

    return this.http
      .post(
        this.baseUrl + 'auth/login',
        JSON.stringify({userName, password}), {headers}
      )
      .subscribe((res: any) => {
          localStorage.setItem('auth_token', res.auth_token);
          this.loggedIn = true;
          this._authNavStatusSource.next(true);
          return true;
        },
        error => {
          console.log(error);
        }
      );

  }

  Logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn() {
    return this.loggedIn;
  }

  // FacebookLogin(accessToken:string) {
  //   let headers = new HttpHeaders();
  //   headers.append('Content-Type', 'application/json');
  //   let body = JSON.stringify({ accessToken });
  //   return this.http
  //     .post(
  //       this.baseUrl + '/externalauth/facebook', body, { headers })
  //     .map(res => res.json())
  //     .map(res => {
  //       localStorage.setItem('auth_token', res.auth_token);
  //       this.loggedIn = true;
  //       this._authNavStatusSource.next(true);
  //       return true;
  //     })
  //     .catch(this.handleError);
  // }

}
