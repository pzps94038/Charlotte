import { RoleService } from 'src/app/shared/api/role/role.service';
import { UserService } from 'src/app/shared/api/user/user.service';
import { map, Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { InitDataTable, InitDataTableFunction } from 'src/app/shared/component/data-table/data.table.interface';
import { GetUsersRes } from 'src/app/shared/api/user/user.interface';
import { MatDialog } from '@angular/material/dialog';
import { SwalService } from 'src/app/shared/service/swal/swal.service';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-manager-user-setting',
  templateUrl: './manager-user-setting.component.html',
  styleUrls: ['./manager-user-setting.component.scss']
})
export class ManagerUserSettingComponent implements OnInit, InitDataTable, InitDataTableFunction<GetUsersRes> {
  dataList$: Observable<GetUsersRes[]>
  columns: {key: string, value: string | number}[]= []
  constructor
  (
    private userService: UserService,
    private roleService: RoleService,
    private dialog: MatDialog,
    private swalService: SwalService
  )
  {
    this.columns = this.createColumns()
    this.dataList$ = this.getUsers()
  }
  ngOnInit(): void {}
  createColumns(): { key: string; value: string | number; }[] {
    const columns =
    [
      {
        key: 'userName',
        value: '使用者姓名'
      },
      {
        key: 'email',
        value: '電子郵件'
      },
      {
        key: 'flag',
        value: 'Flag'
      },
      {
        key: 'roleName',
        value: '角色名稱'
      }
    ]
    return columns
  }
  refresh(): void {
    this.dataList$ = this.getUsers()
  }
  create(): void {
    const options = this.createRoleOptions()
    const dialog = this.dialog.open(FormDialogComponent,{
      data: {
        title: '新建使用者',
        dataList:[
          {
            controlName: 'userName',
            labelText: '使用者姓名',
            type: 'text',
            maxLength: 20,
            valids: [Validators.required, Validators.maxLength(20)]
          },
          {
            controlName: 'account',
            labelText: '使用者帳號',
            type: 'text',
            maxLength: 20,
            valids: [Validators.required, Validators.maxLength(20)]
          },
          {
            controlName: 'password',
            labelText: '使用者密碼',
            type: 'password',
            maxLength: 200,
            valids: [Validators.maxLength(200), Validators.required]
          },
          {
            controlName: 'email',
            labelText: '電子郵件',
            type: 'eamil',
            valids: [Validators.required, Validators.email]
          },
          {
            controlName: 'address',
            labelText: '地址',
            type: 'text',
            maxLength: 250,
            valids: [Validators.maxLength(250)]
          },
          {
            controlName: 'birthday',
            labelText: '生日',
            type: 'date',
            valids: [Validators.required]
          },
          {
            controlName: 'roleId',
            labelText: '角色',
            type: 'select',
            options: options,
            value: '',
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
    dialog.afterClosed().subscribe(data=>{
      if(data){
        this.userService.createUser(data).subscribe(res=>{
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
  modify(row: GetUsersRes): void {
    const options = this.createRoleOptions()
    const dialog = this.dialog.open(FormDialogComponent,{
      data: {
        title: '修改使用者',
        dataList:[
          {
            controlName: 'userName',
            labelText: '使用者姓名',
            type: 'text',
            maxLength: 20,
            value: row.userName,
            valids: [Validators.required, Validators.maxLength(20)]
          },
          {
            controlName: 'account',
            labelText: '使用者帳號',
            type: 'text',
            maxLength: 20,
            value: row.account,
            valids: [Validators.required, Validators.maxLength(20)]
          },
          {
            controlName: 'password',
            labelText: '使用者密碼',
            type: 'password',
            maxLength: 200,
            value: row.password,
            valids: [Validators.maxLength(200), Validators.required]
          },
          {
            controlName: 'email',
            labelText: '電子郵件',
            type: 'eamil',
            value: row.email,
            valids: [Validators.required, Validators.email]
          },
          {
            controlName: 'address',
            labelText: '地址',
            type: 'text',
            maxLength: 250,
            value: row.address,
            valids: [Validators.maxLength(250)]
          },
          {
            controlName: 'birthday',
            labelText: '生日',
            type: 'date',
            value: row.birthday,
            valids: [Validators.required]
          },
          {
            controlName: 'roleId',
            labelText: '角色',
            type: 'select',
            options: options,
            value: row.roleId,
            valids: [Validators.required]
          },
          {
            controlName: 'flag',
            labelText: 'Flag',
            type: 'toggle',
            value: row.flag === "Y" ? true: false,
          }
        ]
      }
    })
    dialog.afterClosed().subscribe(data=>{
      if(data){
        this.userService.modifyUser(row.managerUserId, data).subscribe(res=>{
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
  delete(row: GetUsersRes): void {
    this.swalService.alert({
      text: '確定要刪除這個使用者嗎?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: '確認',
      cancelButtonText: '取消'
    }).subscribe(data=>{
      if(data.isConfirmed){
        this.userService.deleteUser(row.managerUserId).subscribe(res=>{
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
  multipleDelete(rows: GetUsersRes[]): void {
    this.swalService.alert({
      text: `確定要刪除這${rows.length}筆使用者資料嗎?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: '確認',
      cancelButtonText: '取消'
    }).subscribe(data=>{
      if(data.isConfirmed){
        let deleteArr : number[] = []
        for(let item of rows)deleteArr.push(item.managerUserId)
        this.userService.batchDeleteUsers(deleteArr).subscribe(res=>{
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

  getUsers(): Observable<GetUsersRes[]>{
    return this.userService.getUsers().pipe(map(
      res=> res.data
    ))
  }
  createRoleOptions(): { text: string; value: number; }[]{
    let roleOptions: { text: string; value: number; }[] = []
    this.roleService.getRoles().pipe(map(
      res => res.data
    )).subscribe((datas)=>{
      for(let data of datas){
        roleOptions.push({
          text: data.roleName,
          value: data.roleId
        })
      }
    })
    return roleOptions
  }



}
