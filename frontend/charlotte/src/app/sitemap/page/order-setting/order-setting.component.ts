import { UserService } from 'src/app/shared/api/user/user.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';
import { OrderService } from './../../../shared/api/order/order.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { GetOrdersResult as Orders } from 'src/app/shared/api/order/order.interface';
import {
  BaseDataTable,
  DataTableInfo,
  InitDataTableFunction,
} from 'src/app/shared/component/data-table/data.table.model';
import {
  map,
  takeUntil,
  finalize,
  Subject,
  concatMap,
  filter,
  forkJoin,
} from 'rxjs';
import { ApiService } from 'src/app/shared/api/api.service';
import { ProductService } from 'src/app/shared/api/product/product.service';
import { MatDialog } from '@angular/material/dialog';
import { OrderDialogComponent } from 'src/app/shared/dialog/order-dialog/order-dialog.component';

@Component({
  selector: 'app-order-setting',
  templateUrl: './order-setting.component.html',
  styleUrls: ['./order-setting.component.scss'],
})
export class OrderSettingComponent
  extends BaseDataTable<Orders>
  implements OnInit, OnDestroy, InitDataTableFunction<Orders>
{
  private destroy$ = new Subject<any>();
  constructor(
    private orderService: OrderService,
    private swalService: SwalService,
    private apiService: ApiService,
    private userService: UserService,
    private productService: ProductService,
    private dialog: MatDialog
  ) {
    super();
    this.columns = this.createColumns();
    this.getOrders();
  }
  createColumns(): { key: string; value: string | number }[] {
    const columns = [
      {
        key: 'orderId',
        value: '訂單編號',
      },
      {
        key: 'userId',
        value: '使用者ID',
      },
      {
        key: 'userName',
        value: '使用者名稱',
      },
      {
        key: 'orderAmount',
        value: '訂單金額',
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
  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }
  refresh(): void {
    this.getOrders();
  }
  getOrders(info?: DataTableInfo) {
    this.loading$.next(true);
    this.orderService
      .getOrders(info)
      .pipe(
        map((res) => res.data),
        takeUntil(this.destroy$),
        finalize(() => this.loading$.next(false))
      )
      .subscribe((res) => {
        this.tableDataList = res.tableDataList;
        this.tableTotalCount = res.tableTotalCount;
      });
  }
  create(): void {
    const users$ = this.userService
      .getUsers()
      .pipe(map((res) => res.data.tableDataList));
    const products$ = this.productService
      .getProducts()
      .pipe(map((res) => res.data.tableDataList));
    forkJoin([users$, products$])
      .pipe(
        concatMap(([users, products]) => {
          const dialog = this.dialog.open(OrderDialogComponent, {
            data: { users, products, title: '新建訂單' },
          });
          return dialog.afterClosed();
        }),
        takeUntil(this.destroy$)
      )
      .subscribe((res) => {
        console.log(res);
      });
  }
  modify(row: Orders): void {
    throw new Error('Method not implemented.');
  }
  delete(row: Orders): void {
    this.swalService
      .delete()
      .pipe(
        concatMap(() => this.orderService.deleteOrder(row.orderId)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }
  multipleDelete(rows: Orders[]): void {
    this.swalService
      .multipleDelete(rows)
      .pipe(
        map(() => rows.map((a) => a.orderId)),
        concatMap((deleteArr) =>
          this.orderService.batchDeleteOrders(deleteArr)
        ),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }
  filterTable(info: DataTableInfo): void {
    this.getOrders(info);
  }

  ngOnInit(): void {}
}
