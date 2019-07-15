import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {appConfig} from '../config';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _httpClient: HttpClient) { }

  public Login(data: any){
   // return this._httpClient.post<any>(appConfig.BaseApiUri + 'auth')
  }

}
