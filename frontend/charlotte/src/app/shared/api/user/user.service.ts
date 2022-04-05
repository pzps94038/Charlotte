import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import { CreateUserReq, LoginReq, LoginRes, ModifyUserReq, GetUserRes, GetUsersRes } from './user.interface';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }

  /* 登入 */
  login(data: LoginReq): Observable<ResultModel<LoginRes>> {
    return this.http.post<ResultModel<LoginRes>>(ApiUrl.login, data)
  }

  /* 取得使用者資訊 */
  getUser(userId: number): Observable<ResultModel<GetUserRes>>{
    return this.http.get<ResultModel<GetUserRes>>(`${ApiUrl.user}\\${userId}`,{params: {managerUserId: userId}});
  }

  /**
   * 取得多個使用者資訊
   * @returns 多個使用者資訊
   */
  getUsers(): Observable<ResultModel<GetUsersRes[]>>{
    return this.http.get<ResultModel<GetUsersRes[]>>(ApiUrl.user)
  }

  /**
   * 創建使用者
   * @param req 使用者資訊
   * @returns 成功與否訊息
   */
  createUser(req: CreateUserReq): Observable<ResultMessage>{
    return this.http.post<ResultMessage>(ApiUrl.user, req)
  }

  /* 更新部分使用者資訊 */
  modifyUser(userId: number, userData: ModifyUserReq): Observable<ResultMessage>{
    return this.http.patch<ResultMessage>(`${ApiUrl.user}\\${userId}`, userData)
  }

  deleteUser(userId: number): Observable<ResultMessage>{
    return this.http.delete<ResultMessage>(`${ApiUrl.user}\\${userId}`)
  }

  batchDeleteUsers(req: number[]): Observable<ResultMessage>{
    return this.http.delete<ResultMessage>(`${ApiUrl.user}`,{
      body: req
    })
  }
}
