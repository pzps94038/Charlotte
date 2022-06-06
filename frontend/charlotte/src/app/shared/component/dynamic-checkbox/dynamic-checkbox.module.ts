import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DynamicCheckboxComponent } from './dynamic-checkbox.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  declarations: [
    DynamicCheckboxComponent
  ],
  imports: [
    CommonModule,
    MatCheckboxModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
  ],
  exports:[
    DynamicCheckboxComponent
  ]
})
export class DynamicCheckboxModule { }
