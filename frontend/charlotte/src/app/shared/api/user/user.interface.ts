import { Token } from "../../service/token/toke.interface"

export interface LoginReq{
  account: string, //帳號
  password: string // 密碼
}
export interface LoginRes{
  token: Token,
  managerUserId: number
}
export interface UserReq{
  userName: string,
  email: string,
  address: string | null,
  birthday: Date
}
export interface UserRes{
  userName: string,
  email: string,
  address: string | null,
  birthday: Date
}
