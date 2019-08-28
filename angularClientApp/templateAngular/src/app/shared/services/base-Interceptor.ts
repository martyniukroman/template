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


  private tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
  private isTokenRefreshing: boolean = false;

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
        let errorString = '';

        if (!error.ok) {

          console.log('error->');
          console.log(error);
          if (error.error) {
            errorString += ' ' + error.error;
          }

          if (error.status == 401) {
            errorString = 'Token lifetime is expired you are forced to refresh your page to get a new AccessToken, this issue should be fixed by developer in future';
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
              });
            } else {
              this.isTokenRefreshing = false;
            }
          }
          if (error.status == 0) {
            errorString = 'No connection';
          }

          this.RaiseErrorMessage(errorString);

        }
        return throwError(error);
      }));

  }

  private RaiseErrorMessage(text: string) {
    notify({
      message: text,
      closeOnClick: true,
      shading: true
    }, 'error', 10000);
  }


}
