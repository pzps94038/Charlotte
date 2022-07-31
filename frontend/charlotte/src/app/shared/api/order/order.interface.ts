import { GetOrderDetail as Detail } from '../orderDetail/orderDetail.interface';

export interface GetOrdersResult {
  orderId: number;
  userId: number;
  userName: string;
  orderAmount: number;
  createDate: string;
  modifyDate: string;
}

export interface CreateOrderRequest {
  userId: number;
  orderDetail: Detail[];
}
