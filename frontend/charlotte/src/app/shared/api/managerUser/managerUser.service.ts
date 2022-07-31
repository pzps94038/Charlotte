import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataTableInfo } from '../../component/data-table/data.table.model';
import { SharedService } from '../../service/shared.service';
import { DataTable, ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import {
  CreateManagerUserRequest,
  LoginRequest,
  LoginResult,
  ModifyManagerUserRequest,
  GetManagerUserResult,
  GetManagerUsersResult,
  ModfiyManagerUserPasswordRequest,
} from './managerUser.interface';

@Injectable({
  providedIn: 'root',
})
export class ManagerUserService {
  constructor(private http: HttpClient, private sharedService: SharedService) {}

  /* 登入 */
  login(data: LoginRequest): Observable<ResultModel<LoginResult>> {
    return this.http.post<ResultModel<LoginResult>>(ApiUrl.login, data);
  }

  /* 取得使用者資訊 */
  getUser(
    managerUserId: number
  ): Observable<ResultModel<GetManagerUserResult>> {
    return this.http.get<ResultModel<GetManagerUserResult>>(
      `${ApiUrl.managerUser}\\${managerUserId}`,
      { params: { managerUserId: managerUserId } }
    );
  }

  /**
   * 取得多個使用者資訊
   * @returns 多個使用者資訊
   */
  getUsers(
    info?: DataTableInfo
  ): Observable<ResultModel<DataTable<GetManagerUsersResult>>> {
    const params = this.sharedService.createDataTableParams(info);
    return this.http.get<ResultModel<DataTable<GetManagerUsersResult>>>(
      ApiUrl.managerUser,
      {
        params,
      }
    );
  }

  /**
   * 創建使用者
   * @param req 使用者資訊
   * @returns 成功與否訊息
   */
  createUser(req: CreateManagerUserRequest): Observable<ResultMessage> {
    return this.http.post<ResultMessage>(ApiUrl.managerUser, req);
  }

  /* 更新部分使用者資訊 */
  modifyUser(
    managerUserId: number,
    userData: ModifyManagerUserRequest
  ): Observable<ResultMessage> {
    return this.http.patch<ResultMessage>(
      `${ApiUrl.managerUser}\\${managerUserId}`,
      userData
    );
  }

  /** 刪除使用者 */
  deleteUser(managerUserId: number): Observable<ResultMessage> {
    return this.http.delete<ResultMessage>(
      `${ApiUrl.managerUser}\\${managerUserId}`
    );
  }

  /** 刪除多個使用者 */
  batchDeleteUsers(req: number[]): Observable<ResultMessage> {
    return this.http.delete<ResultMessage>(`${ApiUrl.managerUser}`, {
      body: req,
    });
  }

  modifyPassword(
    managerUserId: number,
    req: ModfiyManagerUserPasswordRequest
  ): Observable<ResultMessage> {
    return this.http.put<ResultMessage>(
      `${ApiUrl.managerUser}\\${managerUserId}`,
      req
    );
  }
}
