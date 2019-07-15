import {OnInit} from '@angular/core';
import notify from 'devextreme/ui/notify';

export abstract class BaseComponent implements OnInit {

  protected constructor(){}

  ngOnInit(): void {
  }

  protected AlertNotification(text: string = 'Alert'): void {

    alert(text);

    console.log('--- Service notification ---');
    console.log('--- type: ALERT');
    console.log('--- text: ' + text);
    console.log('----------------------------');

  }
  protected SuccessNotification(text: string = 'Success'): void {
    notify({
      message: text,
      closeOnClick: true,
      shading: false
    }, 'success', 1000);

    console.log('--- Service notification ---');
    console.log('--- type: SUCCESS');
    console.log('--- text: ' + text);
    console.log('----------------------------');

  }
  protected WarningNotification(text: string = 'Warning'): void {
    notify({
      message: text,
      closeOnClick: true,
      shading: false
    }, 'warning', 2000);

    console.log('--- Service notification ---');
    console.log('--- type: WARNING');
    console.log('--- text: ' + text);
    console.log('----------------------------');

  }
  protected ErrorNotification(text: string = 'Error'): void {
    notify({
      message: text,
      closeOnClick: true,
      shading: true
    }, 'error', 3000);

    console.log('--- Service notification ---');
    console.log('--- type: ERROR');
    console.log('--- text: ' + text);
    console.log('----------------------------');

  }

}
