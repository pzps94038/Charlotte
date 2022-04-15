import { Token } from "../../service/token/toke.interface"

export interface LoginRequest{
  account: string, //帳號
  password: string // 密碼
}
export interface LoginResult{
  token: Token,
  managerUserId: number
}
export interface ModifyUserRequest{
  userName: string,
  email: string,
  address: string | null,
  birthday: Date
}
export interface CreateUserRequest{
  userName: string
  account: string
  password: string
  email: string
  address?: string
  birthday: Date
  roleId: number
  flag: boolean
}
export interface GetUserResult{
  userName: string,
  email: string,
  address: string | null,
  birthday: Date
}
export interface GetUsersResult{
  managerUserId: number
  userName: string
  account: string
  password: string
  email: string
  address?: string
  birthday: Date
  flag: string
  roleId: number
  roleName: string
}
