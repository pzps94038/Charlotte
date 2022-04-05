import { DashboardComponent } from './dashboard/dashboard.component';
import { Routes } from "@angular/router";
import { UserSettingComponent } from "./user-setting/user-setting.component";
import { RouterSettingComponent } from './router-setting/router-setting.component';
import { AuthGuard } from '../auth/auth.guard';
import { RoleSettingComponent } from './role-setting/role-setting.component';
import { RoleAuthSettingComponent } from './role-auth-setting/role-auth-setting.component';
import { ProductTypeSettingComponent } from './product-type-setting/product-type-setting.component';
import { ProductInformationSettingComponent } from './product-information-setting/product-information-setting.component';
import { OrderSettingComponent } from './order-setting/order-setting.component';
import { FactorySettingComponent } from './factory-setting/factory-setting.component';
import { BasicDataSettingComponent } from './basic-data-setting/basic-data-setting.component';
import { ConsumerSettingComponent } from './consumer-setting/consumer-setting.component';
import { ManagerUserSettingComponent } from './manager-user-setting/manager-user-setting.component';


export const siteMapRouter : Routes = [
  {
    path: 'home',
    component: DashboardComponent,
    data:{ title: '首頁', animation: 'home' }
  },
  {
    path: 'userSetting',
    component: UserSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '使用者設定', animation: 'userSetting' }
  },
  {
    path: 'routerSetting',
    component: RouterSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '路由設定', animation: 'routerSetting' }
  },
  {
    path: 'roleSetting',
    component: RoleSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '角色設定', animation: 'roleSetting' }
  },
  {
    path: 'consumerSetting',
    component: ConsumerSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '消費者設定', animation: 'consumerSetting' }
  },
  {
    path: 'roleAuthSetting',
    component: RoleAuthSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '角色權限設定', animation: 'roleAuthSetting' }
  },
  {
    path: 'productTypeSetting',
    component: ProductTypeSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '產品類別設定', animation: 'productTypeSetting' }
  },
  {
    path: 'productInformationSetting',
    component: ProductInformationSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '產品資訊設定', animation: 'productInformationSetting' }
  },
  {
    path: 'orderSetting',
    component: OrderSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '訂單設定', animation: 'orderSetting' }
  },
  {
    path: 'factorySetting',
    component: FactorySettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '廠商設定', animation: 'factorySetting' }
  },
  {
    path: 'basicDataSetting',
    component: BasicDataSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '基本資料設定', animation: 'basicDataSetting' }
  },
  {
    path: 'managerUserSetting',
    component: ManagerUserSettingComponent,
    canActivate:[AuthGuard],
    data:{ title: '使用者管理', animation: 'managerUserSetting' }
  },
  {
    path: '**', redirectTo:'/siteMap/home', pathMatch: 'full'
  }
]
