import {Component, OnInit, ViewChild} from '@angular/core';
import {DataServiceProvider} from '../../../shared/services/DataServiceProvider';
import {map} from 'rxjs/operators';
import {BaseComponent} from "../../../shared/base.component";
import {AuthService} from "../../../shared/services/auth.service";
import {appConfig} from "../../../shared/config";
import {DxFileUploaderComponent} from "devextreme-angular";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent extends BaseComponent {

  @ViewChild(DxFileUploaderComponent, {static: false}) fileUploader: DxFileUploaderComponent;

  public responseData: any;

  public IsEditable: boolean = false;

  public popVisible: boolean = false;
  public popPosition: string = '';
  public popText: string = '';

  public imageValue: any[] = [];
  public picUploadUrl = appConfig.BaseApiUrl + 'profile/UploadPicture';

  constructor(private _dataProvider: DataServiceProvider, private authService: AuthService) {
    super();
  }

  async ngOnInit() {
    await this.getResponseData();
  }

  public async onFormSubmit(event) {
    event.preventDefault();
    let storedUserId = localStorage.getItem('userId');
    if (storedUserId) {
      this.responseData.userId = storedUserId;
      this._dataProvider.putData('profile/Update', this.responseData)
        .subscribe(x => {
          console.log(x);
          if (x) {
            this.responseData = x;
            this.SuccessNotification('Success');
          }
          if (x.displayName) {
            localStorage.setItem('displayName', x.displayName);
            this.authService.UserDisplayName.next(x.displayName);
          }
        });
    }

    this.toggleEdit();
  }

  public togglePop(event) {
    this.popPosition = event.srcElement.id;
    if (this.popPosition == 'userNameLabel')
      this.popText = 'You are able to login with the *USERNAME* as well as with *EMAIL*';

    if (this.popPosition == 'emailLabel') {
      this.popText = 'Temporary not allowed to change email';
    }
    this.popVisible = !this.popVisible;
  }

  public toggleEdit() {
    this.IsEditable = !this.IsEditable;
  }

  public async cancelEdit() {
    this.responseData = {};
    await this.getResponseData();
    this.toggleEdit();
  }

  public getResponseData() {
    let storedUserId = localStorage.getItem('userId');
    if (storedUserId) {
      this._dataProvider.getDataObservable('profile/get' + '?userId=' + storedUserId).subscribe(x => {
        this.responseData = x;
      });
    }
  }

  public FileUploaded(event) {
    console.log(event);
    this.fileUploader.instance.reset();
  }

  public FileError(event) {
    console.log(event);
  }

}
