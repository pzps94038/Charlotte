import { map, Observable, Subject, takeUntil, filter, concatMap } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { InitDataTable, InitDataTableFunction } from 'src/app/shared/component/data-table/data.table.interface';
import { RoleService } from 'src/app/shared/api/role/role.service';
import { GetRoleResult } from 'src/app/shared/api/role/role.interface';
import { MatDialog } from '@angular/material/dialog';
import { SwalService } from 'src/app/shared/service/swal/swal.service';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { Validators } from '@angular/forms';
import { CheckBoxDialogComponent } from 'src/app/shared/dialog/check-box-dialog/check-box-dialog.component';
import { ApiService } from 'src/app/shared/api/api.service';
import { SharedService } from 'src/app/shared/service/shared.service';

@Component({
  selector: 'app-role-setting',
  templateUrl: './role-setting.component.html',
  styleUrls: ['./role-setting.component.scss']
})
export class RoleSettingComponent implements OnInit, OnDestroy,InitDataTable, InitDataTableFunction<GetRoleResult> {
  dataList$: Observable<GetRoleResult[]>
  destroy$ = new Subject()
  columns: { key: string; value: string | number; }[] = []
  constructor(
    private roleService: RoleService,
    private dialog: MatDialog,
    private swalService: SwalService<null>,
    private apiService: ApiService,
    private sharedService: SharedService
  )
  {
    this.dataList$ = this.getRoles()
    this.columns = this.createColumns()
  }

  ngOnInit(): void {}
  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
  getRoles(): Observable<GetRoleResult[]>{
    return this.roleService.getRoles().pipe(
      map(res=> res.data))
  }
  createColumns(): { key: string; value: string | number; }[] {
    const columns =
    [
      {
        key: 'roleId',
        value: '角色Id'
      },
      {
        key: 'roleName',
        value: '角色名稱'
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
    this.dataList$ = this.getRoles()
  }
  create(): void {
    const dialog = this.dialog.open(FormDialogComponent,{
      data: {
        title: '新建角色',
        dataList:[
          {
            controlName: 'roleName',
            labelText: '角色名稱',
            type: 'text',
            icon: 'people',
            maxLength: 20,
            valids: [Validators.required, Validators.maxLength(20)]
          },
        ]
      }
    })
    dialog.afterClosed().pipe(
      filter(data=> !this.sharedService.isNullorEmpty(data)),
      concatMap(data=> this.roleService.createRole(data)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=>{
      this.refresh()
    })
  }

  modify(row: GetRoleResult): void {
    const dialog = this.dialog.open(FormDialogComponent,{
      data: {
        title: '修改角色',
        dataList:[
          {
            controlName: 'roleId',
            labelText: '角色Id',
            type: 'text',
            value: row.roleId,
            disabled: true,
          },
          {
            controlName: 'roleName',
            labelText: '角色名稱',
            type: 'text',
            maxLength: 250,
            icon: 'people',
            value: row.roleName,
            valids: [Validators.required, Validators.maxLength(20)]
          }
        ]
      }
    })
    dialog.afterClosed().pipe(
      filter(data=> !this.sharedService.isNullorEmpty(data)),
      concatMap(data=> this.roleService.modifyRole(row.roleId, data)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=>{
      this.refresh()
    })
  }

  delete(row: GetRoleResult): void {
    this.swalService.delete().pipe(
      concatMap(()=> this.roleService.deleteRole(row.roleId)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=>{
      this.refresh()
    })
  }

  multipleDelete(rows: GetRoleResult[]): void {
    this.swalService.multipleDelete(rows).pipe(
      map(()=> rows.map(a=> a.roleId)),
      concatMap((deleteArr)=>this.roleService.batchDeleteRole(deleteArr)),
      filter(res=> this.apiService.judgeSuccess(res)),
      takeUntil(this.destroy$)
    ).subscribe(()=>{
      this.refresh()
    })
  }

  openAuthDialog(row: GetRoleResult){
    this.roleService.getRoleAuth(row.roleId).pipe(
      map(res=> res.data),
      concatMap(rowData=> {
        const dialog =  this.dialog.open(CheckBoxDialogComponent,{
          data:{
            title: '修改權限',
            rowData,
            columns:[
              {
                key: 'routerName',
                value: '路由名稱',
                type: 'string'
              },
              {
                key: 'viewAuth',
                value: '瀏覽權限',
                type: 'checkBox'
              },
              {
                key: 'createAuth',
                value: '新增權限',
                type: 'checkBox'
              },
              {
                key: 'modifyAuth',
                value: '修改權限',
                type: 'checkBox'
              },
              {
                key: 'deleteAuth',
                value: '刪除權限',
                type: 'checkBox'
              },
              {
                key: 'exportAuth',
                value: '匯出權限',
                type: 'checkBox'
              }
            ]
          }
        })
        return dialog.afterClosed()
      }),
      filter(data=> !this.sharedService.isNullorEmpty(data)),
      concatMap(data=> this.roleService.modifyRoleAuth(row.roleId, data)),
      filter(res=> this.apiService.judgeSuccess(res, true)),
      takeUntil(this.destroy$)
    ).subscribe(()=>{
      window.location.reload();
    })
  }
}
