import { Component, Inject, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
  FormArray,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { OrderDialog } from './order-dialog.interface';

@Component({
  selector: 'app-order-dialog',
  templateUrl: './order-dialog.component.html',
  styleUrls: ['./order-dialog.component.scss'],
})
export class OrderDialogComponent implements OnInit {
  userOptions: { text: string; value: any }[] = [];
  productOptions: { text: string; value: any }[] = [];
  required = Validators.required;
  private VaildInventory: ValidatorFn = (
    control: AbstractControl
  ): ValidationErrors | null => {
    if (this.form) {
      let idx!: string;
      for (let [index, con] of Object.entries(this.array.controls)) {
        if (con.parent === control) {
          idx = index;
          break;
        }
      }
      console.log(idx);
    }

    return null;
  };
  form: FormGroup = new FormGroup({
    managerUserId: new FormControl('', Validators.required),
    orderDetail: new FormArray([
      new FormGroup({
        productId: new FormControl('', Validators.required),
        inventory: new FormControl('', [
          Validators.required,
          this.VaildInventory,
        ]),
      }),
    ]),
  });
  get array(): FormArray {
    return this.form.get('orderDetail') as FormArray;
  }
  constructor(
    private fb: FormBuilder,
    private dialog: MatDialogRef<OrderDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public formData: OrderDialog
  ) {}

  ngOnInit(): void {
    this.userOptions = this.formData.users.map((a) => {
      return {
        value: a.managerUserId,
        text: a.userName,
      };
    });
    this.productOptions = this.formData.products.map((a) => {
      return {
        value: a.productId,
        text: a.productName,
      };
    });
  }
  submit() {
    console.log(this.form.value);
  }
  addProduct() {
    this.array.push(
      new FormGroup({
        productId: new FormControl('', Validators.required),
        inventory: new FormControl('', Validators.required),
      })
    );
  }
}
