export interface GetProductResult{
  productId:number
  productName: string
  type: string
  inventory: number
  costPrice: number
  sellPrice: number
  factoryName: string
  productImgPath: string
}

export interface ModifyProductRequest{
  productName: string
  productTypeId: number
  inventory: number
  costPrice: number
  sellPrice: number
  factoryId: number
  productImgPath: string
}

export interface CreateProductRequest{
  productName: string
  productTypeId: number
  inventory: number
  costPrice: number
  sellPrice: number
  factoryId: number
  productImgPath: string
}
