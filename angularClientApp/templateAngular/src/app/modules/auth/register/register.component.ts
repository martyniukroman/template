import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public registerForm: any;

  buttonOptions: any = {
    text: "Register",
    useSubmitBehavior: true
  };

  constructor() {
  }

  ngOnInit() {
  }

  public onFormSubmit(event) {
    event.event.preventDefault();
  }

}
