import { Routes } from '@angular/router';
import { AuthGuard } from '../auth/auth.guard';
import { BasicDataSettingComponent } from './page/basic-data-setting/basic-data-setting.component';
import { DashboardComponent } from './page/dashboard/dashboard.component';
import { FactorySettingComponent } from './page/factory-setting/factory-setting.component';
import { ManagerUserSettingComponent } from './page/manager-user-setting/manager-user-setting.component';
import { OrderSettingComponent } from './page/order-setting/order-setting.component';
import { ProductTypeSettingComponent } from './page/product-type-setting/product-type-setting.component';
import { ProductComponent } from './page/product/product.component';
import { RoleSettingComponent } from './page/role-setting/role-setting.component';
import { RouterSettingComponent } from './page/router-setting/router-setting.component';
import { UserSettingComponent } from './page/user-setting/user-setting.component';

export const siteMapRouter: Routes = [
  {
    path: 'home',
    component: DashboardComponent,
    data: { title: '首頁', animation: 'home' },
  },
  {
    path: 'userSetting',
    component: UserSettingComponent,
    canActivate: [AuthGuard],
    data: { title: '消費者維護', animation: 'userSetting' },
  },
  {
    path: 'routerSetting',
    component: RouterSettingComponent,
    canActivate: [AuthGuard],
    data: { title: '路由維護', animation: 'routerSetting' },
  },
  {
    path: 'roleSetting',
    component: RoleSettingComponent,
    canActivate: [AuthGuard],
    data: { title: '角色與權限維護', animation: 'roleSetting' },
  },
  {
    path: 'productTypeSetting',
    component: ProductTypeSettingComponent,
    canActivate: [AuthGuard],
    data: { title: '產品類別維護', animation: 'productTypeSetting' },
  },
  {
    path: 'productSetting',
    component: ProductComponent,
    canActivate: [AuthGuard],
    data: { title: '產品資訊維護', animation: 'productSetting' },
  },
  {
    path: 'orderSetting',
    component: OrderSettingComponent,
    canActivate: [AuthGuard],
    data: { title: '訂單維護', animation: 'orderSetting' },
  },
  {
    path: 'factorySetting',
    component: FactorySettingComponent,
    canActivate: [AuthGuard],
    data: { title: '廠商維護', animation: 'factorySetting' },
  },
  {
    path: 'basicDataSetting',
    component: BasicDataSettingComponent,
    data: { title: '基本資料維護', animation: 'basicDataSetting' },
  },
  {
    path: 'managerUserSetting',
    component: ManagerUserSettingComponent,
    canActivate: [AuthGuard],
    data: { title: '使用者維護', animation: 'managerUserSetting' },
  },
  {
    path: '**',
    redirectTo: '/siteMap/home',
    pathMatch: 'full',
  },
];
