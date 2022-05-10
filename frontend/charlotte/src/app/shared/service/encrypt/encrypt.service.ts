import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';
@Injectable({
  providedIn: 'root'
})
export class EncryptService {
  private readonly key: string = '12341688915345678942745678812355'
  private readonly iv: string = '1234467811534587'

  constructor() { }

  AESEncrypt(plainText: string): string{
    console.log(plainText)
    const option = {
      iv: CryptoJS.enc.Utf8.parse(this.iv),
      mode: CryptoJS.mode.CBC
    }
    return CryptoJS.AES.encrypt(plainText, CryptoJS.enc.Utf8.parse(this.key), option).toString();
  }

  AESDecrypt<T = any>(cipherText: string): T{
    const option = {
      iv: CryptoJS.enc.Utf8.parse(this.iv),
      mode: CryptoJS.mode.CBC
    }
    const bytes = CryptoJS.AES.decrypt(cipherText, this.key, option)
    return JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
  }
}
