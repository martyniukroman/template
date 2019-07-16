import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {Observable, throwError} from 'rxjs';
import {catchError, map} from 'rxjs/operators';
import notify from 'devextreme/ui/notify';
import {BaseComponent} from '../base.component';

@Injectable()
export class TokenInterceptorService implements HttpInterceptor {

  constructor(private _router: Router) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = localStorage.getItem('token');

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
        if (event instanceof HttpResponse) {
          console.log('event--->>>', event);
        }
        return event;
      }),
      catchError((error: HttpErrorResponse) => {
        console.log('error--->>>', error);

        let errorString = '';

        if (!error.ok){

          errorString += 'Status: ' + error.status + ' ' + error.statusText + ' Details: ';
          errorString += error.error.message;

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
    }, 'error', 3000);
  }


}
