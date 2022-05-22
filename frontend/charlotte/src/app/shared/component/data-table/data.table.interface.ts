import { Sort } from "@angular/material/sort";
import { BehaviorSubject } from "rxjs";

export interface InitDataTable<T>{
  tableDataList: T[] // table資料來源
  tableTotalCount: number
  loading$ : BehaviorSubject<boolean> // loading spiiner us
  createColumns(): {key: string, value: string | number}[] // 資料表頭
}
export interface InitDataTableFunction<T> {
  refresh(): void
  create(): void
  modify(row: T): void
  delete(row: T): void
  multipleDelete(rows: T[]): void
  filterTable(info: DataTableInfo): void
}
export interface DataTableInfo{
  page?: {
    limit: number
    offset: number
  }
  sort: Sort
  filterStr?: string
}
