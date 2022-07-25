import { Component, Inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormGroup,
} from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GetRoleAuthResult as RoleAuth } from '../../api/role/role.interface';
import { FormDialog } from '../form-dialog/form.dialog.interface';
import { CheckBoxColumn, CheckBoxDialog } from './check-box-dialog.interface';

@Component({
  selector: 'app-check-box-dialog',
  templateUrl: './check-box-dialog.component.html',
  styleUrls: ['./check-box-dialog.component.scss'],
})
export class CheckBoxDialogComponent implements OnInit {
  form: FormGroup = new FormGroup({
    array: new FormArray([]),
  });
  allComplete: { [key: string]: boolean } = {
    viewAuth: false,
    createAuth: false,
    modifyAuth: false,
    deleteAuth: false,
    exportAuth: false,
  };
  columns: CheckBoxColumn[] = []; // column資料
  displayedColumns: string[] = []; // 顯示column文字
  dataSource: RoleAuth[] = []; //資料來源
  get array() {
    return this.form.controls['array'] as FormArray;
  }
  constructor(
    private fb: FormBuilder,
    private dialog: MatDialogRef<CheckBoxDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public formData: CheckBoxDialog
  ) {
    this.columns = [...this.formData.columns];
    this.dataSource = this.formData.rowData;
    this.createForm(this.dataSource);
    this.displayedColumns = this.columns.map((a) => a.key);
    this.setAllComplete();
  }

  ngOnInit(): void {}

  createForm(rowData: RoleAuth[]) {
    for (let data of rowData) {
      this.array.push(
        this.fb.group({
          routerId: data.routerId,
          viewAuth: data.viewAuth,
          createAuth: data.createAuth,
          modifyAuth: data.modifyAuth,
          deleteAuth: data.deleteAuth,
          exportAuth: data.exportAuth,
        })
      );
    }
  }

  submit() {
    const valid = this.form.valid;
    this.form.markAllAsTouched();
    if (valid) this.dialog.close(this.form.value.array);
  }

  selectAll(columns: CheckBoxColumn, event: MatCheckboxChange) {
    for (const control of this.array.controls) {
      control.get(columns.key)?.setValue(event.checked);
    }
  }

  setAllComplete() {
    for (let key of Object.keys(this.allComplete)) {
      this.allComplete[key] = this.array.controls.every(
        (controls) => controls.get(key)?.value === true
      );
    }
  }

  updateAllComplete(e: MatCheckboxChange) {
    this.setAllComplete();
  }
}
