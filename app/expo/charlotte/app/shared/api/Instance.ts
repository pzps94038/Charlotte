import axios from "axios";
import { tokenService } from "../services/userInfo-service";
import { ResultModel } from "./api.interface";
export const baseURL = "http://172.20.10.12:8080/";
const instance = axios.create({
  baseURL,
  headers: { "Content-Type": "application/json" },
  timeout: 20000,
});

// 請求攔截
instance.interceptors.request.use(
  async (config) => {
    const token = await tokenService.getToken();
    if (token) {
      config.headers = {
        Authorization: token.accessToken,
      };
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// 回應攔截
instance.interceptors.response.use(
  (response) => response.data,
  // error handle
  (error) => {
    if (error.response) {
      switch (error.response.status) {
        case 401:
          console.log("權限不足");
          // go to 404 page
          break;
        default:
          console.log(error.message);
      }
    }
    return Promise.reject(error);
  }
);

// T附載 R回應
const http = {
  get: <R = any>(url: string, id?: string | number) =>
    instance.get<R, ResultModel<R>>(id ? `${url}\\${id}` : url),
  post: <T, R = any>(url: string, data: T) =>
    instance.post<ResultModel<R>>(url, data),
  put: <T, R = any>(url: string, id: string | number, data: T) =>
    instance.put<ResultModel<R>>(`${url}\\${id}`, data),
  patch: <T, R = any>(url: string, id: string | number, data: T) =>
    instance.patch<ResultModel<R>>(`${url}\\${id}`, data),
  delete: <R = any, T = null>(url: string, id: string | number, data?: T) =>
    instance.delete<ResultModel<R>>(`${url}\\${id}`, { params: data }),
};

export default http;
