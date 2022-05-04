export interface GetFactorysResult{
  factoryId: number
  factoryName: string
  createDate: string
  modifyDate: string
}

export interface CreateFactoryRequest{
  factoryName: string
}

export interface ModifyFactoryRequest{
  factoryName: string
}
