import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { SwalService } from './service/swal/swal.service';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { NgxSpinnerModule } from 'ngx-spinner';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatNativeDateModule, MatRippleModule } from '@angular/material/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ChartModule } from 'primeng/chart';
import { MatMenuModule } from '@angular/material/menu';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { CommonModule } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { DataTableComponent } from './component/data-table/data-table.component';
import { FormDialogComponent } from './dialog/form-dialog/form-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSelectModule } from '@angular/material/select';
import { CheckBoxDialogComponent } from './dialog/check-box-dialog/check-box-dialog.component';
import { ProgressBarComponent } from './component/progress-bar/progress-bar.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
@NgModule({
  declarations:[
    DataTableComponent,
    FormDialogComponent,
    CheckBoxDialogComponent,
    ProgressBarComponent
  ],
  imports: [
    MatIconModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatInputModule,
    MatCheckboxModule,
    MatDialogModule,
    MatSlideToggleModule,
    MatSelectModule,
    MatDatepickerModule,
    MatProgressBarModule,
  ],
  providers:[SwalService],
  exports: [
    MatDialogModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    NgbModule,
    MatIconModule,
    NgxSpinnerModule,
    MatToolbarModule,
    MatListModule,
    MatSidenavModule,
    MatExpansionModule,
    MatCardModule,
    MatRippleModule,
    ChartModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatMenuModule,
    MatPaginatorModule,
    DataTableComponent,
    FormDialogComponent,
    ProgressBarComponent,
    MatSnackBarModule
  ]
})
export class SharedModule { }
