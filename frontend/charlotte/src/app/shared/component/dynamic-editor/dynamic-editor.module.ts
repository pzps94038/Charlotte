import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DynamicEditorComponent } from './dynamic-editor.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@NgModule({
  declarations: [DynamicEditorComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    AngularEditorModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
  ],
  exports: [DynamicEditorComponent],
})
export class DynamicEditorModule {}
