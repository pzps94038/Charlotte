import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataTableInfo } from '../../component/data-table/data.table.model';
import { SharedService } from '../../service/shared.service';
import { DataTable, ResultMessage, ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import {
  CreateProductRequest,
  GetProductResult,
  ModifyProductRequest,
} from './product.interface';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private sharedService: SharedService, private http: HttpClient) {}

  /**
   * 取得產品
   * @param info 表格資訊
   * @returns
   */
  getProducts(
    info?: DataTableInfo
  ): Observable<ResultModel<DataTable<GetProductResult>>> {
    const params = this.sharedService.createDataTableParams(info);
    return this.http.get<ResultModel<DataTable<GetProductResult>>>(
      ApiUrl.product,
      {
        params,
      }
    );
  }

  /**
   * 創建產品
   * @param request 產品資訊
   * @returns 成功與否
   */
  createProduct(request: CreateProductRequest): Observable<ResultMessage> {
    return this.http.post<ResultMessage>(ApiUrl.product, request);
  }

  /**
   * 上傳產品圖片
   * @param formData 產品圖片
   * @returns 成功與否
   */
  fileUpload(files: FormData): Observable<ResultModel<string>> {
    files.forEach((file) => console.log(file));
    return this.http.post<ResultModel<string>>(ApiUrl.productFileUpload, files);
  }

  /**
   * 修改產品
   * @param productId 產品ID
   * @param request 產品資訊
   * @returns 成功與否
   */
  modifyProduct(
    productId: number,
    request: ModifyProductRequest
  ): Observable<ResultMessage> {
    return this.http.patch<ResultMessage>(
      `${ApiUrl.product}\\${productId}`,
      request
    );
  }

  /**
   * 刪除產品
   * @param productId 產品ID
   * @returns 成功與否
   */
  deleteProduct(productId: number): Observable<ResultMessage> {
    return this.http.delete<ResultMessage>(`${ApiUrl.product}\\${productId}`);
  }

  /**
   * 批次刪除產品
   * @param request 多個產品ID
   * @returns 成功與否
   */
  batchDeleteProduct(request: number[]) {
    return this.http.delete<ResultMessage>(ApiUrl.product, {
      body: request,
    });
  }
}
