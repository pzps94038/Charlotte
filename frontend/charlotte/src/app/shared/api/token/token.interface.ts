import { Token } from "../../service/token/toke.interface"

export interface RefreshTokenRequest{
  refreshToken: string,
  userId: number
}
export interface RefreshTokenResponse{
  accessToken: string
  refreshToken: string
}
