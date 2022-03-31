import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import { LoginReq, LoginRes, UserReq, UserRes } from './user.interface';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  /* 登入 */
  public login(data: LoginReq): Observable<ResultModel<LoginRes>> {
    return this.http.post<ResultModel<LoginRes>>(ApiUrl.login, data)
  }
  /* 取得使用者資訊 */
  public getUser(userId: number): Observable<ResultModel<UserRes>>{
    return this.http.get<ResultModel<UserRes>>(`${ApiUrl.user}\\${userId}`,{params: {managerUserId: userId}});
  }
  /* 更新部分使用者資訊 */
  public modifyUser(userId: number, userData: UserReq): Observable<ResultMessage>{
    return this.http.patch<ResultMessage>(`${ApiUrl.user}\\${userId}`, userData)
  }
}
