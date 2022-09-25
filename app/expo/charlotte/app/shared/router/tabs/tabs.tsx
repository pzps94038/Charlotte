import {
  BottomTabBarButtonProps,
  BottomTabNavigationOptions,
} from '@react-navigation/bottom-tabs';
import * as React from 'react';
import AccountRouter from '../../../account/account-router';
import Home from '../../../home/home';
import ShopCartRouter from '../../../shop-cart/shop-router';
import ShopRouter from '../../../shop/shop-router';
import TabButton from './tab-button';

export interface Tab {
  name: string;
  component: any;
  options?: BottomTabNavigationOptions;
}
export interface BottomTabButton extends BottomTabBarButtonProps {
  name: string;
}
const Tabs: Tab[] = [
  // 首頁
  {
    name: 'Home',
    component: Home,
    options: {
      tabBarShowLabel: false,
      headerShown: false,
      tabBarButton: props => {
        const obj: BottomTabButton = { ...props, name: 'home' };
        return <TabButton {...obj} />;
      },
    },
  },
  // 商城
  {
    name: 'Shop',
    component: ShopRouter,
    options: {
      tabBarShowLabel: false,
      headerShown: false,
      tabBarButton: props => {
        const obj: BottomTabButton = { ...props, name: 'shopping-bag' };
        return <TabButton {...obj} />;
      },
    },
  },
  //購物車
  {
    name: 'ShopCart',
    component: ShopCartRouter,
    options: {
      tabBarShowLabel: false,
      headerShown: false,
      tabBarButton: props => {
        const obj: BottomTabButton = { ...props, name: 'shopping-cart' };
        return <TabButton {...obj} />;
      },
    },
  },
  //帳戶資訊
  {
    name: 'Account',
    component: AccountRouter,
    options: {
      tabBarShowLabel: false,
      headerShown: false,
      tabBarButton: props => {
        const obj: BottomTabButton = { ...props, name: 'account-circle' };
        return <TabButton {...obj} />;
      },
    },
  },
];

export default Tabs;
