import {BrowserModule} from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';

import {AppComponent} from './app.component';
import {RouterModule} from '@angular/router';
import {HomeModule} from './modules/home/home.module';
import {HomePageComponent} from './modules/home/home-page/home-page.component';
import {Page404Component} from './modules/home/page404/page404.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {LoadingBarHttpClientModule} from '@ngx-loading-bar/http-client';
import {BaseInterceptor} from "./shared/services/base-Interceptor";

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HomeModule,
    LoadingBarHttpClientModule,
    RouterModule.forRoot([
      {path: '', component: HomePageComponent},
      {path: 'home', component: HomePageComponent},

      {path: 'other', loadChildren: './modules/other/other.module#OtherModule'},
      {path: 'auth', loadChildren: './modules/auth/auth.module#AuthModule'},

      // this shit should be at the bottom and in such order
      {path: '**', redirectTo: '404'},
      {path: '404', component: Page404Component},
    ]),
  ],
  providers: [
    {
      provide: LOCALE_ID,
      useValue: 'ru'
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseInterceptor,
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
