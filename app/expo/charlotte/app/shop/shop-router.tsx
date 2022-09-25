import * as React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import Product from './product/product';
import ProductType from './product-type/product-type';
import Search from './search/search';
import ProductDetail from './product-detail/product-detail';
const ShopRouter = ({ navigation }: any) => {
  const ShopStack = createNativeStackNavigator();
  return (
    <ShopStack.Navigator initialRouteName="ProductType">
      <ShopStack.Screen
        name="ProductType"
        component={ProductType}
        options={{
          headerTitle: "商城"
        }}
      />
      <ShopStack.Screen
        name="Product"
        component={Product}
        options={{
          headerTitle: "商品", headerBackTitle: "返回"
        }}
      />
      <ShopStack.Screen
        name="ProductDetail"
        component={ProductDetail}
        options={{
          headerTitle: "商品資訊", headerBackTitle: "返回",
        }}
      />
      <ShopStack.Screen
        name="Search"
        component={Search}
        options={{
          headerShown: false,
        }}
      />
    </ShopStack.Navigator>
  );
};
export default ShopRouter;
