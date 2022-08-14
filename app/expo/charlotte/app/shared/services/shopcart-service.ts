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
  removeProduct: async (productId: number) => {
    const oldShopCart = await shopCartService.getShopCart();
    const shopCart = oldShopCart.filter((a) => a.productId !== productId);
    await AsyncStorage.setItem("shopCart", JSON.stringify(shopCart));
  },
  fixProduct: async (fixProduct: ShopCart) => {
    const shopCart = await shopCartService.getShopCart();
    let product = shopCart.find((a) => a.productId === fixProduct.productId);
    if (product) {
      product.amount = fixProduct.amount;
      product.inventory = fixProduct.inventory;
      product.price = fixProduct.price;
      product.productName = fixProduct.productName;
      product.productImgPath = fixProduct.productImgPath;
    }
    await AsyncStorage.setItem("shopCart", JSON.stringify(shopCart));
  },
};
