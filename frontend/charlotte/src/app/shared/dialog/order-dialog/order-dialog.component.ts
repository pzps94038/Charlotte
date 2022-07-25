import { Subject, takeUntil } from 'rxjs';
import { SwalService } from './../../service/swal/swal.service';
import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
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
import { numberFormat } from 'highcharts';
import { OrderDialog } from './order-dialog.interface';

@Component({
  selector: 'app-order-dialog',
  templateUrl: './order-dialog.component.html',
  styleUrls: ['./order-dialog.component.scss'],
})
export class OrderDialogComponent implements OnInit, OnDestroy {
  userOptions: { text: string; value: any }[] = [];
  productOptions: { text: string; value: any }[] = [];
  required = Validators.required;
  min = Validators.min(1);
  private destroy$ = new Subject<any>();
  // 驗證該商品庫存量
  private VaildInventory: ValidatorFn = (
    vaildControl: AbstractControl
  ): ValidationErrors | null => {
    let valid = true;
    if (this.form) {
      const { productId, inventory } = vaildControl.value;

      const product = this.formData.products.find(
        (a) => a.productId === productId
      );
      console.log(vaildControl, productId, product);
      // 如果輸入數量大於庫存量
      if (product) {
        if (product.inventory < inventory) {
          valid = false;
        }
      }
    }
    return valid ? null : { inventory: true };
  };

  // 驗證產品唯一
  private VaildProductDistinct: ValidatorFn = (
    vaildControl: AbstractControl
  ): ValidationErrors | null => {
    let valid = true;
    if (this.form) {
      const array = vaildControl as FormArray;
      let obj: { [key: string]: number } = {};
      for (let controls of array.controls) {
        const { productId }: { productId: number; inventory: number } =
          controls.value;
        if (!obj[productId]) {
          obj[productId] = 1;
        } else {
          valid = false;
          break;
        }
      }
    }
    return valid ? null : { productDistinct: true };
  };

  form: FormGroup = new FormGroup({
    managerUserId: new FormControl('', Validators.required),
    orderDetail: new FormArray(
      [
        new FormGroup(
          {
            productId: new FormControl('', Validators.required),
            inventory: new FormControl(0, [
              Validators.required,
              Validators.min(1),
            ]),
          },
          this.VaildInventory
        ),
      ],
      this.VaildProductDistinct
    ),
  });
  get array(): FormArray {
    return this.form.get('orderDetail') as FormArray;
  }

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialogRef<OrderDialogComponent>,
    private SwalService: SwalService,
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
  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
  /**
   * 送出表單
   */
  submit() {
    this.form.markAllAsTouched();
    console.log(this.form.value);
  }
  /**
   * 加商品
   */
  addProduct() {
    this.array.push(
      new FormGroup(
        {
          productId: new FormControl('', Validators.required),
          inventory: new FormControl(0, [
            Validators.required,
            this.VaildInventory,
          ]),
        },
        this.VaildInventory
      )
    );
  }
  /**
   * 刪除商品
   * @param idx 第幾條row
   */
  deleteProduct(idx: number) {
    if (this.array.controls.length > 1) {
      this.array.removeAt(idx);
    } else {
      this.SwalService.alert({
        title: '沒東西刪啦',
        icon: 'error',
      })
        .pipe(takeUntil(this.destroy$))
        .subscribe();
    }
  }
}
