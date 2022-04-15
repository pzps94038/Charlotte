import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { map, switchMap } from 'rxjs';
import { UserService } from 'src/app/shared/api/user/user.service';
import { SwalService } from 'src/app/shared/service/swal/swal.service';
import { UserInfoService } from 'src/app/shared/service/userInfo/userInfo.service';

@Component({
  selector: 'app-user-setting',
  templateUrl: './user-setting.component.html',
  styleUrls: ['./user-setting.component.scss']
})
export class UserSettingComponent implements OnInit {
  ngOnInit(): void {

  }

}
