import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import { RegisterComponent } from './register/register.component';
import {DxButtonModule, DxFormModule, DxTextBoxModule} from "devextreme-angular";
import {DxiItemModule} from "devextreme-angular/ui/nested/item-dxi";
import {FlexLayoutModule} from "@angular/flex-layout";

@NgModule({
  declarations: [
    RegisterComponent,
  ],
  imports: [
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
