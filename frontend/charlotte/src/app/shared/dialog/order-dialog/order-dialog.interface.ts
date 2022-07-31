import { GetOrderDetail as OrderDetail } from '../../api/orderDetail/orderDetail.interface';
import { GetProductResult as Product } from '../../api/product/product.interface';
import { GetUsersResult as User } from '../../api/user/user.interface';

export interface OrderDialog {
  title: string;
  products: Product[];
  users: User[];
  orderDetail: undefined | OrderDetail[];
  userId: undefined | number;
}
