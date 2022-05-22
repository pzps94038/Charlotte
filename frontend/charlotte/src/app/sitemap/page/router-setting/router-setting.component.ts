import { Component, OnDestroy, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { map, Observable, filter, concatMap, Subject, takeUntil, BehaviorSubject, delay, of } from 'rxjs';
import { ApiService } from 'src/app/shared/api/api.service';
import { GetRouterResult as Router } from 'src/app/shared/api/router/router.interface';

import { RouterService } from 'src/app/shared/api/router/router.service';
import { DataTableInfo, InitDataTable, InitDataTableFunction } from 'src/app/shared/component/data-table/data.table.interface';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { SharedService } from 'src/app/shared/service/shared.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';

@Component({
  selector: 'app-router-setting',
  templateUrl: './router-setting.component.html',
  styleUrls: ['./router-setting.component.scss']
})
export class RouterSettingComponent implements OnInit, InitDataTable<Router>, InitDataTableFunction<Router>, OnDestroy {
  columns : {key: string, value: string | number}[]= []
  loading$ : BehaviorSubject<boolean>  = new BehaviorSubject<boolean>(false)
  tableDataList: Router[] = []
  tableTotalCount: number = 0;
  destroy$ = new Subject()
  constructor
  (
    private routerService: RouterService,
    private dialog: MatDialog,
    private swalService: SwalService,
    private apiService: ApiService,
    private sharedService: SharedService
  )
  {
    this.getRouters()
    this.columns = this.createColumns()
  }


  ngOnInit(): void{}
  ngOnDestroy(): void {
    this.destroy$.next(null)
    this.destroy$.complete()
  }
  /** 刷新路由 */
  refresh(): void {
    this.getRouters()
  }

  /** 創建路由 */
  create(): void {
    const dialog = this.dialog.open(FormDialogComponent,{
      data: {
        title: '新建路由',
        dataList:[
          {
            controlName: 'link',
            labelText: '連結',
            type: 'text',
            icon: 'link',
            maxLength: 250,
            valids: [Validators.required, Validators.maxLength(250)]
          },
          {
            controlName: 'routerName',
            labelText: '路由名稱',
            type: 'text',
            icon: 'badge',
            maxLength: 250,
            valids: [Validators.required, Validators.maxLength(250)]
          },
          {
            controlName: 'icon',
            labelText: 'Icon',
            type: 'text',
            icon: 'insert_emoticon',
            maxLength: 250,
            valids: [Validators.maxLength(250)]
          },
          {
            controlName: 'groupId',
            labelText: 'GroupId',
            type: 'number',
            icon: 'groups',
            valids: [Validators.required]
          },
          {
            controlName: 'flag',
            labelText: 'Flag',
            type: 'toggle',
            value: true,
          }
        ]
      }

    })
    dialog.afterClosed().pipe(
      filter(data=> !this.sharedService.isNullorEmpty(data)),
      concatMap(data=> this.routerService.createRouter(data)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=>{
      window.location.reload();
    })
  }

  /** 修改路由 */
  modify(row : Router): void {
    const dialog = this.dialog.open(FormDialogComponent,{
      data: {
        title: '修改路由',
        dataList:[
          {
            controlName: 'routerId',
            labelText: '路由Id',
            disabled: true,
            type: 'number',
            value: row.routerId
          },
          {
            controlName: 'routerName',
            labelText: '路由名稱',
            type: 'text',
            maxLength: 250,
            icon: 'badge',
            value: row.routerName,
            valids: [Validators.required, Validators.maxLength(250)]
          },
          {
            controlName: 'link',
            labelText: '連結',
            type: 'text',
            icon: 'link',
            value: row.link,
            maxLength: 250,
            valids: [Validators.required, Validators.maxLength(250)]
          },
          {
            controlName: 'icon',
            labelText: 'Icon',
            type: 'text',
            icon: 'insert_emoticon',
            maxLength: 250,
            value: row.icon,
            valids: [Validators.maxLength(250)]
          },
          {
            controlName: 'groupId',
            labelText: 'GroupId',
            type: 'number',
            icon: 'groups',
            value: row.groupId,
            valids: [Validators.required]
          },
          {
            controlName: 'flag',
            labelText: 'Flag',
            type: 'toggle',
            value: row.flag === "Y",
          }
        ]
      }
    })
    dialog.afterClosed().pipe(
      filter(data=> !this.sharedService.isNullorEmpty(data)),
      concatMap(data=> this.routerService.modifyRouter(row.routerId, data)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=>{
      window.location.reload();
    })
  }

  /** 刪除路由 */
  delete(row :Router): void {
    this.swalService.delete().pipe(
        filter(data=> data.isConfirmed),
        concatMap(()=> this.routerService.deleteRouter(row.routerId)),
        filter(res=> this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      ).subscribe(()=>{
        window.location.reload();
    })
  }

  /** 批次刪除路由 */
  multipleDelete(rows: Router[]): void {
    this.swalService.multipleDelete(rows).pipe
    (
      map(()=> rows.map(a=> a.routerId)),
      concatMap((deleteArr)=>this.routerService.batchDeleteRouter(deleteArr)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=>{
      window.location.reload();
    })
  }

  /** 創建表頭 */
  createColumns(): { key: string; value: string | number; }[]
  {
    const columns =
    [
      {
        key: 'routerId',
        value: '路由Id'
      },
      {
        key: 'routerName',
        value: '路由名稱'
      },
      {
        key: 'link',
        value: '連結'
      },
      {
        key: 'icon',
        value: 'Icon'
      },
      {
        key: 'groupId',
        value: 'GroupId'
      },
      {
        key: 'flag',
        value: 'Flag'
      }
    ]
    return columns
  }

  /** 取得路由表 */
  getRouters(info?: DataTableInfo): void{
    this.loading$.next(true)
    this.routerService.getRouters(info).pipe(
      map(res=> res.data)
    ).subscribe((res)=> {
      this.loading$.next(false)
      this.tableDataList = res.tableDataList
      this.tableTotalCount = res.tableTotalCount
    })
  }

  filterTable(info: DataTableInfo): void {
    console.log(info)
    this.getRouters(info)
  }

}

