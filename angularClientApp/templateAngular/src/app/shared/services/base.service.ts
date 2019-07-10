import {Observable} from "rxjs";

export abstract class BaseService {

  protected handleError(error: any) {

    let applicationError = error.headers.get('Application-Error');

    if (applicationError) {
      return Observable.throw(applicationError);
    }

    let modelStateErrors: string = '';
    let serverError = error.json();

    if (!serverError.type) {
      for (var key in serverError) {
        if (serverError[key])
          modelStateErrors += serverError[key] + '\n';
      }
    }

    modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
    return Observable.throw(modelStateErrors || 'Server Error');

  }

}
