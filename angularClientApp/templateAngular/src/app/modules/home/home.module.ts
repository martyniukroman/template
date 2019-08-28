import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { HomePageComponent } from './home-page/home-page.component';
import {RouterModule} from '@angular/router';
import { FooterComponent } from './footer/footer.component';
import {DxButtonModule, DxDropDownButtonModule, DxLoadIndicatorModule, DxTextBoxModule} from 'devextreme-angular';
import {Page404Component} from "./page404/page404.component";
import {FlexLayoutModule} from "@angular/flex-layout";

@NgModule({
  declarations: [
    HeaderComponent,
    HomePageComponent,
    FooterComponent,
    Page404Component,
  ],
  imports: [
    CommonModule,
    FlexLayoutModule,
    DxButtonModule,
    DxDropDownButtonModule,
    DxLoadIndicatorModule,
    DxTextBoxModule,
    RouterModule.forChild([
      {path: '', component: HomePageComponent}
    ]),
  ],
  exports: [
    HeaderComponent,
    HomePageComponent,
    FooterComponent,
    Page404Component,
  ],
})
export class HomeModule { }
