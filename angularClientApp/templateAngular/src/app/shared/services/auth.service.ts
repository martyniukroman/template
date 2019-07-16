import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {appConfig} from '../config';
import {catchError, tap} from 'rxjs/operators';
import {Observable, of} from 'rxjs';
import {BaseComponent} from '../base.component';
import {Router} from '@angular/router';
import {json} from 'express';
import {ErrorResponseModel} from '../models/ErrorResponseModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseComponent {

  constructor(private _httpClient: HttpClient, private _router: Router) {
    super();
  }

  public Login(data: any) {
    // let promise = this._httpClient.post<any>(appConfig.BaseApiUri + 'auth/login', data).toPromise();
    // promise.catch(error => {
    //   console.log('Login Error: ', error);
    //   this.ErrorNotification(error.error.message);
    // });
    // promise.then( response => {
    //   console.log('Login: ', response);
    // });
    // return promise;

    return this._httpClient.post<any>(appConfig.BaseApiUri + 'auth/login', data);

  }

  public Register(data: any) {

    let promise = this._httpClient.post<any>(appConfig.BaseApiUri + 'accaunts', data).toPromise();

    let mitka: boolean = false;
    let errorModel: any = null;

    promise.catch(error => {
      console.log('Register Error: ', error);

      if (!error.ok) {
        mitka = true;
        errorModel = new ErrorResponseModel({
          isOk: error.ok,
          apiUrl: error.url,
          statusCode: error.status,
          statusText: error.statusText,
          httpMessage: error.message,
          coreMessage: error.error.message
        });
      }

      this.ErrorNotification(errorModel.coreMessage);
    });
    promise.then( response => {
      console.log('Register : ', response);
    });

    // if (mitka) return errorModel;
    return promise;
  }

  Logout() {
    localStorage.removeItem('token');
    this._router.navigate(['/login']);
  }

}
