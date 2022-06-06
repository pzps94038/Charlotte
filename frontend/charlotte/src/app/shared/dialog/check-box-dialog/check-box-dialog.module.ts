import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckBoxDialogComponent } from './check-box-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { DynamicCheckboxModule } from '../../component/dynamic-checkbox/dynamic-checkbox.module';

@NgModule({
  declarations: [
    CheckBoxDialogComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatTableModule,
    MatCheckboxModule,
    DynamicCheckboxModule
  ]
})
export class CheckBoxDialogModule { }
