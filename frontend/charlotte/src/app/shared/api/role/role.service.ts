import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ChceckRoleAuthResult, CreateRoleRequest, GetRoleAuthResult, GetRoleResult, ModifyRoleAuthRequest, ModifyRoleRequest } from './role.interface';
import { ApiUrl } from '../api.url';
import { ResultMessage, ResultModel } from '../api.interface';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http: HttpClient) { }

  getRoles(): Observable<ResultModel<GetRoleResult[]>>{
    return this.http.get<ResultModel<GetRoleResult[]>>(ApiUrl.role)
  }

  createRole(req: CreateRoleRequest): Observable<ResultMessage>{
    return this.http.post<ResultMessage>(ApiUrl.role, req)
  }

  modifyRole(roleId: number, req: ModifyRoleRequest){
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

  getRoleAuth(roleId: number): Observable<ResultModel<GetRoleAuthResult[]>>{
    return this.http.get<ResultModel<GetRoleAuthResult[]>>(`${ApiUrl.roleAuth}\\${roleId}`)
  }

  modifyRoleAuth(roleId: number, req: ModifyRoleAuthRequest[]): Observable<ResultMessage>{
    return this.http.put<ResultMessage>(`${ApiUrl.roleAuth}\\${roleId}`, req)
  }

  checkRoleAuth(userId: number, routerPath: string): Observable<ResultModel<ChceckRoleAuthResult>>{
    return this.http.get<ResultModel<ChceckRoleAuthResult>>(ApiUrl.roleAuth, {
      params: {
        userId,
        routerPath
      }
    })
  }
}
