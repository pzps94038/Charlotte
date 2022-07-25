import { UserInfoService } from './../../shared/service/userInfo/userInfo.service';
import { MenuService } from './../../shared/api/menu/menu.service';
import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatDrawerMode } from '@angular/material/sidenav';
import { ChildrenOutletContexts } from '@angular/router';
import {
  BehaviorSubject,
  filter,
  map,
  Observable,
  Subject,
  takeUntil,
  tap,
} from 'rxjs';
import { RouterAnimations } from 'src/app/shared/animations/router';
import { SideNavService } from 'src/app/shared/service/sideNav/side-nav.service';
import { GetMenuResult as Menu } from 'src/app/shared/api/menu/menu.interface';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss'],
  animations: [RouterAnimations],
})
export class SideNavComponent implements OnInit, OnDestroy {
  mode: MatDrawerMode = 'side';
  show$: Observable<boolean>;
  destroy$ = new Subject<any>();
  menu$: Observable<Menu[]>;
  constructor(
    private observer: BreakpointObserver,
    private contexts: ChildrenOutletContexts,
    private sideNavService: SideNavService,
    private menuService: MenuService,
    private userInfoService: UserInfoService
  ) {
    this.show$ = this.sideNavService
      .getSideState()
      .pipe(takeUntil(this.destroy$));
    this.menu$ = this.getMenu();
  }

  ngOnInit(): void {
    this.getMenu();
    this.initSideNav();
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }

  /** 設定SideNav響應 */
  initSideNav() {
    this.observer
      .observe(['(max-width: 800px)'])
      .pipe(takeUntil(this.destroy$))
      .subscribe((res) => {
        if (res.matches) {
          this.mode = 'over';
          this.sideNavService.hide();
        } else {
          this.mode = 'side';
          this.sideNavService.show();
        }
      });
  }
  /**
   * 設定動畫
   * @returns 路由參數
   */
  getRouteAnimationData() {
    return this.contexts.getContext('primary')?.route?.snapshot?.data?.[
      'animation'
    ];
  }

  /**
   * 取得左邊菜單
   * @returns 該使用者權限菜單
   */
  getMenu(): Observable<Menu[]> {
    const userId = this.userInfoService.getUserInfo().managerUserId;
    return this.menuService.getMenu(userId).pipe(
      filter((res) => res.code === 200),
      map((res) => res.data),
      takeUntil(this.destroy$)
    );
  }
}
