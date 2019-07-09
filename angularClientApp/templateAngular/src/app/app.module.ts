import {BrowserModule} from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';

import {AppComponent} from './app.component';
import {RouterModule} from '@angular/router';
import {HomeModule} from './modules/home/home.module';
import {HomePageComponent} from "./modules/home/home-page/home-page.component";
import {Page404Component} from "./modules/home/page404/page404.component";

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HomeModule,
    RouterModule.forRoot([
      {path: '', component: HomePageComponent},
      {path: 'home', component: HomePageComponent},
      {path: 'other', loadChildren: './modules/other/other.module#OtherModule'},
      {path: '**', component: Page404Component},
    ]),
  ],
  providers: [{provide: LOCALE_ID, useValue: 'ru'}],
  bootstrap: [AppComponent]
})
export class AppModule {
}
