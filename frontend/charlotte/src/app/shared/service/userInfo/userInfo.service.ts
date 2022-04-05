import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { UserAuth, UserInfo } from './userInfo.interface';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  UserAuth$: BehaviorSubject<UserAuth> = new BehaviorSubject<UserAuth>({
    create: false,
    modify: false,
    delete: false,
  });
  constructor() { }
  saveUserInfo(userInfo: UserInfo): void {
    localStorage.setItem('UserInfo', JSON.stringify(userInfo))
  }
  getUserInfo():UserInfo{
    let userInfo = localStorage.getItem('UserInfo')!
    return JSON.parse(userInfo)
  }
  getUserAuth(): UserAuth{
    return this.UserAuth$.value
  }
  changeUserAuth(auth: UserAuth){
    this.UserAuth$.next(auth)
  }
}
