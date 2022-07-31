import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultModel } from '../api.interface';
import { ApiUrl } from '../api.url';
import { RefreshTokenRequest, RefreshTokenResponse } from './token.interface';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor(private http: HttpClient) {}

  /**
   * 刷新Token
   * @param req userId and refreshToen
   * @returns new Token
   */
  refreshToken(
    req: RefreshTokenRequest
  ): Observable<ResultModel<RefreshTokenResponse>> {
    return this.http.post<ResultModel<RefreshTokenResponse>>(
      ApiUrl.refreshToken,
      req
    );
  }
}
