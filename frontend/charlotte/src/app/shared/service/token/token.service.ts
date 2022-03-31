import { Injectable } from '@angular/core';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { Token } from './toke.interface';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  /**
   * @returns 驗證跟刷新Token
   */
  getToken(): Token{
    let Token = localStorage.getItem('Token')
    if(Token) return JSON.parse(Token)
    else return { accessToken: '', refreshToken: ''};
  }
  /**
   * @param token 驗證跟刷新token
   */
  saveToken(token: Token): void{
    localStorage.setItem('Token', JSON.stringify(token))
  }

  checkToken(): boolean{
    const { accessToken , refreshToken } = this.getToken()
    if(accessToken === "" && refreshToken === "")return false
    else return true
  }
}
