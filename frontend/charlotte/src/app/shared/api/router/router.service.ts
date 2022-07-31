import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataTableInfo } from '../../component/data-table/data.table.model';
import { SharedService } from '../../service/shared.service';
import { DataTable, ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import {
  CreateRouterRequest,
  GetRouterResult,
  ModifyRouterRequest,
} from './router.interface';

@Injectable({
  providedIn: 'root',
})
export class RouterService {
  constructor(private http: HttpClient, private sharedService: SharedService) {}

  /**
   * 取得路由清單
   * @returns 路由清單
   */
  getRouters(
    info?: DataTableInfo
  ): Observable<ResultModel<DataTable<GetRouterResult>>> {
    let params = this.sharedService.createDataTableParams(info);
    return this.http.get<ResultModel<DataTable<GetRouterResult>>>(
      ApiUrl.router,
      {
        params,
      }
    );
  }

  /**
   * 創建路由
   * @param req 路由資料
   * @returns 成功失敗訊息
   */
  createRouter(req: CreateRouterRequest): Observable<ResultMessage> {
    return this.http.post<ResultMessage>(ApiUrl.router, req);
  }
  /**
   * 修改路由
   * @param routerId 路由ID
   * @param req 路由資料
   * @returns 成功失敗訊息
   */
  modifyRouter(
    routerId: number,
    req: ModifyRouterRequest
  ): Observable<ResultMessage> {
    return this.http.patch<ResultMessage>(`${ApiUrl.router}\\${routerId}`, req);
  }

  /**
   * 刪除路由
   * @param routerId 路由ID
   * @returns 成功失敗訊息
   */
  deleteRouter(routerId: number): Observable<ResultMessage> {
    return this.http.delete<ResultMessage>(`${ApiUrl.router}\\${routerId}`);
  }

  /**
   * 批次刪除路由
   * @param req 路由ID的集合
   * @returns 成功失敗訊息
   */
  batchDeleteRouter(req: number[]) {
    return this.http.delete<ResultMessage>(ApiUrl.router, {
      body: req,
    });
  }
}
