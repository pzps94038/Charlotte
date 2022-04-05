export interface GetRoleRes{
  roleId: number
  roleName: string
  createDate: Date
  modifyDate: Date
}
export interface CreateRoleReq{
  roleName: string
}
export interface ModifyRoleReq{
  roleName: string
}
