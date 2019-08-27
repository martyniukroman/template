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
import {catchError, filter, finalize, map, switchMap, take, tap} from 'rxjs/operators';
import notify from 'devextreme/ui/notify';
import {BaseComponent} from '../base.component';
import {AuthService} from './auth.service';

@Injectable()
export class TokenInterceptorService implements HttpInterceptor {

  private isTokenRefreshing: boolean = false;
  tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);

  constructor(private _router: Router, private _authService: AuthService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(this.attachTokenToRequest(request)).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          console.log("Success");
        }
      }),
      catchError((err): Observable<any> => {
        if (err instanceof HttpErrorResponse) {
          switch ((<HttpErrorResponse>err).status) {
            case 401:
              console.log("Token expired. Attempting refresh ...");
              return this.handleHttpResponseError(request, next);
            case 400:
              return <any>this._authService.Logout();
          }
        } else {
          return throwError(this.handleError);
        }
      })
    );

  }

  // Global error handler method
  private handleError(errorResponse: HttpErrorResponse) {
    let errorMsg: string;

    if (errorResponse.error instanceof Error) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMsg = "An error occured : " + errorResponse.error.message;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMsg = `Backend returned code ${errorResponse.status}, body was: ${errorResponse.error}`;
    }

    return throwError(errorMsg);
  }


  // Method to handle http error response
  private handleHttpResponseError(request: HttpRequest<any>, next: HttpHandler) {

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
            localStorage.setItem('acc', tokenresponse.authToken.token);
            localStorage.setItem('username', tokenresponse.authToken.username);
            localStorage.setItem('expiration', tokenresponse.authToken.expiration);
            localStorage.setItem('userRole', tokenresponse.authToken.roles);
            localStorage.setItem('refreshToken', tokenresponse.authToken.refresh_token);
            console.log("Token refreshed...");
            return next.handle(this.attachTokenToRequest(request));

          }
          return <any>this._authService.Logout();
        }),
        catchError(err => {
          this._authService.Logout();
          return this.handleError(err);
        }),
        finalize(() => {
          this.isTokenRefreshing = false;
        })
      );

    } else {
      this.isTokenRefreshing = false;
      return this.tokenSubject.pipe(filter(token => token != null),
        take(1),
        switchMap(token => {
          return next.handle(this.attachTokenToRequest(request));
        }));
    }


  }

  private attachTokenToRequest(request: HttpRequest<any>) {
    var token = localStorage.getItem('jwt');

    return request.clone({setHeaders: {Authorization: `Bearer ${token}`}});
  }

}
