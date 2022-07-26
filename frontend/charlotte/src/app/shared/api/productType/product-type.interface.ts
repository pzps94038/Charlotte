export interface GetProductTypesResult {
  productTypeId: number;
  type: string;
  icon?: string;
  createDate: string;
  modifyDate: string;
}
export interface ModifyProductTypeRequest {
  type: string;
  icon?: string;
}
export interface CreateProductTypeRequest {
  type: string;
  icon?: string;
}
