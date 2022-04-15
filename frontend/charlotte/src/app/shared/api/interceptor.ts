import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { catchError, finalize, map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Interceptor implements HttpInterceptor{
  private taskCount: number = 0
  constructor(private spinner: NgxSpinnerService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if(this.taskCount == 0) this.spinner.show()
    this.taskCount++;
    return next.handle(req).pipe(
      map((event: HttpEvent<any>) =>{
        return event;
      }),
      catchError(()=>{
        throw new Error("網路發生異常")
      }),
      finalize(()=> {
        this.taskCount--;
        if(this.taskCount == 0)this.spinner.hide()
      })
    )
  }
}
