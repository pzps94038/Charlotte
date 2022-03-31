import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { SideNavService } from 'src/app/shared/service/sideNav/side-nav.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent{
  constructor(
    private swalService: SwalService,
    private router: Router,
    private sideNavService: SideNavService
  ) { }

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
    }).subscribe(res=>{
      if(res.isConfirmed){
        this.swalService.alert({
          text: '登出成功',
          icon: 'success',
          confirmButtonText: "確定",
        })
        localStorage.clear()
        this.router.navigate(['/login'])
      }
    })
  }
  /**
   * 切換SideNav
   */
  toggleSideNav(){
    this.sideNavService.toggle()
  }
}
