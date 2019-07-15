import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';
import {DataServiceProvider} from '../../../shared/services/DataServiceProvider';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent extends BaseComponent {

  constructor(private _dataS: DataServiceProvider) {
    super();
  }

  ngOnInit() {
    let response = this._dataS.getData();
    console.log(response);
  }

  onA() {
    this.AlertNotification('Alert template text');
  }

  onS() {
    this.SuccessNotification('Success template text');
  }

  onW() {
    this.WarningNotification('Warning template text');
  }

  onE() {
    this.ErrorNotification('Error template text');
  }

}
