import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse
} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {BehaviorSubject, Observable, throwError} from 'rxjs';
import {catchError, finalize, map, switchMap} from 'rxjs/operators';
import notify from 'devextreme/ui/notify';
import {BaseComponent} from '../base.component';
import {AuthService} from './auth.service';

@Injectable()
export class BaseInterceptor implements HttpInterceptor {

//TODO: move to baseComponent
  private tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
  private isTokenRefreshing: boolean = false;
  private errorString: string = '';

  constructor(private _router: Router, private _authService: AuthService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = localStorage.getItem('jwt');

    if (token) {
      req = req.clone({
        setHeaders: {
          'Authorization': 'Bearer ' + token
        }
      });
    }

    if (!req.headers.has('Content-Type')) {
      req = req.clone({
        setHeaders: {
          'Content-Type': 'application/json'
        }
      });
    }

    req = req.clone({
      headers: req.headers.set('Accept', 'application/json')
    });

    return next.handle(req).pipe(
      map((event: HttpEvent<any>) => {
        return event;
      }),
      catchError((error: HttpErrorResponse) => {

          this.errorString = '';

          if (!error.ok) {

            console.log('error->');
            console.log(error);

            if (error.status) {
              if (error.error) {
                if (error.error.caption) {
                  this.errorString += ' ' + error.error.caption;
                }
                else {
                  this.errorString += ' ' + error.error;
                }
                if (error.error.afterAction == 'relogin') {
                  this._authService.Logout();
                  this._router.navigateByUrl('/auth/signin');
                }
              }
              if (error.status == 401) {
                this.setTokenResponse();
              }
            }

            if (this.errorString)
              this.RaiseErrorMessage(this.errorString);

          }
          return throwError(error);
        }
      ));

  }

  private RaiseErrorMessage(text: string) {
    notify({
      message: text,
      closeOnClick: true,
      shading: true
    }, 'error', 3000);
    this.errorString = null;
  }

  private setTokenResponse(): void {
    if (!this.isTokenRefreshing) {
      this.isTokenRefreshing = true;
      this.tokenSubject.next(null);
      this._authService.GetNewRefreshToken().subscribe(result => {
        console.log(result);
        if (result && result.authToken.token) {
          localStorage.setItem('loginStatus', '1');
          localStorage.setItem('jwt', result.authToken.token);
          localStorage.setItem('username', result.authToken.username);
          localStorage.setItem('expiration', result.authToken.expiration);
          localStorage.setItem('userRole', result.authToken.roles);
          localStorage.setItem('refreshToken', result.authToken.refresh_token);
          localStorage.setItem('displayName', result.authToken.displayName);
          localStorage.setItem('userId', result.authToken.userId);
        }
        location.reload();
      });
    } else {
      this.isTokenRefreshing = false;
    }
  }


}
