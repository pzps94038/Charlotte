import { Component, OnDestroy, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import {
  BehaviorSubject,
  concatMap,
  filter,
  map,
  Subject,
  takeUntil,
  finalize,
} from 'rxjs';
import { ApiService } from 'src/app/shared/api/api.service';
import { GetProductTypesResult as ProductType } from 'src/app/shared/api/productType/product-type.interface';
import { ProductTypeService } from 'src/app/shared/api/productType/product-type.service';
import {
  BaseDataTable,
  DataTableInfo,
  InitDataTableFunction,
} from 'src/app/shared/component/data-table/data.table.model';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { SharedService } from 'src/app/shared/service/shared.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';

@Component({
  selector: 'app-product-type-setting',
  templateUrl: './product-type-setting.component.html',
  styleUrls: ['./product-type-setting.component.scss'],
})
export class ProductTypeSettingComponent
  extends BaseDataTable<ProductType>
  implements OnInit, InitDataTableFunction<ProductType>, OnDestroy
{
  constructor(
    private productTypeService: ProductTypeService,
    private sharedService: SharedService,
    private apiService: ApiService,
    private dialog: MatDialog,
    private swalService: SwalService
  ) {
    super();
  }
  destroy$: Subject<any> = new Subject<any>();
  ngOnInit(): void {
    this.columns = this.createColumns();
    this.getProductTypes();
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }

  createColumns(): { key: string; value: string | number }[] {
    const columns = [
      {
        key: 'productTypeId',
        value: '產品類型ID',
      },
      {
        key: 'type',
        value: '類型',
      },
      {
        key: 'iconType',
        value: 'IconType',
      },
      {
        key: 'iconName',
        value: 'IconName',
      },
      {
        key: 'createDate',
        value: '創建日期',
      },
      {
        key: 'modifyDate',
        value: '修改日期',
      },
    ];
    return columns;
  }

  getProductTypes(info?: DataTableInfo) {
    this.setLoading(true);
    this.productTypeService
      .getProductTypes(info)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => this.setLoading(false))
      )
      .subscribe((res) => {
        this.tableDataList = res.data.tableDataList;
        this.tableTotalCount = res.data.tableTotalCount;
      });
  }

  refresh(): void {
    this.getProductTypes();
  }

  create(): void {
    const dialog = this.dialog.open(FormDialogComponent, {
      data: {
        title: '新建產品類型',
        dataList: [
          {
            controlName: 'type',
            labelText: '產品類型',
            type: 'text',
            icon: 'inventory_2',
            valids: [Validators.required],
          },
          {
            controlName: 'IconType',
            labelText: 'IconType',
            type: 'text',
            valids: [Validators.required],
          },
          {
            controlName: 'IconName',
            labelText: 'IconName',
            type: 'text',
            valids: [Validators.required],
          },
        ],
      },
    });
    dialog
      .afterClosed()
      .pipe(
        filter((data) => !this.sharedService.isNullorEmpty(data)),
        concatMap((data) => this.productTypeService.createProductType(data)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.refresh();
      });
  }

  modify(row: ProductType): void {
    const dialog = this.dialog.open(FormDialogComponent, {
      data: {
        title: '修改產品類型',
        dataList: [
          {
            controlName: 'type',
            labelText: '產品類型',
            type: 'text',
            icon: 'inventory_2',
            value: row.type,
            valids: [Validators.required],
          },
          {
            controlName: 'IconType',
            labelText: 'IconType',
            type: 'text',
            value: row.iconType,
          },
          {
            controlName: 'IconName',
            labelText: 'IconName',
            type: 'text',
            value: row.iconName,
          },
        ],
      },
    });
    dialog
      .afterClosed()
      .pipe(
        filter((data) => !this.sharedService.isNullorEmpty(data)),
        concatMap((data) =>
          this.productTypeService.modifyProductType(row.productTypeId, data)
        ),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.refresh();
      });
  }

  delete(row: ProductType): void {
    this.swalService
      .delete()
      .pipe(
        filter((data) => data.isConfirmed),
        concatMap(() =>
          this.productTypeService.deleteProductType(row.productTypeId)
        ),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.refresh();
      });
  }

  multipleDelete(rows: ProductType[]): void {
    this.swalService
      .multipleDelete(rows)
      .pipe(
        map(() => rows.map((a) => a.productTypeId)),
        concatMap((deleteArr) =>
          this.productTypeService.batchDeleteProductType(deleteArr)
        ),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.refresh();
      });
  }

  filterTable(info: DataTableInfo): void {
    this.getProductTypes(info);
  }
}
