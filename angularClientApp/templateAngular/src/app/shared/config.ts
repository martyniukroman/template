import {HttpHeaders} from "@angular/common/http";

export const appConfig: any = {
    BaseApiUrl:  'https://localhost:57493/api/',
    DefaultHeaders: new HttpHeaders({'Content-Type': 'application/json'}),
};
