import {Component, OnInit} from '@angular/core';
import {DataServiceProvider} from '../../../shared/services/DataServiceProvider';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  public responseData: any;

  constructor(private _dataProvider: DataServiceProvider) {
  }

  async ngOnInit() {

    // this._dataProvider.getDataObservable('dashboard/home').subscribe( (x: any) => {
    //   this.responseData = x;
    // });

    this.responseData = await this._dataProvider.getDataPromise('dashboard/home');
    console.log(this.responseData);

  }
}
