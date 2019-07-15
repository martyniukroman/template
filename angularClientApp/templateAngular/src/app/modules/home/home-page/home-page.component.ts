import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})
export class HomePageComponent extends BaseComponent {

  constructor() {
    super();
  }

  ngOnInit() {
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
