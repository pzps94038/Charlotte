import { SweetAlertIcon } from "sweetalert2";
export interface SwalModel {
  title?: string, // 顯示標題
  text?: string, // 顯示內容文字
  icon?: SweetAlertIcon, // icon種類 'success' | 'error' | 'warning' | 'info' | 'question'
  showCancelButton?: boolean, // 顯示取消按鈕
  confirmButtonText?: string, //確認按鈕文字
  cancelButtonText?: string // 取消按鈕文字
}

