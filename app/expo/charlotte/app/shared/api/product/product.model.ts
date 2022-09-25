export interface ProductTypesResponse {
  productTypeId: number;
  iconType: string;
  iconName: string;
  type: string;
}
export interface ProductResponse {
  productId: number;
  productName: string;
  type: string;
  productImgPath: string;
  productDescription: string;
  inventory: number;
  sellPrice: number;
}

export interface SearchResponse {
  productId: number;
  productName: string;
  productImgPath: string;
}
