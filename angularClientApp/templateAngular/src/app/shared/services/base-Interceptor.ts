import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
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

        if (!error.ok){

          console.log('error->');
          console.log(error);
          errorString += error.status;
          if (error.error){
            errorString += ' ' + error.error;
          }

          if (error.status == 401){
            this.RefreshTokenHandler(req, next);
            this._authService.Logout();
            this._router.navigateByUrl('/auth/signin');
          }
          if (error.status == 0){
            errorString = 'No connection';
          }

          this.RaiseErrorMessage(errorString);

        }
        return throwError(error);
      }));

  }

  private RefreshTokenHandler(request: HttpRequest<any>, next: HttpHandler) {

    // First thing to check if the token is in process of refreshing
    if (!this.isTokenRefreshing)  // If the Token Refresheing is not true
    {
      this.isTokenRefreshing = true;

      // Any existing value is set to null
      // Reset here so that the following requests wait until the token comes back from the refresh token API call
      this.tokenSubject.next(null);

      /// call the API to refresh the token
      return this._authService.GetNewRefreshToken().pipe(
        switchMap((tokenresponse: any) => {
          if (tokenresponse) {
            this.tokenSubject.next(tokenresponse.authToken.token);
            localStorage.setItem('loginStatus', '1');
            localStorage.setItem('jwt', tokenresponse.authToken.token);
            localStorage.setItem('username', tokenresponse.authToken.username);
            localStorage.setItem('expiration', tokenresponse.authToken.expiration);
            localStorage.setItem('userRole', tokenresponse.authToken.roles);
            localStorage.setItem('refreshToken', tokenresponse.authToken.refresh_token);
            console.log("Token refreshed...");
          }
          return <any>this._authService.Logout();
        }),
        catchError(err => {
          this._authService.Logout();
          return throwError(err);
        }),
        finalize(() => {
          this.isTokenRefreshing = false;
        })
      );
    } else {
      this.isTokenRefreshing = false;
    }

  }

  private RaiseErrorMessage(text: string) {
    notify({
      message: text,
      closeOnClick: true,
      shading: false
    }, 'error', 3000);
  }


}
