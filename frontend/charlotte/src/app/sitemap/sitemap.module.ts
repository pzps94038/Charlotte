import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InventoryComponent } from './inventory/inventory.component';
import { RouterModule } from '@angular/router';
import { siteMapRouter } from './sitemap.router';
import { SharedModule } from '../shared/shared-module';
import { UserSettingComponent } from './user-setting/user-setting.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RouterSettingComponent } from './router-setting/router-setting.component';

@NgModule({
  declarations: [
    InventoryComponent,
    UserSettingComponent,
    DashboardComponent,
    RouterSettingComponent,
  ],
  imports: [
    RouterModule.forChild(siteMapRouter),
    CommonModule,
    SharedModule,
  ],
  exports: [
    RouterModule
  ]
})
export class SitemapModule { }
