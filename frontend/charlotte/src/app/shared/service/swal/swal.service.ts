import { Injectable } from '@angular/core';
import { filter, finalize, from, Observable, take } from 'rxjs';
import Swal, { SweetAlertResult } from 'sweetalert2';
import { SwalModel } from './swal.interface';

@Injectable({
  providedIn: 'root'
})
export class SwalService<T = null> {

  /**
   * 跳出swalAlert
   * @param options 設定
   * @returns swalalert觀察者物件
   */
  alert(options: SwalModel): Observable<SweetAlertResult<T>>{
    const config = {
      title : options.title,
      text: options.text,
      icon: options.icon,
      showCancelButton: options.showCancelButton,
      confirmButtonText: options.confirmButtonText,
      cancelButtonText: options.cancelButtonText,
      heightAuto: false
    }
    return from(Swal.fire(config))
  }


  /**
   * 刪除alert
   * @returns swalalert觀察者物件
   */
  delete(options?: SwalModel): Observable<SweetAlertResult<T>>{
    return this.alert({
      text: options?.text ?? '確定要刪除這筆資料嗎?',
      icon: options?.icon ?? 'warning',
      showCancelButton: options?.showCancelButton ?? true,
      confirmButtonText: options?.confirmButtonText ?? '確認',
      cancelButtonText: options?.cancelButtonText ?? '取消'
    }).pipe(
      filter(data=> data.isConfirmed)
    )
  }

  /**
   * 多選刪除alert
   * @returns swalalert觀察者物件
   */
  multipleDelete(rows: any[], options?: SwalModel): Observable<SweetAlertResult<T>>{
    console.log(rows)
    return this.alert({
      text: options?.text ?? `確定要刪除這${rows.length}筆資料嗎?`,
      icon: options?.icon ?? 'warning',
      showCancelButton: options?.showCancelButton ?? true,
      confirmButtonText: options?.confirmButtonText ?? '確認',
      cancelButtonText: options?.cancelButtonText ?? '取消'
    }).pipe(
      filter(data=> data.isConfirmed)
    )
  }
}
