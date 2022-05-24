import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { siteMapRouter } from './sitemap.router';
import { SharedModule } from '../shared/shared-module';
import { BasicDataSettingComponent } from './page/basic-data-setting/basic-data-setting.component';
import { ConsumerSettingComponent } from './page/consumer-setting/consumer-setting.component';
import { DashboardComponent } from './page/dashboard/dashboard.component';
import { FactorySettingComponent } from './page/factory-setting/factory-setting.component';
import { ManagerUserSettingComponent } from './page/manager-user-setting/manager-user-setting.component';
import { OrderSettingComponent } from './page/order-setting/order-setting.component';
import { ProductInformationSettingComponent } from './page/product-information-setting/product-information-setting.component';
import { ProductTypeSettingComponent } from './page/product-type-setting/product-type-setting.component';
import { RoleSettingComponent } from './page/role-setting/role-setting.component';
import { RouterSettingComponent } from './page/router-setting/router-setting.component';
import { UserSettingComponent } from './page/user-setting/user-setting.component';
import { SideNavModule } from './side-nav/side-nav.module';
import { SitemapComponent } from './sitemap.component';
import { HeaderModule } from './header/header.module';
import { CommonModule } from '@angular/common';
import { ChartModule } from 'angular-highcharts';
@NgModule({
  declarations: [
    SitemapComponent,
    UserSettingComponent,
    DashboardComponent,
    RouterSettingComponent,
    RoleSettingComponent,
    ProductTypeSettingComponent,
    ProductInformationSettingComponent,
    OrderSettingComponent,
    FactorySettingComponent,
    BasicDataSettingComponent,
    ConsumerSettingComponent,
    ManagerUserSettingComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(siteMapRouter),
    SideNavModule,
    SharedModule,
    HeaderModule,
    ChartModule
  ],
  exports: [
    RouterModule,
  ]
})
export class SitemapModule { }
