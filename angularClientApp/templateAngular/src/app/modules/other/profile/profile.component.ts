import {Component, OnInit} from '@angular/core';
import {DataServiceProvider} from '../../../shared/services/DataServiceProvider';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public responseData: any;

  constructor(private _dataProvider: DataServiceProvider) {
  }

  async ngOnInit() {

    let username = localStorage.getItem('username');
    console.log('username');
    console.log(username);
    if (username) {
      this.responseData = await this._dataProvider.getDataPromise('profile/get', username);
      console.log(this.responseData);
    }


  }
}
