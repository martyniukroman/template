import {Component, OnInit} from '@angular/core';
import {DataServiceProvider} from '../../../shared/services/DataServiceProvider';
import {map} from 'rxjs/operators';
import {BaseComponent} from "../../../shared/base.component";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent extends BaseComponent {

  public responseData: any;

  public IsEditable: boolean = false;

  public popVisible: boolean = false;
  public popPosition: string = '';
  public popText: string = '';

  constructor(private _dataProvider: DataServiceProvider) {
    super();
  }

  async ngOnInit() {
    await this.getResponseData();
  }

  public onFormSubmit(event) {
    event.preventDefault();
    this.WarningNotification('TODO: save changes to db');
    this.toggleEdit();
  }

  public togglePop(event) {
    this.popPosition = event.srcElement.id;
    if (this.popPosition == 'userNameLabel')
      this.popText = 'In future you will be able to login with the USERNAME as well as with EMAIL';

    if (this.popPosition == 'emailLabel') {
      this.popText = 'Temporary not allowed to change email';
    }
    this.popVisible = !this.popVisible;
  }

  public toggleEdit() {
    this.IsEditable = !this.IsEditable;
  }
  public async cancelEdit(){
    this.responseData = {};
    await this.getResponseData();
    this.toggleEdit();
  }

  public getResponseData(){
    let username = localStorage.getItem('username');
    if (username) {
      this._dataProvider.getDataObservable('profile/get' + '?userName=' + username).subscribe(x => {
        this.responseData = x;
      });
      console.log(this.responseData);
    }
  }

}
