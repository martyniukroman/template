import {Component, OnInit} from '@angular/core';
import {UserRegistrationModel} from '../../../shared/models/UserRegistrationModel';
import {BaseComponent} from '../../../shared/base.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends BaseComponent {

  public registerForm: UserRegistrationModel = new UserRegistrationModel();
  public passwordConfirm: string = '';

  constructor() {
    super();
  }

  ngOnInit() {
  }

  public onFormSubmit(event) {
    event.preventDefault();

    this.SuccessNotification();

  }

  validatePassword(event): boolean{
    let value = event.value.toString();
    return value.length >= 6;
  }
  validateEmail (event): boolean{
    let value = event.value.toString();
    return  value.includes('.') && value.includes('@') && value.length > 5;
  }

}
