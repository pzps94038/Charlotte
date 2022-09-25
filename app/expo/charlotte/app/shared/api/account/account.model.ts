export interface LoginRequest {
  account: string;
  password: string;
}
export interface RegisterRequest {
  account: string;
  userName: string;
  password: string;
  email: string;
  address?: string;
  birthday: string;
}
