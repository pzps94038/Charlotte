import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { siteMapRouter } from './sitemap.router';
import { SharedModule } from '../shared/shared-module';
import { UserSettingComponent } from './user-setting/user-setting.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RouterSettingComponent } from './router-setting/router-setting.component';
import { RoleSettingComponent } from './role-setting/role-setting.component';
import { ProductTypeSettingComponent } from './product-type-setting/product-type-setting.component';
import { ProductInformationSettingComponent } from './product-information-setting/product-information-setting.component';
import { OrderSettingComponent } from './order-setting/order-setting.component';
import { FactorySettingComponent } from './factory-setting/factory-setting.component';
import { BasicDataSettingComponent } from './basic-data-setting/basic-data-setting.component';
import { ConsumerSettingComponent } from './consumer-setting/consumer-setting.component';
import { ManagerUserSettingComponent } from './manager-user-setting/manager-user-setting.component';

@NgModule({
  declarations: [
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
    RouterModule.forChild(siteMapRouter),
    SharedModule,
  ],
  exports: [
    RouterModule
  ]
})
export class SitemapModule { }
