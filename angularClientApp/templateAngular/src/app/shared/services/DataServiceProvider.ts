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

  public getData(path: string = 'values') {

    return this.http.get<any>(appConfig.BaseApiUri + path);
  }

}
