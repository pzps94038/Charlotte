import { Component, OnDestroy, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { concatMap, filter, map, Observable, Subject, takeUntil, tap } from 'rxjs';
import { ApiService } from 'src/app/shared/api/api.service';
import { GetFactorysResult as Factory } from 'src/app/shared/api/factory/factory.interface';
import { FactoryService } from 'src/app/shared/api/factory/factory.service';
import { InitDataTable, InitDataTableFunction } from 'src/app/shared/component/data-table/data.table.interface';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { SharedService } from 'src/app/shared/service/shared.service';

@Component({
  selector: 'app-factory-setting',
  templateUrl: './factory-setting.component.html',
  styleUrls: ['./factory-setting.component.scss']
})
export class FactorySettingComponent implements OnInit, OnDestroy, InitDataTable, InitDataTableFunction<Factory> {
  destroy$ = new Subject()
  dataList$ : Observable<Factory[]>
  columns: { key: string; value: string | number; }[]

  constructor
  (
    private factoryService : FactoryService,
    private dialog: MatDialog,
    private sharedService : SharedService,
    private apiService: ApiService
  )
  {
    this.dataList$ = this.getFactorys()
    this.columns = this.createColumns()
  }

  ngOnInit(): void {

  }
  ngOnDestroy(): void {
    this.destroy$.next(null)
    this.destroy$.complete()
  }

  getFactorys(): Observable<Factory[]>{
    return this.factoryService.getFactorys().pipe(
      map(res=> res.data)
    )
  }

  createColumns(): { key: string; value: string | number; }[] {
    const columns =
    [
      {
        key: 'factoryId',
        value: '廠商Id'
      },
      {
        key: 'factoryName',
        value: '廠商名稱'
      },
      {
        key: 'createDate',
        value: '創建日期'
      },
      {
        key: 'modifyDate',
        value: '修改日期'
      }
    ]
    return columns;
  }

  refresh(): void {
    this.dataList$ = this.getFactorys()
  }
  create(): void {
    const dialog = this.dialog.open(FormDialogComponent,{
      data: {
        title: '新建廠商',
        dataList:[
          {
            controlName: 'factoryName',
            labelText: '廠商名稱',
            type: 'text',
            icon: 'factory',
            valids: [Validators.required]
          },
        ]
      }
    })
    dialog.afterClosed().pipe(
      filter(res=> !this.sharedService.isNullorEmpty(res)),
      concatMap(res=> this.factoryService.createFactory(res)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=>this.refresh())
  }
  modify(row: Factory): void {
    const dialog = this.dialog.open(FormDialogComponent,{
      data: {
        title: '修改廠商',
        dataList:[
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
            valids: [Validators.required]
          }
        ]
      }
    })
    dialog.afterClosed().pipe(
      filter(data=> !this.sharedService.isNullorEmpty(data)),
      concatMap(data=> this.factoryService.modifyFactory(row.factoryId, data)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=> this.refresh())
  }
  delete(row: Factory): void {
    this.factoryService.deleteFactory(row.factoryId).pipe(
      filter(res=> this.apiService.judgeSuccess(res, true)),
    ).subscribe(()=> this.refresh())
  }
  multipleDelete(rows: Factory[]): void {
    throw new Error('Method not implemented.');
  }
}
