import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { MatDrawerMode } from '@angular/material/sidenav';
import { ChildrenOutletContexts } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { RouterAnimations } from 'src/app/shared/animations/router';
import { SideNavService } from 'src/app/shared/service/sideNav/side-nav.service';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss'],
  animations: [RouterAnimations]
})
export class SideNavComponent implements OnInit {
  mode : MatDrawerMode = "side"
  show$: BehaviorSubject<boolean>
  constructor(
    private observer: BreakpointObserver,
    private contexts: ChildrenOutletContexts,
    private sideNavService: SideNavService
    )
    {
      this.show$ = this.sideNavService.show$
    }

  ngOnInit(): void {
    this.initSideNav()
  }
  /** 設定SideNav響應 */
  initSideNav(){
    this.observer.observe(['(max-width: 800px)']).subscribe((res) => {
      if (res.matches) {
        this.mode = 'over';
        this.sideNavService.hide()
      } else {
        this.mode = 'side';
        this.sideNavService.show()
      }
    });
  }
  /**
   * 設定動畫
   * @returns 路由參數
   */
  getRouteAnimationData() {
    return this.contexts.getContext('primary')?.route?.snapshot?.data?.['animation'];
  }
}
