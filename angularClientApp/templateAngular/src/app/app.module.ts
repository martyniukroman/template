import { BrowserModule } from '@angular/platform-browser';
import {LOCALE_ID, NgModule} from '@angular/core';

import { AppComponent } from './app.component';
import {RouterModule} from '@angular/router';
import {HomeModule} from './modules/home/home.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HomeModule,
    RouterModule.forRoot([
      {path: 'home', loadChildren: './modules/home/home.module#HomeModule'},
      // {path: 'home', component: HomePageComponent}
    ]),
  ],
  providers: [{provide: LOCALE_ID, useValue: 'ru'}],
  bootstrap: [AppComponent]
})
export class AppModule { }
