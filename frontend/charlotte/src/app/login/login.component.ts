import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, of, switchMap, throwError } from 'rxjs';
import { UserService } from '../shared/api/user/user.service';
import { SwalService } from '../shared/service/swal/swal.service';
import { TokenService } from '../shared/service/token/token.service';
import { UserInfoService } from '../shared/service/userInfo/userInfo.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form : FormGroup = this.fb.group({
    account:['', Validators.required],
    password:['', Validators.required]
  })
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private swalService: SwalService,
    private tokenService: TokenService,
    private userInfoService: UserInfoService,
    private router: Router
    ) { }

  ngOnInit(): void {}
  login(): void{
    if(this.form.valid){
      const data = {
        account: this.form.value.account,
        password: this.form.value.password
      }
      this.router.navigate(['/siteMap/home'])
      // this.userService.login(data).pipe(
      //   switchMap(res=> res.code == 200 ? of(res.data) : throwError(()=> new Error(res.message))),
      //   catchError(err=>{
      //     this.swalService.alert({
      //       text: err.message,
      //       icon: 'error',
      //       confirmButtonText: '確認'
      //     })
      //     throw err
      //   })
      // ).subscribe(data=>{
      //   this.tokenService.saveToken({accessToken: data.token.accessToken, refreshToken: data.token.refreshToken })
      //   this.userInfoService.saveUserInfo({managerUserId: data.managerUserId})
      //   this.router.navigate(['/siteMap/home'])
      // })
    }
  }
}
