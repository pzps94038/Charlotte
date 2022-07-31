import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import { GetOrderDetail } from './orderDetail.interface';

@Injectable({
  providedIn: 'root',
})
export class OrderDetailService {
  constructor(private http: HttpClient) {}

  /**
   * 取得訂單
   * @param info 表格資訊
   * @returns
   */
  getOrderDetail(orderId: number): Observable<ResultModel<GetOrderDetail>> {
    return this.http.get<ResultModel<GetOrderDetail>>(
      `${ApiUrl.orderDetail}\\${orderId}`
    );
  }
}
