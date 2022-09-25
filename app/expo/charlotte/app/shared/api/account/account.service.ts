import { Token } from "../../services/userInfo.model";
import http from "../Instance";
import { LoginRequest, RegisterRequest } from "./account.model";

export const login = (req: LoginRequest) => http.post<Token>("api/Login", req);
export const register = (req: RegisterRequest) =>
  http.post<null>("api/Register", req);
