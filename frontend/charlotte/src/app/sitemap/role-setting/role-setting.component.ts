import { map, observable, Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { InitDataTable, InitDataTableFunction } from 'src/app/shared/component/data-table/data.table.interface';
import { RoleService } from 'src/app/shared/api/role/role.service';
import { GetRoleRes } from 'src/app/shared/api/role/role.interface';
import { MatDialog } from '@angular/material/dialog';
import { SwalService } from 'src/app/shared/service/swal/swal.service';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-role-setting',
  templateUrl: './role-setting.component.html',
  styleUrls: ['./role-setting.component.scss']
})
export class RoleSettingComponent implements OnInit, InitDataTable, InitDataTableFunction<GetRoleRes> {
  dataList$: Observable<GetRoleRes[]>
  columns: { key: string; value: string | number; }[] = []
  constructor(
    private roleService: RoleService,
    private dialog: MatDialog,
    private swalService: SwalService
  )
  {
    this.dataList$ = this.getRoles()
    this.columns = this.createColumns()
  }
  getRoles(): Observable<GetRoleRes[]>{
    return this.roleService.getRoles().pipe(map(
      res=> res.data
    ))
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
    dialog.afterClosed().subscribe((data)=>{
      if(data){
        this.roleService.createRole(data).subscribe(res=>{
          this.swalService.alert({
            text: res.message,
            icon: res.code === 200 ? 'success' : 'error',
            confirmButtonText: '確認'
          })
          this.refresh()
        })
      }
    })
  }
  modify(row: GetRoleRes): void {
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
    dialog.afterClosed().subscribe(data=>{
      if(data){
        this.roleService.modifyRole(row.roleId, data).subscribe(res=>{
          this.swalService.alert({
            text: res.message,
            icon: res.code === 200 ? 'success' : 'error',
            confirmButtonText: '確認'
          })
          this.refresh()
        })
      }
    })
  }
  delete(row: GetRoleRes): void {
    this.swalService.alert({
      text: '確定要刪除這筆資料嗎?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: '確認',
      cancelButtonText: '取消'
    }).subscribe(data=>{
      if(data.isConfirmed){
        this.roleService.deleteRole(row.roleId).subscribe(res=>{
          this.swalService.alert({
            text: res.message,
            icon: res.code === 200 ? 'success' : 'error',
            confirmButtonText: '確認'
          })
          this.refresh()
        })
      }
    })
  }
  multipleDelete(rows: GetRoleRes[]): void {
    this.swalService.alert({
      text: `確定要刪除這${rows.length}筆資料嗎?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: '確認',
      cancelButtonText: '取消'
    }).subscribe(data=>{
      if(data.isConfirmed){
        let deleteArr : number[] = []
        for(let item of rows)deleteArr.push(item.roleId)
        this.roleService.batchDeleteRole(deleteArr).subscribe(res=>{
          this.swalService.alert({
            text: res.message,
            icon: res.code === 200 ? 'success' : 'error',
            confirmButtonText: '確認'
          })
          this.refresh()
        })
      }
    })
  }

  ngOnInit(): void {}

}
