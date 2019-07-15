import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {appConfig} from '../config';
import {catchError, tap} from 'rxjs/operators';
import {Observable, of} from 'rxjs';
import {BaseComponent} from '../base.component';
import {Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseComponent {

  constructor(private _httpClient: HttpClient, private _router: Router) {
    super();
  }

  public Login(data: any) {
    return this._httpClient.post<any>(appConfig.BaseApiUri + 'auth/login', data).pipe(
      tap(_ => console.log('login')),
      catchError(this.handleError('Login', []))
    );
  }

  public Register(data: any): Observable<any> {
    return this._httpClient.post<any>(appConfig.BaseApiUri + 'accaunts', data)
      .pipe(
        tap(_ => console.log('login')),
        catchError(this.handleError('Register', []))
      );
  }
  Logout() {
    localStorage.removeItem('token');
    this._router.navigate(['/login']);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      this.ErrorNotification(error);
      this.ErrorNotification(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

}
