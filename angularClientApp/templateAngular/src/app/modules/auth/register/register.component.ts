import {Component, OnInit} from '@angular/core';
import {UserRegistrationModel} from '../../../shared/models/UserRegistrationModel';
import {UserAuthService} from '../../../shared/services/user-auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public registerForm: UserRegistrationModel = new UserRegistrationModel();
  public passwordConfirm: string = '';

  constructor(private _authService: UserAuthService) {
  }

  ngOnInit() {
  }

  public onFormSubmit(event) {
    event.preventDefault();

    let response = this._authService.Register(this.registerForm.email,
      this.registerForm.password,
      this.registerForm.firstName,
      this.registerForm.lastName,
      this.registerForm.location);

    console.log(response)

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
