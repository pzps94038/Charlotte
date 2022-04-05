export interface InitDataTable{
  createColumns(): {key: string, value: string | number}[]
}
export interface InitDataTableFunction {
  refresh(): void
  create(): void
  modify(row: any): void
  delete(row: any): void
  multipleDelete(row: any[]): void
}
