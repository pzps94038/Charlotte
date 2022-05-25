import { Component, Inject, OnInit } from '@angular/core';
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
    "productImg": new FormControl('', Validators.required)
  });
  url: string = ""

  constructor
  (
    @Inject(MAT_DIALOG_DATA) public formData: FormDialog
  ) { }

  ngOnInit(): void {}
  preview(e: Event){
    const target = e.target as HTMLInputElement
    const files = target.files as FileList
    if (files && files[0]) {
      console.log(files[0])
      var a = files[0].type
      var reader = new FileReader();
      reader.onload = (event: any) => {
          this.url = event.target.result;
      }
      reader.readAsDataURL(files[0]);
  }
  }
  submit(){
    console.log(this.form)
  }

}
