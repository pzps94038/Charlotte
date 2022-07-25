import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map, filter, Subject, takeUntil } from 'rxjs';
import { ApiService } from 'src/app/shared/api/api.service';
import { UserService } from 'src/app/shared/api/user/user.service';
import { UserInfoService } from 'src/app/shared/service/userInfo/userInfo.service';

@Component({
  selector: 'app-basic-data-setting',
  templateUrl: './basic-data-setting.component.html',
  styleUrls: ['./basic-data-setting.component.scss'],
})
export class BasicDataSettingComponent implements OnInit, OnDestroy {
  form: FormGroup = this.fb.group({
    userName: ['', [Validators.required, Validators.maxLength(20)]],
    email: ['', [Validators.required, Validators.email]],
    address: [''],
    birthday: ['', Validators.required],
  });
  destroy$: Subject<any> = new Subject<any>();
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private userInfoService: UserInfoService,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    this.init();
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
  /* 初始化 */
  init() {
    const userId = this.userInfoService.getUserInfo().managerUserId;
    this.userService
      .getUser(userId)
      .pipe(map((res) => res.data))
      .subscribe((data) => {
        this.setFormValue(
          data.userName,
          data.email,
          data.address,
          data.birthday
        );
      });
  }

  /**
   * 設定Form值
   * @param userName{使用者名稱}
   * @param email{使用者Email}
   * @param address{使用者地址}
   * @param birthday{使用者生日}
   */
  setFormValue(
    userName: string,
    email: string,
    address: string | null,
    birthday: Date
  ) {
    this.form.controls['userName'].setValue(userName);
    this.form.controls['email'].setValue(email);
    this.form.controls['address'].setValue(address);
    this.form.controls['birthday'].setValue(birthday);
  }

  /* 保存使用者資訊 */
  save() {
    const valid = this.form.valid;
    this.form.markAllAsTouched();
    if (valid) {
      const userId = this.userInfoService.getUserInfo().managerUserId;
      this.userService
        .modifyUser(userId, this.form.value)
        .pipe(
          filter((res) => this.apiService.judgeSuccess(res, true)),
          takeUntil(this.destroy$)
        )
        .subscribe(() => this.init());
    }
  }

  /* 重設值 */
  reset() {
    this.init();
  }
}
