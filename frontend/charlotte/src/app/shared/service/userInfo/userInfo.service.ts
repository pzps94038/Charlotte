import { Injectable } from '@angular/core';
import { UserInfo } from './userInfo.interface';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {

  constructor() { }
  saveUserInfo(userInfo: UserInfo): void {
    localStorage.setItem('UserInfo', JSON.stringify(userInfo))
  }
  getUserInfo():UserInfo{
    let userInfo = localStorage.getItem('UserInfo')!
    return JSON.parse(userInfo)
  }
}
