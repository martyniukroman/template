import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from "@angular/router";
import {FlexLayoutModule} from '@angular/flex-layout';
import {ProfileComponent} from './profile/profile.component';
import {DxButtonModule} from "devextreme-angular/ui/button";
import {DxPopoverModule, DxTextBoxModule, DxValidatorModule} from "devextreme-angular";

@NgModule({
  declarations: [
    ProfileComponent,
  ],
  imports: [
    CommonModule,
    DxButtonModule,
    DxPopoverModule,
    FlexLayoutModule,
    DxValidatorModule,
    DxTextBoxModule,
    RouterModule.forChild([
      {path: 'profile', component: ProfileComponent}
    ]),
  ],
  exports: [],
})
export class OtherModule {
}
