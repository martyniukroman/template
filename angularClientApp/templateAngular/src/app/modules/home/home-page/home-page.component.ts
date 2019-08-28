import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';
import {DataServiceProvider} from '../../../shared/services/DataServiceProvider';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent extends BaseComponent {

  stroke: any[] = [];

  constructor(private _dataS: DataServiceProvider) {
    super();
  }

}
