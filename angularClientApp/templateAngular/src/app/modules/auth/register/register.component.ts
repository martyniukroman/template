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

    this._authService.Register(this.registerForm).subscribe( response => {

      console.log('response');
      console.log(response);

      if (response.email && response.status == 1){
        this.SuccessNotification('Your account successfully created');
        this._router.navigate(['/auth/signin']);
      }
      else{
        this.ErrorNotification('error');
      }

    });


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
