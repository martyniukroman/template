import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';
import {AuthService} from '../../../shared/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent extends BaseComponent {

  public name: string = '';

  constructor(private _auth: AuthService, private _router: Router) {
    super();
  }

  ngOnInit() {
  }

  public btnClick(): void {
    alert('Hello ' + this.name);
  }

  public SignOut() {
    this._auth.Logout();
    this._router.navigate(['/auth/signin']).finally(() => {
      location.reload();
    });
  }
}
