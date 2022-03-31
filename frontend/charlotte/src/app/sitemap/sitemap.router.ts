import { DashboardComponent } from './dashboard/dashboard.component';
import { Routes } from "@angular/router";
import { InventoryComponent } from "./inventory/inventory.component";
import { UserSettingComponent } from "./user-setting/user-setting.component";
import { RouterSettingComponent } from './router-setting/router-setting.component';


export const siteMapRouter : Routes = [
  {
    path: 'home',
    component: DashboardComponent,
    data:{ title: '首頁', animation: 'home' }
  },
  {
    path: 'inventory',
    component: InventoryComponent,
    data:{ title: '庫存維護', animation: 'inventory' }
  },
  {
    path: 'userSetting',
    component: UserSettingComponent,
    data:{ title: '基本資料設定', animation: 'userSetting' }
  },
  {
    path: 'routerSetting',
    component: RouterSettingComponent,
    data:{ title: '路由設定', animation: 'routerSetting' }
  },
]
