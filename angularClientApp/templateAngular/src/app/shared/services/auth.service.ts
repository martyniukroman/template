import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {appConfig} from '../config';
import {catchError, map, tap} from 'rxjs/operators';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {BaseComponent} from '../base.component';
import {Router} from '@angular/router';
import {json} from 'express';
import {ErrorResponseModel} from '../models/ErrorResponseModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseComponent {
  // Need HttpClient to communicate over HTTP with Web API

  constructor(private http: HttpClient, private router: Router) {
    super();
  }

  // Url to access our Web API’s
  // private baseUrlLogin: string = "/api/account/login";
  // private baseUrlRegister: string = "/api/account/register";
  // // Token Controller
  // private baseUrlToken: string = "/api/token/auth";


  // User related properties
  private loginStatus = new BehaviorSubject<boolean>(this.checkLoginStatus());
  private UserName = new BehaviorSubject<string>(localStorage.getItem('username'));
  private UserRole = new BehaviorSubject<string>(localStorage.getItem('userRole'));


  // Register Method
  Register(data) {
    return this.http.post<any>(appConfig.BaseApiUrl + 'accaunt/register', data).pipe(map(result => {
      //registration was successful
      return result;
    }, error => {
      return error;
    }));
  }

  // Method to get new refresh token
  GetNewRefreshToken(): Observable<any> {
    let username = localStorage.getItem('username');
    let refreshToken = localStorage.getItem('refreshToken');
    const grantType = "refresh_token";

    return this.http.post<any>(appConfig.BaseApiUrl + 'token/login', {username, refreshToken, grantType}).pipe(
      map(result => {
        if (result && result.authToken.token) {
          this.loginStatus.next(true);
          localStorage.setItem('loginStatus', '1');
          localStorage.setItem('jwt', result.authToken.token);
          localStorage.setItem('username', result.authToken.username);
          localStorage.setItem('expiration', result.authToken.expiration);
          localStorage.setItem('userRole', result.authToken.roles);
          localStorage.setItem('refreshToken', result.authToken.refresh_token);
        }

        return <any>result;

      })
    );

  }


  //Login Method
  Login(username: string, password: string) {
    const grantType = "password";
    // pipe() let you combine multiple functions into a single function.
    // pipe() runs the composed functions in sequence.
    return this.http.post<any>(appConfig.BaseApiUrl + 'token/login', {username, password, grantType}).pipe(
      map(result => {

        // login successful if there's a jwt token in the response
        if (result && result.authToken.token) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes

          this.loginStatus.next(true);
          localStorage.setItem('loginStatus', '1');
          localStorage.setItem('jwt', result.authToken.token);
          localStorage.setItem('username', result.authToken.username);
          localStorage.setItem('expiration', result.authToken.expiration);
          localStorage.setItem('userRole', result.authToken.roles);
          localStorage.setItem('refreshToken', result.authToken.refresh_token);
          this.UserName.next(localStorage.getItem('username'));
          this.UserRole.next(localStorage.getItem('userRole'));

        }

        return result;

      })
    );
  }

  Logout() {
    // Set Loginstatus to false and delete saved jwt cookie
    this.loginStatus.next(false);
    localStorage.removeItem('jwt');
    localStorage.removeItem('userRole');
    localStorage.removeItem('username');
    localStorage.removeItem('expiration');
    localStorage.setItem('loginStatus', '0');
    this.router.navigate(['/login']);
    console.log("Logged Out Successfully");
  }

  checkLoginStatus(): boolean {

    var loginCookie = localStorage.getItem("loginStatus");

    if (loginCookie == "1") {
      if (localStorage.getItem('jwt') != null || localStorage.getItem('jwt') != undefined) {
        return true;
      }
    }
    return false;
  }

  get isLoggesIn() {
    return this.loginStatus.asObservable();
  }

  get currentUserName() {
    return this.UserName.asObservable();
  }

  get currentUserRole() {
    return this.UserRole.asObservable();
  }

}
