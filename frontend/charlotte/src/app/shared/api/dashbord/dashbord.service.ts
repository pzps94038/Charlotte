import { Observable } from 'rxjs';
import { ApiUrl } from './../api.url';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResultModel } from '../api.interface';
import { Chart } from 'angular-highcharts';

@Injectable({
  providedIn: 'root',
})
export class DashbordService {
  constructor(private http: HttpClient) {}

  /** 取得本週銷售產品類型分布 */
  getWeekSale(): Observable<ResultModel<Highcharts.Options>> {
    return this.http.get<ResultModel<Highcharts.Options>>(
      ApiUrl.dashbordWeekSale
    );
  }

  /** ˇ取得本週註冊會員數量 */
  getRegisteredMember(): Observable<ResultModel<Highcharts.Options>> {
    return this.http.get<ResultModel<Highcharts.Options>>(
      ApiUrl.dashbordRegisteredMember
    );
  }

  /** 取得本月銷售數量 */
  getMonthSale(): Observable<ResultModel<Highcharts.Options>> {
    return this.http.get<ResultModel<Highcharts.Options>>(
      ApiUrl.dashbordMonthSale
    );
  }
}
