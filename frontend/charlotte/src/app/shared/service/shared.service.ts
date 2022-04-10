import { Injectable } from '@angular/core';

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
}
