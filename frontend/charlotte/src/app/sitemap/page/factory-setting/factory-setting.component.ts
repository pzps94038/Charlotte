import { Component, OnDestroy, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import {
  BehaviorSubject,
  concatMap,
  filter,
  map,
  Observable,
  Subject,
  takeUntil,
  tap,
  finalize,
} from 'rxjs';
import { ApiService } from 'src/app/shared/api/api.service';
import { GetFactorysResult as Factory } from 'src/app/shared/api/factory/factory.interface';
import { FactoryService } from 'src/app/shared/api/factory/factory.service';
import {
  BaseDataTable,
  DataTableInfo,
  InitDataTableFunction,
} from 'src/app/shared/component/data-table/data.table.model';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { SharedService } from 'src/app/shared/service/shared.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';

@Component({
  selector: 'app-factory-setting',
  templateUrl: './factory-setting.component.html',
  styleUrls: ['./factory-setting.component.scss'],
})
export class FactorySettingComponent
  extends BaseDataTable<Factory>
  implements OnInit, OnDestroy, InitDataTableFunction<Factory>
{
  private destroy$ = new Subject<any>();
  constructor(
    private factoryService: FactoryService,
    private dialog: MatDialog,
    private sharedService: SharedService,
    private apiService: ApiService,
    private swalService: SwalService
  ) {
    super();
    this.getFactorys();
    this.columns = this.createColumns();
  }

  filterTable(info: DataTableInfo): void {
    this.getFactorys(info);
  }

  ngOnInit(): void {}
  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }

  getFactorys(info?: DataTableInfo) {
    this.loading$.next(true);
    this.factoryService
      .getFactorys(info)
      .pipe(
        tap(console.log),
        map((res) => res.data),
        takeUntil(this.destroy$),
        finalize(() => this.loading$.next(false))
      )
      .subscribe((res) => {
        console.log(res);
        this.tableDataList = res.tableDataList;
        this.tableTotalCount = res.tableTotalCount;
      });
  }

  createColumns(): { key: string; value: string | number }[] {
    const columns = [
      {
        key: 'factoryId',
        value: '廠商Id',
      },
      {
        key: 'factoryName',
        value: '廠商名稱',
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

  refresh(): void {
    this.getFactorys();
  }
  create(): void {
    const dialog = this.dialog.open(FormDialogComponent, {
      data: {
        title: '新建廠商',
        dataList: [
          {
            controlName: 'factoryName',
            labelText: '廠商名稱',
            type: 'text',
            icon: 'factory',
            valids: [Validators.required],
          },
        ],
      },
    });
    dialog
      .afterClosed()
      .pipe(
        filter((res) => !this.sharedService.isNullorEmpty(res)),
        concatMap((res) => this.factoryService.createFactory(res)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }
  modify(row: Factory): void {
    const dialog = this.dialog.open(FormDialogComponent, {
      data: {
        title: '修改廠商',
        dataList: [
          {
            controlName: 'factoryId',
            labelText: '廠商ID',
            type: 'number',
            value: row.factoryId,
            disabled: true,
          },
          {
            controlName: 'factoryName',
            labelText: '廠商名稱',
            type: 'text',
            icon: 'factory',
            value: row.factoryName,
            valids: [Validators.required],
          },
        ],
      },
    });
    dialog
      .afterClosed()
      .pipe(
        filter((data) => !this.sharedService.isNullorEmpty(data)),
        concatMap((data) =>
          this.factoryService.modifyFactory(row.factoryId, data)
        ),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }

  delete(row: Factory): void {
    this.swalService
      .delete()
      .pipe(
        concatMap(() => this.factoryService.deleteFactory(row.factoryId)),
        filter((res) => this.apiService.judgeSuccess(res, true))
      )
      .subscribe(() => this.refresh());
  }

  multipleDelete(rows: Factory[]): void {
    this.swalService.multipleDelete(rows).pipe(
      map(() => rows.map((a) => a.factoryId)),
      concatMap((factoryIds) =>
        this.factoryService.batchDeleteFactory(factoryIds)
      ),
      filter((res) => this.apiService.judgeSuccess(res, true))
    );
  }
}
