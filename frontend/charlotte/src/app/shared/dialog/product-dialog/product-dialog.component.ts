import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormDialog } from '../form-dialog/form.dialog.interface';

@Component({
  selector: 'app-product-dialog',
  templateUrl: './product-dialog.component.html',
  styleUrls: ['./product-dialog.component.scss']
})
export class ProductDialogComponent implements OnInit {
  form: FormGroup = new FormGroup({
    "productImg": new FormControl('', [Validators.required])
  });
  @ViewChild('upload') productImg!: ElementRef
  url: string = ""
  imgTypeValid: boolean = true
  constructor
  (
    @Inject(MAT_DIALOG_DATA) public formData: FormDialog
  )
  {

  }

  ngOnInit(): void {}

  /**
   * 預覽圖片
   * @param e input element
   */
  preview(e: Event){
    const target = e.target as HTMLInputElement
    const files = target.files as FileList
    console.log(files && files[0] && this.validFileType(files[0]))
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
    if(this.form.valid && this.validFileType(files[0])){

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
