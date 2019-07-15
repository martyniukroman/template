import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import { RegisterComponent } from './register/register.component';
import {DxButtonModule, DxFormModule, DxTextBoxModule, DxValidatorModule} from 'devextreme-angular';
import {DxiItemModule} from "devextreme-angular/ui/nested/item-dxi";
import {FlexLayoutModule} from "@angular/flex-layout";
import {DxiValidationRuleModule} from 'devextreme-angular/ui/nested/validation-rule-dxi';

@NgModule({
  declarations: [
    RegisterComponent,
  ],
  imports: [
    DxValidatorModule,
    DxiValidationRuleModule,
    DxFormModule,
    DxiItemModule,
    DxTextBoxModule,
    DxButtonModule,
    FlexLayoutModule,
    CommonModule,
    RouterModule.forChild([
      {path: 'register', component: RegisterComponent}
    ]),
  ]
})
export class AuthModule { }
