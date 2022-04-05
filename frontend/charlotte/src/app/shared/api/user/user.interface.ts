import { Token } from "../../service/token/toke.interface"

export interface LoginReq{
  account: string, //帳號
  password: string // 密碼
}
export interface LoginRes{
  token: Token,
  managerUserId: number
}
export interface ModifyUserReq{
  userName: string,
  email: string,
  address: string | null,
  birthday: Date
}
export interface CreateUserReq{
  userName: string
  account: string
  password: string
  email: string
  address?: string
  birthday: Date
  roleId: number
}
export interface UserRes{
  userName: string,
  email: string,
  address: string | null,
  birthday: Date
}
