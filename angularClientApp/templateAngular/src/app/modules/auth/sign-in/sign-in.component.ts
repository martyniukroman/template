import {Component, OnInit} from '@angular/core';
import {BaseComponent} from '../../../shared/base.component';
import {AuthService} from '../../../shared/services/auth.service';
import {Router} from '@angular/router';
import {Local} from "protractor/built/driverProviders";

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})


export class SignInComponent extends BaseComponent {

  public password: string;
  public email: string;
  private _invalidLogin: boolean;

  constructor(private _authService: AuthService, private _router: Router) {
    super();
  }

  ngOnInit() {
  }

  public onFormSubmit(event) {

    this._authService.Login(this.email, this.password).subscribe(result => {

      console.log(result);

        let token = (<any>result).authToken.token;
        console.log(token);
        console.log(result.authToken.roles);
        console.log("User Logged In Successfully");
        this._invalidLogin = false;
        this._router.navigateByUrl('/home');

      },
      error => {
        this._invalidLogin = true;
        this.ErrorNotification(error.error.loginError);
        console.log(error.error.loginError);
      })


  }


}
