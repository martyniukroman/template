import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {DashboardComponent} from './dashboard/dashboard.component';
import {RouterModule} from "@angular/router";
import { Page404Component } from './page404/page404.component';

@NgModule({
  declarations: [
    DashboardComponent,
    Page404Component,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: 'other/dashboard', component: DashboardComponent}
    ]),
  ],
  exports: [
    DashboardComponent,
    Page404Component,
  ],
})
export class OtherModule {
}
