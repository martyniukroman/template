import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {Observable, throwError} from 'rxjs';
import {catchError, map} from 'rxjs/operators';
import notify from 'devextreme/ui/notify';
import {BaseComponent} from '../base.component';
import {AuthService} from './auth.service';

@Injectable()
export class TokenInterceptorService implements HttpInterceptor {

  constructor(private _router: Router, private _authService: AuthService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = localStorage.getItem('access_token');

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

    //return next.handle(req);

    return next.handle(req).pipe(
      map((event: HttpEvent<any>) => {
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        let errorString = '';

        if (!error.ok){

          console.log('error->');
          console.log(error);

          errorString += error.status + ' ' + error.statusText;

          if (error.error && error.error.message){
            errorString += ' ' + error.error.message;
          }

          if (error.status == 401){
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

  private RaiseErrorMessage(text: string) {
    notify({
      message: text,
      closeOnClick: true,
      shading: false
    }, 'error', 3000);
  }


}
