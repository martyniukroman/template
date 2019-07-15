import {HttpHeaders} from "@angular/common/http";

export const appConfig: any = {
    BaseApiUri:  'https://localhost:5001/api/',
    DefaultHeaders: new HttpHeaders({'Content-Type': 'application/json'}),
};
