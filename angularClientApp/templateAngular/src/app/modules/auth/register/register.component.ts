import {Component, OnInit} from '@angular/core';
import {UserRegistrationModel} from '../../../shared/models/UserRegistrationModel';
import {BaseComponent} from '../../../shared/base.component';
import {AuthService} from '../../../shared/services/auth.service';
import {Router} from '@angular/router';
import {DataServiceProvider} from "../../../shared/services/DataServiceProvider";
import {appConfig} from "../../../shared/config";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends BaseComponent {

  public registerForm: UserRegistrationModel = new UserRegistrationModel();
  public passwordConfirm: string = '';

  constructor(private _authService: AuthService, private _router: Router, private _dataService: DataServiceProvider) {
    super();
    this.validatePassword = this.validatePassword.bind(this);
    this.validateEmail = this.validateEmail.bind(this);
    this.validateUsername = this.validateUsername.bind(this);
  }

  ngOnInit() {
  }

  public onFormSubmit(event) {
    event.preventDefault();
    this._authService.Register(this.registerForm);
  }

  validatePassword(event): boolean {
    let value = event.value.toString();
    return value.length >= 6;
  }

  validateUsername(event) {
    this._dataService.getDataObservable('accaunt/IsUserNameExist' + '?username=' + event.value).subscribe(
      x => {
        if (x) {
          event.rule.message = 'This username is already taken';
          event.rule.isValid = false;
        }
      }
    );
    return true;
  }

}
