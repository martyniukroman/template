import { Component, OnInit } from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent extends BaseComponent {

  public name: string = '';

  constructor() { super(); }

  ngOnInit() {
  }

  public btnClick(): void{
    alert('Hello ' + this.name);
  }

}
