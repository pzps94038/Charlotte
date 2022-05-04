import { Injectable } from '@angular/core';
import { finalize, from, Observable, take } from 'rxjs';
import Swal, { SweetAlertResult } from 'sweetalert2';
import { SwalModel } from './swal.interface';

@Injectable({
  providedIn: 'root'
})
export class SwalService<T = null> {

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
}
