import { ApiService } from './../shared/api/api.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  catchError,
  of,
  Subject,
  switchMap,
  throwError,
  filter,
  map,
} from 'rxjs';
import { ManagerUserService } from '../shared/api/managerUser/managerUser.service';
import { SwalService } from '../shared/service/swal/swal.service';
import { TokenService } from '../shared/service/token/token.service';
import { UserInfoService } from '../shared/service/userInfo/userInfo.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  form: FormGroup = this.fb.group({
    account: ['', Validators.required],
    password: ['', Validators.required],
  });
  constructor(
    private fb: FormBuilder,
    private userService: ManagerUserService,
    private tokenService: TokenService,
    private userInfoService: UserInfoService,
    private router: Router,
    private apiService: ApiService
  ) {
    const isToken = this.tokenService.checkToken();
    if (isToken) {
      this.router.navigate(['/siteMap/home']);
    }
  }

  ngOnInit(): void {}
  login(): void {
    this.form.markAllAsTouched();
    if (this.form.valid) {
      const data = {
        account: this.form.value.account,
        password: this.form.value.password,
      };
      this.userService
        .login(data)
        .pipe(
          filter((res) => this.apiService.judgeSuccess(res)),
          map((res) => res.data)
        )
        .subscribe((data) => {
          this.tokenService.saveToken({
            accessToken: data.token.accessToken,
            refreshToken: data.token.refreshToken,
          });
          this.userInfoService.saveUserInfo({
            managerUserId: data.managerUserId,
          });
          this.router.navigate(['/siteMap/home']);
        });
    }
  }
}
