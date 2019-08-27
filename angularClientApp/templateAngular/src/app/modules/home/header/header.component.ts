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

  IsLoggedState: boolean = false;
  UserName$: Observable<string>;

  constructor(private _auth: AuthService, private _router: Router) {
    super();
  }

  async ngOnInit() {

    this._auth.isLoggesIn.subscribe(x => {
      this.IsLoggedState = x;
    });
    this.UserName$ = this._auth.currentUserName;
  }

  public SignOut() {
    this._auth.Logout();
    this._router.navigateByUrl('/auth/signin');
  }
}
