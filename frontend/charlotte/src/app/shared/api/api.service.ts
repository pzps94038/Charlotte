import { Injectable } from '@angular/core';
import { SwalService } from '../service/swal/swal.service';
import { ResultMessage, ResultModel } from './api.interface';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private swalService: SwalService<null>) { }

  /**
   * 判斷Api回傳Code
   * @param res Api回傳
   * @param successShow 成功需要showDialog嗎?
   * @returns api成功or失敗
   */
  judgeSuccess(res: ResultModel<any> | ResultMessage, successShow: boolean = false){
    if(res.code === 200){
      if(successShow){
        this.swalService.alert({
          text: res.message,
          icon: 'success',
          confirmButtonText: '確認'
        })
      }
      return true
    }
    else{
        this.swalService.alert({
          text: res.message,
          icon: 'error',
          confirmButtonText: '確認'
        })
      return false
    }
  }



}
