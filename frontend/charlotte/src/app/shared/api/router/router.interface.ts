export interface GetRouterRes{
  routerId: number
  routerName: string
  link: string
  icon: string
  groupId: number
  flag: string
}
export interface CreateRouterReq{
  link: string
  routerName: string
  icon: string
  groupId: string
  flag: string
}
export interface ModifyRouterReq{
  routerId: number
  routerName: string
  link: string
  icon: string
  groupId: string
  flag: string
}

