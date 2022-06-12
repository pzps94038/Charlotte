import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApiUrl } from '../../api/api.url';
import { BaseInput, FormDialog } from '../form-dialog/form.dialog.interface';

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.scss']
})
export class ProductDialogComponent implements OnInit {
  form: FormGroup = new FormGroup({});
  @ViewChild('upload') productImg!: ElementRef
  url: string = ""
  imgTypeValid: boolean = true
  get productImgPath(){
    return this.form.get('productImgPath') as FormControl
  }
  constructor
  (
    private fb: FormBuilder,
    private dialog: MatDialogRef<ProductDialogComponent>,
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
   for(const data of this.formData.dataList)
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
    if(data.controlName === 'productImgPath'){
      this.imgTypeValid = true
      this.url = data.value ? `${ApiUrl.baseUrl}\\${data.value}`: ''
      return new FormControl(null, data.valids)
    }
    else{
      return new FormControl({
        value: data.value === '' ? '' : data.value,
        disabled: data.disabled
      },
        data.valids
      )
    }
  }
  /**
   * 預覽圖片
   * @param e input element
   */
  preview(e: Event){
    const target = e.target as HTMLInputElement
    const files = target.files as FileList
    if (files && files[0] && this.validFileType(files[0])) {
      const reader = new FileReader()
      reader.onload = (event: any) =>  this.url = event.target.result
      reader.readAsDataURL(files[0])
      this.imgTypeValid = true
    }else{
      this.imgTypeValid = false
      this.url = ''
    }
  }

  submit(){
    const target = this.productImg.nativeElement as HTMLInputElement
    const files = target.files as FileList
    if(this.form.valid && this.url !== ''){
      this.dialog.close(Object.assign({
        files,
        url: this.url.replace(`${ApiUrl.baseUrl}\\`, '')
      }, this.form.value))
    }
  }
  /**
   * 驗證檔案格式
   * @param file 檔案
   * @returns 是否符合格式
   */
  validFileType(file: File): boolean{
    if(file.type.match('image.*'))
      return true
    else
      return false
  }

}
