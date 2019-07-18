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

  public async getDataPromise(path: string) : Promise<any> {
    return this.http.get<any>(appConfig.BaseApiUri + path).toPromise();
  }

  public getDataObservable(path: string) : Observable<any> {
    return this.http.get<any>(appConfig.BaseApiUri + path);
  }

  public async postData(path: string, body: any = null) {

    let promise = this.http.get<any>(appConfig.BaseApiUri + path, body).toPromise();
    promise.catch( error => {
      console.log('DataServiceProvider: ', error);
    });
    promise.then( response => {
      console.log('DataServiceProvider: ', response);
    });

    return promise;

  }

  public async putData(path: string, body: any = null) {

    let promise = this.http.put<any>(appConfig.BaseApiUri + path, body).toPromise();
    promise.catch( error => {
      console.log('DataServiceProvider: ', error);
    });
    promise.then( response => {
      console.log('DataServiceProvider: ', response);
    });

    return promise;

  }

  public async deleteData(path: string, body: any = null) {

    let promise = this.http.delete<any>(appConfig.BaseApiUri + path, body).toPromise();
    promise.catch( error => {
      console.log('DataServiceProvider: ', error);
    });
    promise.then( response => {
      console.log('DataServiceProvider: ', response);
    });

    return promise;

  }

}
