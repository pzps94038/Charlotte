import { ApiService } from './../../shared/api/api.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map, filter } from 'rxjs';
import { UserService } from 'src/app/shared/api/user/user.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';
import { UserInfoService } from 'src/app/shared/service/userInfo/userInfo.service';

@Component({
  selector: 'app-basic-data-setting',
  templateUrl: './basic-data-setting.component.html',
  styleUrls: ['./basic-data-setting.component.scss']
})
export class BasicDataSettingComponent implements OnInit {
  form: FormGroup = this.fb.group({
    UserName: ['', [Validators.required, Validators.maxLength(20)]],
    Email: ['', [Validators.required, Validators.email]],
    Address: [''],
    Birthday: ['',Validators.required],
  })
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private userInfoService: UserInfoService,
    private swalService: SwalService<null>,
    private apiService: ApiService
    ) {  }

  ngOnInit(): void { this.init() }

  /* 初始化 */
  init(){
    const userId =  this.userInfoService.getUserInfo().managerUserId
    this.userService.getUser(userId).pipe(
        map(res=> res.data))
      .subscribe((data)=>{
        this.setFormValue(data.userName, data.email, data.address,data.birthday)
      })
  }

  /**
   * 設定Form值
   * @param userName{使用者名稱}
   * @param email{使用者Email}
   * @param address{使用者地址}
   * @param birthday{使用者生日}
   */
  setFormValue(userName: string, email: string, address: string | null, birthday: Date){
    this.form.controls['UserName'].setValue(userName)
    this.form.controls['Email'].setValue(email)
    this.form.controls['Address'].setValue(address)
    this.form.controls['Birthday'].setValue(birthday)
  }

   /* 保存使用者資訊 */
  save(){
    const valid = this.form.valid
    if(valid){
      const userId =  this.userInfoService.getUserInfo().managerUserId
      this.userService.modifyUser(userId, this.form.value)
      .pipe(
        filter(res=> this.apiService.judgeSuccess(res, true)
      )).subscribe(()=>{
        this.init()
      })
    }
  }

  /* 重設值 */
  reset(){
    this.init()
  }
}
