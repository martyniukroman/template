import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';
import {AuthService} from '../../../shared/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})


export class SignInComponent extends BaseComponent {

  public password: string;
  public email: string;

  constructor(private _authService: AuthService, private _router: Router) {
    super();
  }

  ngOnInit() {
  }

  public onFormSubmit(event) {
    let response;
    event.preventDefault();

    this._authService.Login(
      {
        userName: this.email,
        password: this.password
      }).subscribe(data => {

      response = JSON.parse(data);
      if (response.auth_token) {
        localStorage.setItem('token', response.auth_token);
        this._router.navigate(['/home']).finally(() => location.reload());
      }
    });

  }


}
