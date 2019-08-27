import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';
import {AuthService} from '../../../shared/services/auth.service';
import {Router} from '@angular/router';
import {Observable} from "rxjs";

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent extends BaseComponent {

  LoginStatus$ : Observable<boolean>;
  UserName$ : Observable<string>;

  constructor(private _auth: AuthService, private _router: Router) {
    super();
  }

 async ngOnInit() {

   this.LoginStatus$ = this._auth.isLoggesIn;
   this.UserName$ = this._auth.currentUserName;
  }

  public SignOut() {
    this._auth.Logout();
    this._router.navigate(['/auth/signin']).finally(() => {
      location.reload();
    });
  }
}
