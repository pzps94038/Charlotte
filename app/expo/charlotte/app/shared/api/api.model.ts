export interface ResultModel<T = null> {
  code: number; // 回傳code
  message: string; // 回傳訊息
  data: T; // 回傳資料
}
