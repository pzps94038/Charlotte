import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataTableInfo } from '../component/data-table/data.table.model';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor() { }

  /**
   * 判斷資料是不是空字串、或未產生的
   * @param data 資料
   * @returns 判斷
   */
  isNullorEmpty(data: any): boolean{
    if(data === "" || data === undefined || data === null)
      return true
    else return false
  }

  /**
   * 創建DataTale查詢Params
   * @param info 查詢參數
   * @returns Params
   */
  createDataTableParams(info?: DataTableInfo): HttpParams{
    let params = new HttpParams()
    if(info){
      if(info.page)
        params = params.set('limit', info.page.limit).set('offset', info.page.offset)
      if(info.sort?.active && info.sort?.direction)
        params = params.set('orderBy', info.sort.active).set('orderDescription', info.sort.direction)
      if(info.filterStr)
        params = params.set('filterStr', info.filterStr)
    }
    else
      params = params.set('limit', 10).set('offset', 0)
    return params
  }
}
