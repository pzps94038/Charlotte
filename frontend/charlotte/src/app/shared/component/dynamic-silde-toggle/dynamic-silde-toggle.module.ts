import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { DynamicSildeToggleComponent } from './dynamic-silde-toggle.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    DynamicSildeToggleComponent
  ],
  imports: [
    CommonModule,
    MatSlideToggleModule,
    ReactiveFormsModule
  ],
  exports: [
    DynamicSildeToggleComponent
  ]
})
export class DynamicSildeToggleModule { }
