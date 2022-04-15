export interface GetRouterResult{
  routerId: number
  routerName: string
  link: string
  icon: string
  groupId: number
  flag: string
}
export interface CreateRouterRequest{
  link: string
  routerName: string
  icon: string
  groupId: string
  flag: string
}
export interface ModifyRouterRequest{
  routerId: number
  routerName: string
  link: string
  icon: string
  groupId: string
  flag: string
}

