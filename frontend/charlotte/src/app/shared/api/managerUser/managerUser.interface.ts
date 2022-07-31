import { Token } from '../../service/token/toke.interface';

export interface LoginRequest {
  account: string; //帳號
  password: string; // 密碼
}
export interface LoginResult {
  token: Token;
  managerUserId: number;
}
export interface ModifyManagerUserRequest {
  userName: string;
  account: string;
  password: string;
  email: string;
  address: string | null;
  birthday: string;
  roleId: number;
  flag: string;
}
export interface CreateManagerUserRequest {
  userName: string;
  account: string;
  password: string;
  email: string;
  address?: string;
  birthday: string;
  roleId: number;
  flag: boolean;
}
export interface GetManagerUserResult {
  userName: string;
  email: string;
  address: string | null;
  birthday: string;
}
export interface GetManagerUsersResult {
  managerUserId: number;
  userName: string;
  account: string;
  password: string;
  email: string;
  address?: string;
  birthday: string;
  flag: string;
  roleId: number;
  roleName: string;
}

export interface ModfiyManagerUserPasswordRequest {
  password: string;
  newPassword: string;
}
