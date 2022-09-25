import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDialogComponent } from './product-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { DynamicDatepickerModule } from '../../component/dynamic-datepicker/dynamic-datepicker.module';
import { DynamicInputModule } from '../../component/dynamic-input/dynamic-input.module';
import { DynamicSelectModule } from '../../component/dynamic-select/dynamic-select.module';
import { DynamicSildeToggleModule } from '../../component/dynamic-silde-toggle/dynamic-silde-toggle.module';
import { DynamicEditorModule } from '../../component/dynamic-editor/dynamic-editor.module';

@NgModule({
  declarations: [ProductDialogComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    DynamicInputModule,
    DynamicDatepickerModule,
    DynamicSildeToggleModule,
    DynamicSelectModule,
    DynamicEditorModule,
  ],
})
export class ProductDialogModule {}
