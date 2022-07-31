import { RoleService } from 'src/app/shared/api/role/role.service';
import { ManagerUserService } from 'src/app/shared/api/managerUser/managerUser.service';
import {
  map,
  Observable,
  filter,
  takeUntil,
  Subject,
  concatMap,
  tap,
  BehaviorSubject,
  finalize,
  switchMap,
} from 'rxjs';
import { Component, OnInit, OnDestroy } from '@angular/core';
import {
  BaseDataTable,
  DataTableInfo,
  InitDataTableFunction,
} from 'src/app/shared/component/data-table/data.table.model';
import { GetManagerUsersResult as Users } from 'src/app/shared/api/managerUser/managerUser.interface';
import { MatDialog } from '@angular/material/dialog';
import { SwalService } from 'src/app/shared/service/swal/swal.service';
import { FormDialogComponent } from 'src/app/shared/dialog/form-dialog/form-dialog.component';
import { Validators } from '@angular/forms';
import { ApiService } from 'src/app/shared/api/api.service';
import { SharedService } from 'src/app/shared/service/shared.service';

@Component({
  selector: 'app-manager-user-setting',
  templateUrl: './manager-user-setting.component.html',
  styleUrls: ['./manager-user-setting.component.scss'],
})
export class ManagerUserSettingComponent
  extends BaseDataTable<Users>
  implements OnInit, OnDestroy, InitDataTableFunction<Users>
{
  constructor(
    private userService: ManagerUserService,
    private roleService: RoleService,
    private dialog: MatDialog,
    private swalService: SwalService<null>,
    private apiService: ApiService,
    private sharedService: SharedService
  ) {
    super();
    this.columns = this.createColumns();
    this.getUsers();
  }

  private destroy$ = new Subject<any>();

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }

  ngOnInit(): void {}
  createColumns(): { key: string; value: string | number }[] {
    const columns = [
      {
        key: 'userName',
        value: '使用者姓名',
      },
      {
        key: 'email',
        value: '電子郵件',
      },
      {
        key: 'flag',
        value: 'Flag',
      },
      {
        key: 'roleName',
        value: '角色名稱',
      },
    ];
    return columns;
  }

  refresh(): void {
    this.getUsers();
  }

  create(): void {
    const options$ = this.createRoleOptions();
    options$
      .pipe(
        concatMap((options) => {
          const dialog = this.dialog.open(FormDialogComponent, {
            data: {
              title: '新建使用者',
              dataList: [
                {
                  controlName: 'userName',
                  labelText: '使用者姓名',
                  type: 'text',
                  maxLength: 20,
                  valids: [Validators.required, Validators.maxLength(20)],
                },
                {
                  controlName: 'account',
                  labelText: '使用者帳號',
                  type: 'text',
                  maxLength: 20,
                  valids: [Validators.required, Validators.maxLength(20)],
                },
                {
                  controlName: 'password',
                  labelText: '使用者密碼',
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
                  controlName: 'roleId',
                  labelText: '角色',
                  type: 'select',
                  options: options,
                  value: '',
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
          return dialog.afterClosed();
        }),
        filter((data) => !this.sharedService.isNullorEmpty(data)),
        concatMap((data) => this.userService.createUser(data)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }

  modify(row: Users): void {
    const options$ = this.createRoleOptions();
    options$
      .pipe(
        concatMap((options) => {
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
                  controlName: 'roleId',
                  labelText: '角色',
                  type: 'select',
                  options: options,
                  value: row.roleId,
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
          return dialog.afterClosed();
        }),
        filter((data) => !this.sharedService.isNullorEmpty(data)),
        concatMap((data) =>
          this.userService.modifyUser(row.managerUserId, data)
        ),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => this.refresh());
  }

  delete(row: Users): void {
    this.swalService
      .delete()
      .pipe(
        concatMap(() => this.userService.deleteUser(row.managerUserId)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.refresh();
      });
  }

  multipleDelete(rows: Users[]): void {
    this.swalService
      .multipleDelete(rows)
      .pipe(
        map(() => rows.map((a) => a.managerUserId)),
        concatMap((deleteArr) => this.userService.batchDeleteUsers(deleteArr)),
        filter((res) => this.apiService.judgeSuccess(res, true)),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.refresh();
      });
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

  createRoleOptions(): Observable<{ text: string; value: number }[]> {
    return this.roleService.getRoles().pipe(
      map((res) =>
        res.data.tableDataList.map((a) => {
          return { text: a.roleName, value: a.roleId };
        })
      ),
      takeUntil(this.destroy$)
    );
  }

  filterTable(info: DataTableInfo): void {
    this.getUsers(info);
  }
}
