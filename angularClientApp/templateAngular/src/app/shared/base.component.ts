import {OnInit} from '@angular/core';
import notify from 'devextreme/ui/notify';
import {Observable, Subject} from "rxjs";

export abstract class BaseComponent implements OnInit {

  public isLogined$: Subject<boolean> = new Subject<boolean>();
  public currentUserName$: Subject<boolean>;
  public currentUserRole$: Subject<boolean>;

  protected constructor() {
    let token = localStorage.getItem('access_token');
    this.isLogined$.next(!!token);
  }

  ngOnInit(): void {
  }

  protected AlertNotification(text: string = 'Alert'): void {
    alert(text);
  }

  protected SuccessNotification(text: string = 'Success'): void {
    notify({
      message: text,
      closeOnClick: true,
      shading: false
    }, 'success', 1000);
  }

  protected WarningNotification(text: string = 'Warning'): void {
    notify({
      message: text,
      closeOnClick: true,
      shading: false
    }, 'warning', 2000);
  }

  protected ErrorNotification(text: string = 'Error'): void {
    notify({
      message: text,
      closeOnClick: true,
      shading: false
    }, 'error', 3000);
  }

}
