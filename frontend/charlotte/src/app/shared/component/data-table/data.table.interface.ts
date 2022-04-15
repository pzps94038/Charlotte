export interface InitDataTable{
  createColumns(): {key: string, value: string | number}[]
}
export interface InitDataTableFunction<T> {
  refresh(): void
  create(): void
  modify(row: T): void
  delete(row: T): void
  multipleDelete(rows: T[]): void
}
