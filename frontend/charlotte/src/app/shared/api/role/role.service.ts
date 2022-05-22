import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ChceckRoleAuthResult, CreateRoleRequest, GetRoleAuthResult, GetRoleResult, ModifyRoleAuthRequest, ModifyRoleRequest } from './role.interface';
import { ApiUrl } from '../api.url';
import { DataTable, ResultMessage, ResultModel } from '../api.interface';
import { DataTableInfo } from '../../component/data-table/data.table.interface';
import { SharedService } from '../../service/shared.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(
    private http: HttpClient,
    private sharedService: SharedService
  ) { }

  getRoles(info? : DataTableInfo): Observable<ResultModel<DataTable<GetRoleResult>>>{
    let params = this.sharedService.createDataTableParams(info)
    return this.http.get<ResultModel<DataTable<GetRoleResult>>>(ApiUrl.role,{
      params
    })
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
