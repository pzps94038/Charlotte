import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormDialogComponent } from './form-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { DynamicInputModule } from '../../component/dynamic-input/dynamic-input.module';
import { DynamicDatepickerModule } from '../../component/dynamic-datepicker/dynamic-datepicker.module';
import { DynamicSildeToggleModule } from '../../component/dynamic-silde-toggle/dynamic-silde-toggle.module';
import { DynamicSelectModule } from '../../component/dynamic-select/dynamic-select.module';
@NgModule({
  declarations: [
    FormDialogComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    DynamicInputModule,
    DynamicDatepickerModule,
    DynamicSildeToggleModule,
    DynamicSelectModule
  ]
})
export class FormDialogModule { }
