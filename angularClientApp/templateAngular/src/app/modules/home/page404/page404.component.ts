import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {getElement} from "devextreme-angular";

@Component({
  selector: 'app-page404',
  templateUrl: './page404.component.html',
  styleUrls: ['./page404.component.scss']
})
export class Page404Component implements OnInit {

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

}
