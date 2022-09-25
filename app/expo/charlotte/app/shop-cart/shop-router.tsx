import * as React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import ShopCartDetail from './shop-cart-detail/shop-cart-detail';
import Confirm from './confirm/confirm';
import Grateful from './grateful/grateful';
const ShopCartRouter = () => {
  const ShopCartStack = createNativeStackNavigator();

  return (
    <ShopCartStack.Navigator initialRouteName="ShopCartDetail">
      <ShopCartStack.Screen
        name="ShopCartDetail"
        component={ShopCartDetail}
        options={{
          headerTitle: "購物車"
        }}
      />
      <ShopCartStack.Screen
        name="Confirm"
        component={Confirm}
        options={{
          headerTitle: "確認資訊"
        }}
      />
      <ShopCartStack.Screen
        name="Grateful"
        component={Grateful}
        options={{
          headerShown: false
        }}
      />
    </ShopCartStack.Navigator>
  );
};


export default ShopCartRouter;
