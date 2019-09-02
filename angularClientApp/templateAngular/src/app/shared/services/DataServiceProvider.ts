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

  public async getDataPromise(path: string): Promise<any> {
    return this.http.get<any>(appConfig.BaseApiUrl + path).toPromise();
  }

  public getDataObservable(path: string): Observable<any> {
    return this.http.get<any>(appConfig.BaseApiUrl + path);
  }

  public postData(path: string, body: any) {
    return this.http.get<any>(appConfig.BaseApiUrl + path, body);
  }

  public putData(path: string, body: any) {
    return this.http.put<any>(appConfig.BaseApiUrl + path, body);
  }

  public deleteData(path: string, body: any) {
    return  this.http.delete<any>(appConfig.BaseApiUrl + path, body);
  }

}
