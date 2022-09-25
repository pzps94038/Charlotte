import http from "../Instance";
import {
  ProductResponse,
  ProductTypesResponse,
  SearchResponse,
} from "./product.model";

export const getProductTypes = () =>
  http.get<ProductTypesResponse[]>("api/ProductType");

export const getProducts = (typeId: number) =>
  http.get<ProductResponse[]>(`api/Product`, undefined, {
    params: {
      typeId: typeId,
    },
  });

export const getProduct = (productId: number) => {
  return http.get<ProductResponse>(`api/Product`, productId);
};

export const search = (search: string) => {
  return http.post<SearchResponse[]>(`api/Product`, null, {
    params: {
      search,
    },
  });
};
