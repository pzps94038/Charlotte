import {
  BottomTabBarButtonProps,
  BottomTabNavigationOptions,
} from '@react-navigation/bottom-tabs';
import * as React from 'react';
import Account from '../../../account/account';
import Home from '../../../home/home';
import Shop from '../../../shop/shop';
import ShopCart from '../../../shopCart/shopCart';
import TabButton from './tsbButton';

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
    component: Shop,
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
    component: ShopCart,
    options: {
      tabBarShowLabel: false,
      headerShown: false,
      tabBarButton: props => {
        const obj: BottomTabButton = { ...props, name: 'shopping-cart' };
        return <TabButton {...obj} />;
      },
    },
  },
  //購物車
  {
    name: 'Account',
    component: Account,
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
