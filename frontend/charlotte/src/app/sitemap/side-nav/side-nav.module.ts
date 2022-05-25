import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SideNavComponent } from './side-nav.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';
import { MatRippleModule } from '@angular/material/core';

@NgModule({
  declarations: [
    SideNavComponent
  ],
  imports: [
    CommonModule,
    MatSidenavModule,
    MatExpansionModule,
    MatIconModule,
    RouterModule,
    MatRippleModule
  ],
  exports: [
    SideNavComponent
  ]
})
export class SideNavModule { }
