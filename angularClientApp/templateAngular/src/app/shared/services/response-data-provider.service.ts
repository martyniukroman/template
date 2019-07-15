import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {appConfig} from "../config";

@Injectable({
  providedIn: 'root'
})
export class ResponseDataProviderService {

  constructor(private http: HttpClient) { }

  public getResponse(sourceUrl: string){
    return this.http.get(appConfig.DefaultHeaders);
  }

}
