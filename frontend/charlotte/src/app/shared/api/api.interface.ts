/* 回傳只有訊息 */
export interface ResultMessage{
  code: number, // 回傳code
  message: string, // 回傳訊息
}
/* 回傳包含資料 */
export interface ResultModel<T> {
  code: number, // 回傳code
  message: string, // 回傳訊息
  data: T // 回傳資料
}
