import { RoleService } from 'src/app/shared/api/role/role.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable, retry } from 'rxjs';
import { UserInfoService } from '../shared/service/userInfo/userInfo.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(
    private userInfoService: UserInfoService,
    private roleService: RoleService,
    private router: Router
    ){}
   canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean>{
      const userId = this.userInfoService.getUserInfo().managerUserId
      const path = state.url
      return this.roleService.checkRoleAuth(userId,path).pipe(
        map(res=> {
          this.userInfoService.changeUserAuth(res.data)
          if(!res.data.viewAuth)this.router.navigate(['siteMap/home'])
          return res.data.viewAuth
        })
      )
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return true;
  }

}
