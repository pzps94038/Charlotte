import { ApiUrl } from './../api.url';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResultModel } from '../api.interface';
import { GetMenuResult } from './menu.interface';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private http: HttpClient) { }

  getMenu(userId: number): Observable<ResultModel<GetMenuResult[]>>{
    return this.http.get<ResultModel<GetMenuResult[]>>(`${ApiUrl.menu}\\${userId}`)
  }
}
