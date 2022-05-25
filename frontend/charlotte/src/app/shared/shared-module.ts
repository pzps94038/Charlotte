import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatMenuModule } from '@angular/material/menu';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { DataTableModule } from './component/data-table/data-table.module';
import { ProgressBarModule } from './component/progress-bar/progress-bar.module';
import { FormDialogModule } from './dialog/form-dialog/form-dialog.module';
import { CheckBoxDialogModule } from './dialog/check-box-dialog/check-box-dialog.module';
import { MatCardModule } from '@angular/material/card';
import { ChartModule } from 'angular-highcharts';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { ProductDialogModule } from './dialog/product-dialog/product-dialog.module';
@NgModule({
  exports: [
    MatDialogModule,
    NgbModule,
    MatMenuModule,
    MatSnackBarModule,
    DataTableModule,
    ProgressBarModule,
    FormDialogModule,
    ProductDialogModule,
    CheckBoxDialogModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatCardModule,
    ChartModule,
    MatNativeDateModule
  ]
})
export class SharedModule { }
