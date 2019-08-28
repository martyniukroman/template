import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {appConfig} from '../config';
import {catchError, tap} from 'rxjs/operators';
import {BaseComponent} from '../base.component';
import {Observable, of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataServiceProvider extends BaseComponent {

  constructor(private http: HttpClient) {
    super();
  }

  public async getDataPromise(path: string, data: any) : Promise<any> {
    return this.http.get<any>(appConfig.BaseApiUrl + path, ).toPromise();
  }

  public getDataObservable(path: string, data: any) : Observable<any> {
    return this.http.get<any>(appConfig.BaseApiUrl + path, data);
  }

  public async postData(path: string, body: any = null) {

    let promise = this.http.get<any>(appConfig.BaseApiUrl + path, body).toPromise();
    promise.catch( error => {
      console.log('DataServiceProvider: ', error);
    });
    promise.then( response => {
      console.log('DataServiceProvider: ', response);
    });

    return promise;

  }

  public async putData(path: string, body: any = null) {

    let promise = this.http.put<any>(appConfig.BaseApiUrl + path, body).toPromise();
    promise.catch( error => {
      console.log('DataServiceProvider: ', error);
    });
    promise.then( response => {
      console.log('DataServiceProvider: ', response);
    });

    return promise;

  }

  public async deleteData(path: string, body: any = null) {

    let promise = this.http.delete<any>(appConfig.BaseApiUrl + path, body).toPromise();
    promise.catch( error => {
      console.log('DataServiceProvider: ', error);
    });
    promise.then( response => {
      console.log('DataServiceProvider: ', response);
    });

    return promise;

  }

}
