import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DynamicDatepickerComponent } from './dynamic-datepicker.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';



@NgModule({
  declarations: [
    DynamicDatepickerComponent
  ],
  imports: [
    CommonModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
  ],
  exports: [
    DynamicDatepickerComponent
  ]
})
export class DynamicDatepickerModule { }
