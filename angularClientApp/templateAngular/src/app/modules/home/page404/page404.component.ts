import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-page404',
  templateUrl: './page404.component.html',
  styleUrls: ['./page404.component.scss']
})
export class Page404Component implements OnInit {

  public imgSource: string = 'http://lorempixel.com/720/480';

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

}
//http://lorempixel.com/400/200/
