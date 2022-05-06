import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import { CreateFactoryRequest, GetFactorysResult, ModifyFactoryRequest } from './factory.interface';

@Injectable({
  providedIn: 'root'
})
export class FactoryService {

  constructor(private http: HttpClient) { }

  /** 取得所有廠商資料 */
  getFactorys(): Observable<ResultModel<GetFactorysResult[]>>{
    return this.http.get<ResultModel<GetFactorysResult[]>>(ApiUrl.factory)
  }

  /** 創建廠商 */
  createFactory(req: CreateFactoryRequest): Observable<ResultMessage>{
    return this.http.post<ResultMessage>(ApiUrl.factory, req)
  }

  /** 修改廠商 */
  modifyFactory(factoryId: number, req: ModifyFactoryRequest): Observable<ResultMessage>{
    return this.http.patch<ResultMessage>(`${ApiUrl.factory}\\${factoryId}`, req)
  }

  /** 刪除廠商 */
  deleteFactory(factoryId: number): Observable<ResultMessage>{
    return this.http.delete<ResultMessage>(`${ApiUrl.factory}\\${factoryId}`)
  }

  /** 批次刪除廠商 */
  batchDeleteFactory(req: number[]): Observable<ResultMessage>{
    return this.http.delete<ResultMessage>(ApiUrl.factory,{
      body: req
    })
  }
}
