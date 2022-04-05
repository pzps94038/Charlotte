import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateRoleReq, GetRoleRes, ModifyRoleReq } from './role.interface';
import { ApiUrl } from '../api.url';
import { ResultMessage, ResultModel } from '../api.interface';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http: HttpClient) { }

  getRoles(): Observable<ResultModel<GetRoleRes[]>>{
    return this.http.get<ResultModel<GetRoleRes[]>>(ApiUrl.role)
  }
  createRole(req: CreateRoleReq): Observable<ResultMessage>{
    return this.http.post<ResultMessage>(ApiUrl.role, req)
  }
  modifyRole(roleId: number, req: ModifyRoleReq){
    return this.http.patch<ResultMessage>(`${ApiUrl.role}\\${roleId}`, req)
  }
  deleteRole(roleId: number): Observable<ResultMessage>{
    return this.http.delete<ResultMessage>(`${ApiUrl.role}\\${roleId}`)
  }
  batchDeleteRole(req: number[]){
    return this.http.delete<ResultMessage>(ApiUrl.role, {
      body: req
    });
  }
}
