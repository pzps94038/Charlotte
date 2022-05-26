import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DynamicSelectComponent } from './dynamic-select.component';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    DynamicSelectComponent
  ],
  imports: [
    CommonModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatIconModule
  ],
  exports: [
    DynamicSelectComponent
  ]
})
export class DynamicSelectModule { }
