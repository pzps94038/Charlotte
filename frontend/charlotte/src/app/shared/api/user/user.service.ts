import { SharedService } from './../../service/shared.service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataTable, ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import {
  CreateUserRequest,
  GetUsersResult,
  ModifyUserRequest,
} from './user.interface';
import { Observable } from 'rxjs';
import { DataTableInfo } from '../../component/data-table/data.table.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private sharedService: SharedService) {}

  /**
   * 取得多個使用者資訊
   * @returns 多個使用者資訊
   */
  getUsers(
    info?: DataTableInfo
  ): Observable<ResultModel<DataTable<GetUsersResult>>> {
    const params = this.sharedService.createDataTableParams(info);
    return this.http.get<ResultModel<DataTable<GetUsersResult>>>(ApiUrl.user, {
      params,
    });
  }

  /**
   * 創建使用者
   * @param req 使用者資訊
   * @returns 成功與否訊息
   */
  createUser(req: CreateUserRequest): Observable<ResultMessage> {
    return this.http.post<ResultMessage>(ApiUrl.user, req);
  }

  /* 更新部分使用者資訊 */
  modifyUser(
    userId: number,
    userData: ModifyUserRequest
  ): Observable<ResultMessage> {
    return this.http.patch<ResultMessage>(
      `${ApiUrl.user}\\${userId}`,
      userData
    );
  }

  /** 刪除使用者 */
  deleteUser(userId: number): Observable<ResultMessage> {
    return this.http.delete<ResultMessage>(`${ApiUrl.user}\\${userId}`);
  }

  /** 刪除多個使用者 */
  batchDeleteUsers(req: number[]): Observable<ResultMessage> {
    return this.http.delete<ResultMessage>(`${ApiUrl.user}`, {
      body: req,
    });
  }
}
