export interface GetProductTypesResult {
  productTypeId: number;
  type: string;
  iconType: string;
  iconName: string;
  createDate: string;
  modifyDate: string;
}
export interface ModifyProductTypeRequest {
  type: string;
  conType: string;
  iconName: string;
}
export interface CreateProductTypeRequest {
  type: string;
  iconType: string;
  iconName: string;
}
