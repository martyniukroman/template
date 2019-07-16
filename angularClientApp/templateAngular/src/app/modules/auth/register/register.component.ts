import {Component, OnInit} from '@angular/core';
import {UserRegistrationModel} from '../../../shared/models/UserRegistrationModel';
import {BaseComponent} from '../../../shared/base.component';
import {AuthService} from '../../../shared/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends BaseComponent {

  public registerForm: UserRegistrationModel = new UserRegistrationModel();
  public passwordConfirm: string = '';

  constructor(private _authService: AuthService, private _router: Router) {
    super();
  }

  ngOnInit() {
  }

  public onFormSubmit(event) {
    event.preventDefault();
    let response;
    this._authService.Register(this.registerForm).subscribe(
      x => response = x
    );

    if(response.ok){
      this.SuccessNotification();
      this._router.navigate(['/auth/signin']);
      console.log(response);
    }

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
