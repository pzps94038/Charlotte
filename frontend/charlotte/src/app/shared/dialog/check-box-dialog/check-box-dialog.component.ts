import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GetRoleAuthResult } from '../../api/role/role.interface';
import { FormDialog } from '../form-dialog/form.dialog.interface';
import { CheckBoxColumn, CheckBoxDialog } from './check-box-dialog.interface';

@Component({
  selector: 'app-check-box-dialog',
  templateUrl: './check-box-dialog.component.html',
  styleUrls: ['./check-box-dialog.component.scss']
})
export class CheckBoxDialogComponent implements OnInit {
  get array() {
    return this.form.controls["array"] as FormArray;
  }
  form: FormGroup = new FormGroup({
    array: new FormArray([])
  })
  columns: CheckBoxColumn[] = []
  displayedColumns: string[] = [];
  dataSource: GetRoleAuthResult[] = [];
  constructor(
    private fb: FormBuilder,
    private dialog: MatDialogRef<CheckBoxDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public formData: CheckBoxDialog)
  {
    this.columns = [...this.formData.columns]
    this.dataSource = this.formData.rowData
    this.createForm(this.dataSource)
    this.displayedColumns = this.createDisplayedColumns(this.columns)
  }

  ngOnInit(): void
  {

  }
  createForm(rowData: GetRoleAuthResult[]){
    for(let data of rowData){
      this.array.push(this.fb.group({
        routerId: data.routerId,
        viewAuth: data.viewAuth,
        createAuth: data.createAuth,
        modifyAuth: data.modifyAuth,
        deleteAuth: data.deleteAuth,
        exportAuth: data.exportAuth
      }))
    }
  }
  createDisplayedColumns(columns: CheckBoxColumn[]): string[]{
    let displayColumns: string[] = []
    for(let item of columns)
      displayColumns.push(item.key)
    return displayColumns
  }
  submit(){
    const valid = this.form.valid
    if(valid)
      this.dialog.close(this.form.value.array)
  }

  selectAll(columns: CheckBoxColumn, event: MatCheckboxChange){
    this.array.controls.map(controls => controls.get(columns.key)?.setValue(event.checked))
  }

  checkAll(columns: CheckBoxColumn) : boolean{
    return this.array.controls.every(controls=> controls.get(columns.key)?.value === true)
  }
}
