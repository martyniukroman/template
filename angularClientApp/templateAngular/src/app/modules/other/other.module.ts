import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from "@angular/router";
import {FlexLayoutModule} from '@angular/flex-layout';
import {ProfileComponent} from './profile/profile.component';
import {DxButtonModule} from "devextreme-angular/ui/button";
import {DxFileUploaderModule, DxPopoverModule, DxTextBoxModule, DxValidatorModule} from "devextreme-angular";
import {AuthGuard} from "../../shared/guards/auth-gurard";

@NgModule({
  declarations: [
    ProfileComponent,
  ],
  imports: [
    CommonModule,
    DxButtonModule,
    DxPopoverModule,
    FlexLayoutModule,
    DxFileUploaderModule,
    DxValidatorModule,
    DxTextBoxModule,
    RouterModule.forChild([
      {path: 'profile', component: ProfileComponent, canActivate: [AuthGuard]}
    ]),
  ],
  providers: [
    AuthGuard
  ],
  exports: [],
})
export class OtherModule {
}
