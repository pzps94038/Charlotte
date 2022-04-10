import { RoleService } from 'src/app/shared/api/role/role.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { UserInfoService } from '../shared/service/userInfo/userInfo.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(
    private userInfoService: UserInfoService,
    private roleService: RoleService
    ){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const userId = this.userInfoService.getUserInfo().managerUserId
      const path = state.url
      this.roleService.checkRoleAuth(userId,path).pipe(
        map(res=> res.data)
      ).subscribe(data=>{
        console.log(data)
        this.userInfoService.changeUserAuth(data)
      })
    return true;
  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return true;
  }

}
