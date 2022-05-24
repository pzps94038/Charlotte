import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataTableInfo } from '../../component/data-table/data.table.interface';
import { SharedService } from '../../service/shared.service';
import { DataTable, ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import { CreateProductTypeRequest, GetProductTypesResult, ModifyProductTypeRequest } from './product-type.interface';

@Injectable({
  providedIn: 'root'
})
export class ProductTypeService {

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) { }

  /**
   * 取得產品類型
   * @param info 表格資訊
   * @returns
   */
  getProductTypes(info? : DataTableInfo): Observable<ResultModel<DataTable<GetProductTypesResult>>>{
    const params = this.sharedService.createDataTableParams(info)
    return this.http.get<ResultModel<DataTable<GetProductTypesResult>>>(ApiUrl.productType, {
      params
    })
  }

  /**
   * 創建產品類型
   * @param req 產品類型資料
   * @returns 成功與否
   */
  createProductType(req: CreateProductTypeRequest): Observable<ResultMessage>{
    return this.http.post<ResultMessage>(ApiUrl.productType, req)
  }

  /**
   * 修改產品類型
   * @param productTypeId 產品類型ID
   * @param req 產品類型資訊
   * @returns 成功與否
   */
  modifyProductType(productTypeId: number, req: ModifyProductTypeRequest){
    return this.http.patch<ResultMessage>(`${ApiUrl.productType}\\${productTypeId}`, req)
  }

  /**
   * 刪除產品類型
   * @param productTypeId 產品類型ID
   * @returns 成功與否
   */
  deleteProductType(productTypeId: number): Observable<ResultMessage>{
    return this.http.delete<ResultMessage>(`${ApiUrl.productType}\\${productTypeId}`)
  }

  /**
   * 批次刪除產品類型
   * @param req 多個產品類型ID
   * @returns 成功與否
   */
  batchDeleteProductType(req: number[]){
    return this.http.delete<ResultMessage>(ApiUrl.productType, {
      body: req
    });
  }
}
