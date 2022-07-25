import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataTableInfo } from '../../component/data-table/data.table.model';
import { SharedService } from '../../service/shared.service';
import { DataTable, ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import { GetOrdersResult } from './order.interface';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private http: HttpClient, private sharedService: SharedService) {}

  /**
   * 取得訂單
   * @param info 表格資訊
   * @returns
   */
  getOrders(
    info?: DataTableInfo
  ): Observable<ResultModel<DataTable<GetOrdersResult>>> {
    const params = this.sharedService.createDataTableParams(info);
    return this.http.get<ResultModel<DataTable<GetOrdersResult>>>(
      ApiUrl.order,
      {
        params,
      }
    );
  }
  /**
   * 刪除訂單
   * @param productId 訂單ID
   * @returns 成功與否
   */
  deleteOrder(orderId: number): Observable<ResultMessage> {
    return this.http.delete<ResultMessage>(`${ApiUrl.order}\\${orderId}`);
  }

  /**
   * 批次刪除訂單
   * @param request 多個產品ID
   * @returns 成功與否
   */
  batchDeleteOrders(request: number[]) {
    return this.http.delete<ResultMessage>(ApiUrl.product, {
      body: request,
    });
  }
}
