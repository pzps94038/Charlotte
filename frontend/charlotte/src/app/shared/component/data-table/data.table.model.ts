import { Sort } from "@angular/material/sort";
import { BehaviorSubject } from "rxjs";

export abstract class BaseDataTable<T>{
  tableDataList: T[] = [] // 表格資料
  tableTotalCount: number = 0 // 資料總數
  loading$ : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false) // table spinnerloading
  columns:{ key: string; value: string | number }[] = [] // table表頭
  abstract createColumns(): {key: string, value: string | number}[] // 創建資料表頭
}

export interface InitDataTableFunction<T> {
  refresh(): void //刷新
  create(): void //創建
  modify(row: T): void //修改
  delete(row: T): void //刪除
  multipleDelete(rows: T[]): void //多選刪除
  filterTable(info: DataTableInfo): void //篩選事件
}

export interface DataTableInfo{
  page?: {
    limit: number
    offset: number
  }
  sort?: Sort
  filterStr?: string
}
