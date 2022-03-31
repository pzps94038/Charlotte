import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import Swal, { SweetAlertResult } from 'sweetalert2';
import { SwalModel } from './swal.interface';

@Injectable({
  providedIn: 'root'
})
export class SwalService {

  alert(options: SwalModel): Observable<SweetAlertResult<any>>{
    const config = {
      title : options.title,
      text: options.text,
      icon: options.icon,
      showCancelButton: options.showCancelButton,
      confirmButtonText: options.confirmButtonText,
      cancelButtonText: options.cancelButtonText,
      heightAuto: false
    }
    return from(Swal.fire(config)) ;
  }
}
