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

  public async getData(path: string = 'values') {

    let promise = this.http.get<any>(appConfig.BaseApiUri + path).toPromise();
    promise.catch( error => {
      console.log('DataServiceProvider: ', error);
    });
    promise.then( response => {
      console.log('DataServiceProvider: ', response);
    });

    return promise;

  }

}
