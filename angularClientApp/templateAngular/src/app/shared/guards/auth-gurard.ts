import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from "@angular/router";

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private _router: Router) {
  }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    if (localStorage.getItem('loginStatus') == '1'
      && localStorage.getItem('userId')
      && localStorage.getItem('jwt')
    ) return true;

    this._router.navigateByUrl('/auth/signin');
    return false;
  }
}
