import { filter, from, map, Subject, takeUntil } from 'rxjs';
import { Component, EventEmitter, OnInit, Output, OnDestroy, ɵɵsetComponentScope } from '@angular/core';
import { Router } from '@angular/router';
import { SideNavService } from 'src/app/shared/service/sideNav/side-nav.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';
import Swal from 'sweetalert2';
import { UserService } from 'src/app/shared/api/user/user.service';
import { UserInfoService } from 'src/app/shared/service/userInfo/userInfo.service';
import { ApiService } from 'src/app/shared/api/api.service';
import { LogoutService } from 'src/app/shared/service/logout/logout.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnDestroy{
  destroy$ = new Subject()
  constructor(
    private swalService: SwalService<null>,
    private router: Router,
    private sideNavService: SideNavService,
    private userService: UserService,
    private userInfoService: UserInfoService,
    private logoutService: LogoutService
  ) { }

  ngOnDestroy(): void {
    this.destroy$.next(null)
    this.destroy$.complete()
  }

  /**
   * 登出
   */
  logout(){
    this.swalService.alert({
      text: "確定要登出嗎?",
      showCancelButton: true,
      icon: 'question',
      cancelButtonText: "取消",
      confirmButtonText: "確定",
    }).pipe(
        filter(res=> res.isConfirmed),
        takeUntil(this.destroy$)
      ).subscribe(()=>{
        this.swalService.alert({
          text: '登出成功',
          icon: 'success',
          confirmButtonText: "確定",
        }).pipe(
          takeUntil(this.destroy$)
        )
        this.logoutService.logout()
    })
  }

  /** 切換SideNav */
  toggleSideNav(){
    this.sideNavService.toggle()
  }

  /** 變更密碼 */
  changePassword(){
    from(
      Swal.fire({
      title: '變更密碼',
      html:
      `<input id="password" class="swal2-input" name="password" type="password" placeholder="請輸入現在使用的密碼">
       <input id="newPassword" class="swal2-input" name="newPassword" type="password" placeholder="請輸入新密碼">
       <input id="newPassword_confirm" class="swal2-input" name="newPassword_confirm" type="password" placeholder="請再輸入一次新密碼">`,
       confirmButtonText: '確定',
       preConfirm: () => {
         const password = document.getElementById('password') as HTMLInputElement
         const newPassword = document.getElementById('newPassword') as HTMLInputElement
         const newPassword_confirm = document.getElementById('newPassword_confirm') as HTMLInputElement
        if(password.value && newPassword.value && newPassword_confirm.value){
          if(newPassword.value !== newPassword_confirm.value){
            Swal.showValidationMessage('新密碼與確認密碼不相同')
            return undefined
          }else{
            const userId = this.userInfoService.getUserInfo().managerUserId
            return this.userService.modifyPassword(userId, {
              password: password.value,
              newPassword: newPassword.value
            }).pipe(
              filter(res=> {
                if(res.code !== 200){
                  Swal.showValidationMessage(res.message)
                  return false
                }else
                  this.swalService.alert({
                    icon: 'success',
                    text: res.message,
                    confirmButtonText: '確認'
                  })
                  return true
              })
            )
          }
        }
        else{
          Swal.showValidationMessage('欄位必填')
          return undefined
        }
      }
    })).pipe(
      map((res)=> res.value),
      filter(res=> res !== undefined),
      takeUntil(this.destroy$)
    ).subscribe()
  }
}
