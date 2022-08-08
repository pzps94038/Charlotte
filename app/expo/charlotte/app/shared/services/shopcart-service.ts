import AsyncStorage from "@react-native-async-storage/async-storage";
import { ShopCart } from "./shopcart.model";
export const shopCartService = {
  getShopCart: async () => {
    const shopCart = await AsyncStorage.getItem("shopCart");
    return shopCart === null ? [] : (JSON.parse(shopCart) as ShopCart[]);
  },
  addProduct: async (product: ShopCart) => {
    const shopCart = await shopCartService.getShopCart();
    const shopCartProduct = shopCart.find(
      (a) => a.productId === product.productId
    );
    if (shopCartProduct) {
      shopCartProduct.amount = shopCartProduct.amount + product.amount;
    } else {
      shopCart.push(product);
    }
    await AsyncStorage.setItem("shopCart", JSON.stringify(shopCart));
  },
};
