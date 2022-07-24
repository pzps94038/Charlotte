import { HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { catchError, finalize, map, Observable, concatMap, throwError, tap } from 'rxjs';
import { EncryptService } from '../service/encrypt/encrypt.service';
import { LogoutService } from '../service/logout/logout.service';
import { TokenService } from '../service/token/token.service';
import { UserInfoService } from '../service/userInfo/userInfo.service';
import { ApiUrl } from './api.url';
import { TokenService as TokenApiService } from './token/token.service';


@Injectable({
  providedIn: 'root'
})
export class Interceptor implements HttpInterceptor{
  private taskCount: number = 0 // api Loading count
  constructor
  (
    private spinner: NgxSpinnerService,
    private tokenService: TokenService,
    private tokenApiService: TokenApiService,
    private userInfoService: UserInfoService,
    private snackBar: MatSnackBar,
    private logoutService: LogoutService,
    private encryptService: EncryptService
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if(this.taskCount == 0) this.spinner.show()
    this.taskCount++;
    const accessToken = this.tokenService.getToken().accessToken
    let headers = new HttpHeaders({
      'Access-Control-Allow-Origin': ApiUrl.baseUrl,
      'Authorization': `Bearer ${accessToken}`
    })

    if((req.body instanceof FormData) === false)
      headers = headers.append('Content-Type', 'application/json')  
      
    let modifyReq: HttpRequest<any> = req.clone({ headers })
    return next.handle(modifyReq).pipe(
      catchError((err: HttpErrorResponse)=>{
        if(err.status === 401)
          return this.refreshToken(next, modifyReq)
        else
          return throwError(()=> new Error("NetWork error"))
      }),
      finalize(()=> {
        this.taskCount--;
        if(this.taskCount == 0)
          this.spinner.hide()
      })
    )
  }

  refreshToken(next: HttpHandler, req: HttpRequest<any>): Observable<HttpEvent<any>>{
    const userId = this.userInfoService.getUserInfo().managerUserId
    const refreshToken = this.tokenService.getToken().refreshToken
    return this.tokenApiService.refreshToken({ refreshToken, userId }).pipe(
      map(res=> res.data),
      concatMap((data)=> {
        this.tokenService.saveToken(data)
        const headers = req.headers.set('Authorization', `Bearer ${data.accessToken}`)
        const newReq = req.clone({ headers })
        return next.handle(newReq)
      }),
      catchError(()=>{
        this.snackBar.open('身分驗證以過期，請重新登入', '關閉')
        this.logoutService.logout()
        return throwError(()=> new Error('Authorization Expired'))
      })
    )
  }
}
