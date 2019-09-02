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

  // User related properties
  public loginStatus = new BehaviorSubject<boolean>(this.checkLoginStatus());
  public UserName = new BehaviorSubject<string>(localStorage.getItem('username'));
  public UserDisplayName = new BehaviorSubject<string>(localStorage.getItem('displayName'));
  public UserRole = new BehaviorSubject<string>(localStorage.getItem('userRole'));


  // Register Method
  Register(data) {
    return this.http.post<any>(appConfig.BaseApiUrl + 'accaunt/register', data).subscribe(x => {
      if (x.email && x.status == 1) {
        this.SuccessNotification('Your account successfully created');
        this.router.navigateByUrl('/auth/signin');
      } else {
        this.ErrorNotification('error');
      }

    });
  }

  // Method to get new refresh token
  GetNewRefreshToken(): Observable<any> {
    let username = localStorage.getItem('username');
    let refreshToken = localStorage.getItem('refreshToken');
    const grantType = "refresh_token";

    return this.http.post<any>(appConfig.BaseApiUrl + 'token/login',
      {
        UserName: username,
        RefreshToken: refreshToken,
        GrantType: grantType
      });

  }

  //Login Method
  Login(username: string, password: string) {
    const grantType = 'password';
    // pipe() let you combine multiple functions into a single function.
    // pipe() runs the composed functions in sequence.
    return this.http.post<any>(appConfig.BaseApiUrl + 'token/login', {
      UserName: username,
      Password: password,
      GrantType: grantType,
    }).subscribe(x => {
      console.log('auth');
      console.log(x);
      if (x && x.authToken.token) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('loginStatus', '1');
        localStorage.setItem('displayName', x.authToken.displayName);
        localStorage.setItem('userId', x.authToken.userId);
        localStorage.setItem('jwt', x.authToken.token);
        localStorage.setItem('username', x.authToken.username);
        localStorage.setItem('expiration', x.authToken.expiration);
        localStorage.setItem('userRole', x.authToken.roles);
        localStorage.setItem('refreshToken', x.authToken.refresh_token);
        this.UserName.next(localStorage.getItem('username'));
        this.UserDisplayName.next(localStorage.getItem('displayName'));
        this.UserRole.next(localStorage.getItem('userRole'));
        this.loginStatus.next(true);
        this.router.navigateByUrl('/home');
        this.SuccessNotification('Welcome back ' + localStorage.getItem('displayName'));
      }

    });
  }

  Logout() {
    // Set Loginstatus to false and delete saved jwt cookie
    localStorage.removeItem('jwt');
    localStorage.removeItem('userRole');
    localStorage.removeItem('displayName');
    localStorage.removeItem('userId');
    localStorage.removeItem('username');
    localStorage.removeItem('expiration');
    localStorage.setItem('loginStatus', '0');
    console.log("Logged Out Successfully");
    this.router.navigate(['/login']);
    this.loginStatus.next(false);
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
    this.UserName.next(localStorage.getItem('username'));
    return this.UserName.asObservable();
  }

  get currentUserDisplayName() {
    this.UserDisplayName.next(localStorage.getItem('displayName'));
    return this.UserDisplayName.asObservable();
  }

  get currentUserRole() {
    this.UserRole.next(localStorage.getItem('userRole'));
    return this.UserRole.asObservable();
  }

}
