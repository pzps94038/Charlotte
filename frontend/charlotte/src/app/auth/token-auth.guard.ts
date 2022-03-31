import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { TokenService } from '../shared/service/token/token.service';

@Injectable({
  providedIn: 'root'
})
export class TokenAuthGuard implements CanActivate, CanActivateChild {

  constructor(
    private tokenService: TokenService,
    private router: Router)
    {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const haveToken = this.tokenService.checkToken()
    if(haveToken)return true
    else{
      this.router.navigate(['/login'])
      return false
    }
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const haveToken = this.tokenService.checkToken()
      if(haveToken)return true
      else{
        this.router.navigate(['/login'])
        return false
      }
  }

}
