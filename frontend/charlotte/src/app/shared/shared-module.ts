import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ChartModule } from 'primeng/chart';
import { MatMenuModule } from '@angular/material/menu';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { DataTableModule } from './component/data-table/data-table.module';
import { ProgressBarModule } from './component/progress-bar/progress-bar.module';
import { FormDialogModule } from './dialog/form-dialog/form-dialog.module';
import { CheckBoxDialogModule } from './dialog/check-box-dialog/check-box-dialog.module';
import { MatCardModule } from '@angular/material/card';
@NgModule({
  exports: [
    MatDialogModule,
    NgbModule,
    ChartModule,
    MatMenuModule,
    MatSnackBarModule,
    DataTableModule,
    ProgressBarModule,
    FormDialogModule,
    CheckBoxDialogModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatIconModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatCardModule
  ]
})
export class SharedModule { }
