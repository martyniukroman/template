import {BrowserModule} from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';

import {AppComponent} from './app.component';
import {RouterModule} from '@angular/router';
import {HomeModule} from './modules/home/home.module';
import {OtherModule} from "./modules/other/other.module";
import {Page404Component} from "./modules/other/page404/page404.component";

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HomeModule,
    OtherModule,
    RouterModule.forRoot([
      {path: 'home', loadChildren: './modules/home/home.module#HomeModule'},
      {path: '', loadChildren: './modules/other/other.module#OtherModule'},
      {path: '**', component: Page404Component},
    ]),
  ],
  providers: [{provide: LOCALE_ID, useValue: 'ru'}],
  bootstrap: [AppComponent]
})
export class AppModule {
}
