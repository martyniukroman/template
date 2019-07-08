import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { HomePageComponent } from './home-page/home-page.component';
import {RouterModule} from '@angular/router';
import { FooterComponent } from './footer/footer.component';
import {DxButtonModule} from 'devextreme-angular';

@NgModule({
  declarations: [
    HeaderComponent,
    HomePageComponent,
    FooterComponent,
  ],
  imports: [
    CommonModule,
    DxButtonModule,
    RouterModule.forChild([
      {path: '', component: HomePageComponent}
    ]),
  ],
  exports: [
    HeaderComponent,
    HomePageComponent,
    FooterComponent,
  ],
})
export class HomeModule { }
