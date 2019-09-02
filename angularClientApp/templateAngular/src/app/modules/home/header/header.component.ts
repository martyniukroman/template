import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';
import {AuthService} from '../../../shared/services/auth.service';
import {Router} from '@angular/router';
import {Observable} from "rxjs";
import {ProfileDataModel} from "../home-shared/profileDataModel";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent extends BaseComponent {

  IsLoggedState: boolean = false;
  UserName$: Observable<string>;

  public profileOptions: ProfileDataModel[] = [
    { value: 1, name: "Profile", icon: "user" },
    { value: 2, name: "Messages", icon: "email", badge: "5" },
    { value: 3, name: "Friends", icon: "group" },
    { value: 4, name: "Exit", icon: "runner" },
  ];

  constructor(private _auth: AuthService, private _router: Router) {
    super();
  }

  async ngOnInit() {

    this._auth.isLoggesIn.subscribe(x => {
      this.IsLoggedState = x;
    });
    this.UserName$ = this._auth.currentUserDisplayName;
  }

  public SignOut() {
    this._auth.Logout();
    this._router.navigateByUrl('/auth/signin');
  }

  onButtonClick(e) {
    this._router.navigateByUrl('/other/profile');
  }

  onButtonItemClick(e) {
    if (e.itemData.value == 1) this._router.navigateByUrl('/other/profile');
    if (e.itemData.value == 4) this.SignOut();
  }

}
