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

    return this.http.get<any>(appConfig.BaseApiUri + path)
      .pipe(
        tap(_ => console.log('data service provider'),
          catchError(this.handleError(path, [])),
        )
      );
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
