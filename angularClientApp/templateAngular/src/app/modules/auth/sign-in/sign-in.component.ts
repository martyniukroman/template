import { Component, OnInit } from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})


export class SignInComponent extends BaseComponent {

  public password: string;
  public email: string;

  constructor() { super(); }

  ngOnInit() {
  }

  public onFormSubmit(){

  }

}
