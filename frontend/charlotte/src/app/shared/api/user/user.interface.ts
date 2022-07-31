export interface GetUsersResult {
  userId: number;
  userName: string;
  account: string;
  password: string;
  email: string;
  address?: string;
  birthday: Date;
  flag: string;
}

export interface ModifyUserRequest {
  userName: string;
  account: string;
  password: string;
  email: string;
  address: string | null;
  birthday: Date;
  flag: boolean;
}
export interface CreateUserRequest {
  userName: string;
  account: string;
  password: string;
  email: string;
  address?: string;
  birthday: Date;
  flag: boolean;
}
