export interface GetProductTypesResult{
  productTypeId:number
  type: string
  createDate: string
  modifyDate: string
}
export interface ModifyProductTypeRequest{
  type: string
}
export interface CreateProductTypeRequest{
  type: string
}
