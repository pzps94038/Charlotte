import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseInput, FormDialog } from './form.dialog.interface';
@Component({
  selector: 'app-form-dialog',
  templateUrl: './form-dialog.component.html',
  styleUrls: ['./form-dialog.component.scss']
})
export class FormDialogComponent implements OnInit {
  form: FormGroup = new FormGroup({})
  constructor
  (
    private fb: FormBuilder,
    private dialog: MatDialogRef<FormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public formData: FormDialog
  )
  {
    this.createForm()
  }

  ngOnInit(): void {}
  /**
   * 建立Form表單
   */
  createForm()
  {
    let formConfig: { [key: string]: AbstractControl } = {}
    for(let data of this.formData.dataList)
      formConfig[data.controlName] = this.parseControl(data)
    this.form = this.fb.group(formConfig)
  }
  /**
   *
   * @param data Input資料
   * @returns 解析好的Input
   */
  parseControl(data: BaseInput): FormControl
  {
    return new FormControl({
      value: data.value !== '' ? data.value : '',
      disabled: data.disabled
      },
      data.valids
    )
  }
  /**
   * 送出資料
   */
  submit(){
    const valid = this.form.valid
    if(valid)
      this.dialog.close(this.form.value)
  }
}
