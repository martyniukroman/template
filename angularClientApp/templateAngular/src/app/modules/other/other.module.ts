import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DashboardComponent} from './dashboard/dashboard.component';
import {RouterModule} from "@angular/router";
import {FlexLayoutModule} from '@angular/flex-layout';

@NgModule({
  declarations: [
    DashboardComponent,
  ],
  imports: [
    CommonModule,
    FlexLayoutModule,
    RouterModule.forChild([
      {path: 'dashboard', component: DashboardComponent}
    ]),
  ],
  exports: [],
})
export class OtherModule {
}
