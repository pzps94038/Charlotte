export interface GetRoleResult{
  roleId: number
  roleName: string
  createDate: string
  modifyDate: string
}

export interface CreateRoleRequest{
  roleName: string
}

export interface ModifyRoleRequest{
  roleName: string
}

export interface GetRoleAuthResult{
  roleId: number
  routerId: number
  routerName: string
  viewAuth: boolean
  createAuth: boolean
  modifyAuth: boolean
  deleteAuth: boolean
  exportAuth: boolean
}

export interface ModifyRoleAuthRequest{
  routerId: number
  viewAuth: boolean
  createAuth: boolean
  modifyAuth: boolean
  deleteAuth: boolean
  exportAuth: boolean
}

export interface ChceckRoleAuthResult{
  viewAuth: boolean
  createAuth: boolean
  deleteAuth: boolean
  exportAuth: boolean
  modifyAuth: boolean
}
