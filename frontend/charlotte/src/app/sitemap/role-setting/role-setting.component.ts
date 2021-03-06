import { SharedService } from './../../shared/service/shared.service';
import { ApiService } from './../../shared/api/api.service';
import { SideNavService } from 'src/app/shared/service/sideNav/side-nav.service';
import { map, observable, Observable, Subject, takeUntil, filter, concatMap, tap } from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { InitDataTable, InitDataTableFunction } from 'src/app/shared/component/data-table/data.table.interface';
import { RoleService } from 'src/app/shared/api/role/role.service';
import { GetRoleResult } from 'src/app/shared/api/role/role.interface';
import { MatDialog } from '@angular/material/dialog';
import { SwalService } from 'src/app/shared/service/swal/swal.service';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { Validators } from '@angular/forms';
import { CheckBoxDialogComponent } from 'src/app/shared/dialog/check-box-dialog/check-box-dialog.component';

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
        value: '??????Id'
      },
      {
        key: 'roleName',
        value: '????????????'
      },
      {
        key: 'createDate',
        value: '????????????'
      },
      {
        key: 'modifyDate',
        value: '????????????'
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
        title: '????????????',
        dataList:[
          {
            controlName: 'roleName',
            labelText: '????????????',
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
        title: '????????????',
        dataList:[
          {
            controlName: 'roleId',
            labelText: '??????Id',
            type: 'text',
            value: row.roleId,
            disabled: true,
          },
          {
            controlName: 'roleName',
            labelText: '????????????',
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
            title: '????????????',
            rowData,
            columns:[
              {
                key: 'routerName',
                value: '????????????',
                type: 'string'
              },
              {
                key: 'viewAuth',
                value: '????????????',
                type: 'checkBox'
              },
              {
                key: 'createAuth',
                value: '????????????',
                type: 'checkBox'
              },
              {
                key: 'modifyAuth',
                value: '????????????',
                type: 'checkBox'
              },
              {
                key: 'deleteAuth',
                value: '????????????',
                type: 'checkBox'
              },
              {
                key: 'exportAuth',
                value: '????????????',
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
