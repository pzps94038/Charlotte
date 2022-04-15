import { filter, Subject, takeUntil } from 'rxjs';
import { Component, EventEmitter, OnInit, Output, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { SideNavService } from 'src/app/shared/service/sideNav/side-nav.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';

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
    private sideNavService: SideNavService
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
        })
        localStorage.clear()
        this.router.navigate(['/login'])
    })
  }
  /**
   * 切換SideNav
   */
  toggleSideNav(){
    this.sideNavService.toggle()
  }
}
