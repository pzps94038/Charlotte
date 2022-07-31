import { SharedService } from './../../../shared/service/shared.service';
import { ApiService } from './../../../shared/api/api.service';
import { SwalService } from './../../../shared/service/swal/swal.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  map,
  Subject,
  switchMap,
  takeUntil,
  finalize,
  concatMap,
  filter,
} from 'rxjs';
import { ManagerUserService } from 'src/app/shared/api/managerUser/managerUser.service';
import { GetUsersResult as Users } from 'src/app/shared/api/user/user.interface';
import { UserService } from 'src/app/shared/api/user/user.service';
import {
  BaseDataTable,
  DataTableInfo,
  InitDataTableFunction,
} from 'src/app/shared/component/data-table/data.table.model';
import { UserInfoService } from 'src/app/shared/service/userInfo/userInfo.service';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-user-setting',
  templateUrl: './user-setting.component.html',
  styleUrls: ['./user-setting.component.scss'],
})
export class UserSettingComponent
  extends BaseDataTable<Users>
  implements OnInit, OnDestroy, InitDataTableFunction<Users>
{
  constructor(
    private userService: UserService,
    private swalService: SwalService,
    private apiService: ApiService,
    private dialog: MatDialog,
    private sharedService: SharedService
  ) {
    super();
    this.columns = this.createColumns();
    this.getUsers();
  }
  private destroy$: Subject<any> = new Subject<any>();

  ngOnInit(): void {}
  createColumns(): { key: string; value: string | number }[] {
    const columns = [
      {
        key: 'userName',
        value: '消費者姓名',
      },
      {
        key: 'email',
        value: '電子郵件',
      },
      {
        key: 'birthday',
        value: '消費者生日',
      },
      {
        key: 'flag',
        value: 'Flag',
      },
    ];
    return columns;
  }
  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
  refresh(): void {
    this.getUsers();
  }
  create(): void {
    const dialog = this.dialog.open(FormDialogComponent, {
      data: {
        title: '新建消費者',
        dataList: [
          {
            controlName: 'userName',
            labelText: '消費者姓名',
            type: 'text',
            maxLength: 20,
            valids: [Validators.required, Validators.maxLength(20)],
          },
          {
            controlName: 'account',
            labelText: '消費者帳號',
            type: 'text',
            maxLength: 20,
            valids: [Validators.required, Validators.maxLength(20)],
          },
          {
            controlName: 'password',
            labelText: '消費者密碼',
            type: 'password',
            maxLength: 200,
            valids: [Validators.maxLength(200), Validators.required],
          },
          {
            controlName: 'email',
            labelText: '電子郵件',
            type: 'eamil',
            valids: [Validators.required, Validators.email],
          },
          {
            controlName: 'address',
            labelText: '地址',
            type: 'text',
            maxLength: 250,
            valids: [Validators.maxLength(250)],
          },
          {
            controlName: 'birthday',
            labelText: '生日',
            type: 'date',
            valids: [Validators.required],
          },
          {
            controlName: 'flag',
            labelText: 'Flag',
            type: 'toggle',
            value: true,
          },
        ],
      },
    });
    dialog
      .afterClosed()
      .pipe(
        filter((data) => !this.sharedService.isNullorEmpty(data)),
        concatMap((data) => this.userService.createUser(data)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }
  modify(row: Users): void {
    const dialog = this.dialog.open(FormDialogComponent, {
      data: {
        title: '修改使用者',
        dataList: [
          {
            controlName: 'userName',
            labelText: '使用者姓名',
            type: 'text',
            maxLength: 20,
            value: row.userName,
            valids: [Validators.required, Validators.maxLength(20)],
          },
          {
            controlName: 'account',
            labelText: '使用者帳號',
            type: 'text',
            maxLength: 20,
            value: row.account,
            valids: [Validators.required, Validators.maxLength(20)],
          },
          {
            controlName: 'password',
            labelText: '使用者密碼',
            type: 'password',
            maxLength: 200,
            value: row.password,
            valids: [Validators.maxLength(200), Validators.required],
          },
          {
            controlName: 'email',
            labelText: '電子郵件',
            type: 'eamil',
            value: row.email,
            valids: [Validators.required, Validators.email],
          },
          {
            controlName: 'address',
            labelText: '地址',
            type: 'text',
            maxLength: 250,
            value: row.address,
            valids: [Validators.maxLength(250)],
          },
          {
            controlName: 'birthday',
            labelText: '生日',
            type: 'date',
            value: row.birthday,
            valids: [Validators.required],
          },
          {
            controlName: 'flag',
            labelText: 'Flag',
            type: 'toggle',
            value: row.flag === 'Y' ? true : false,
          },
        ],
      },
    });
    dialog
      .afterClosed()
      .pipe(
        filter((data) => !this.sharedService.isNullorEmpty(data)),
        concatMap((data) => this.userService.modifyUser(row.userId, data)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }
  delete(row: Users): void {
    this.swalService
      .delete()
      .pipe(
        map(() => row.userId),
        concatMap((userId) => this.userService.deleteUser(userId)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }

  multipleDelete(rows: Users[]): void {
    this.swalService
      .multipleDelete(rows)
      .pipe(
        map(() => rows.map((a) => a.userId)),
        concatMap((deleteArr) => this.userService.batchDeleteUsers(deleteArr)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }
  filterTable(info: DataTableInfo): void {
    this.getUsers(info);
  }
  getUsers(info?: DataTableInfo) {
    this.setLoading(true);
    this.userService
      .getUsers(info)
      .pipe(
        map((res) => res.data),
        takeUntil(this.destroy$),
        finalize(() => this.setLoading(false))
      )
      .subscribe((res) => {
        this.tableDataList = res.tableDataList;
        this.tableTotalCount = res.tableTotalCount;
      });
  }
}
