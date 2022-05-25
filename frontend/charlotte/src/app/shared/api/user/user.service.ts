import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataTableInfo } from '../../component/data-table/data.table.model';
import { SharedService } from '../../service/shared.service';
import { DataTable, ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import { CreateUserRequest, LoginRequest, LoginResult, ModifyUserRequest, GetUserResult, GetUsersResult, ModfiyUserPasswordRequest } from './user.interface';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) { }

  /* 登入 */
  login(data: LoginRequest): Observable<ResultModel<LoginResult>> {
    return this.http.post<ResultModel<LoginResult>>(ApiUrl.login, data)
  }

  /* 取得使用者資訊 */
  getUser(userId: number): Observable<ResultModel<GetUserResult>>{
    return this.http.get<ResultModel<GetUserResult>>(`${ApiUrl.user}\\${userId}`,{params: {managerUserId: userId}});
  }

  /**
   * 取得多個使用者資訊
   * @returns 多個使用者資訊
   */
  getUsers(info?: DataTableInfo): Observable<ResultModel<DataTable<GetUsersResult>>>{
    const params = this.sharedService.createDataTableParams(info);
    return this.http.get<ResultModel<DataTable<GetUsersResult>>>(ApiUrl.user, {
      params
    })
  }

  /**
   * 創建使用者
   * @param req 使用者資訊
   * @returns 成功與否訊息
   */
  createUser(req: CreateUserRequest): Observable<ResultMessage>{
    return this.http.post<ResultMessage>(ApiUrl.user, req)
  }

  /* 更新部分使用者資訊 */
  modifyUser(userId: number, userData: ModifyUserRequest): Observable<ResultMessage>{
    return this.http.patch<ResultMessage>(`${ApiUrl.user}\\${userId}`, userData)
  }

  /** 刪除使用者 */
  deleteUser(userId: number): Observable<ResultMessage>{
    return this.http.delete<ResultMessage>(`${ApiUrl.user}\\${userId}`)
  }

  /** 刪除多個使用者 */
  batchDeleteUsers(req: number[]): Observable<ResultMessage>{
    return this.http.delete<ResultMessage>(`${ApiUrl.user}`,{
      body: req
    })
  }

  modifyPassword(userId:number, req: ModfiyUserPasswordRequest): Observable<ResultMessage>{
    return this.http.put<ResultMessage>(`${ApiUrl.user}\\${userId}`,req)
  }

}
