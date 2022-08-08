import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { NavigationContainer } from '@react-navigation/native';
import { StatusBar } from 'expo-status-bar';
import * as React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import Toast from 'react-native-toast-message';
import { Provider } from 'react-redux';
import Tabs from './app/shared/router/tabs/tabs';
import { store } from './app/shared/store/store';

export default function App() {
  const Tab = createBottomTabNavigator();
  return (
    <Provider store={store}>
      <NavigationContainer>
        {/* 安全邊界 */}
        <Tab.Navigator
          screenOptions={{
            tabBarActiveTintColor: '#4B0091',
            headerShown: false,
            tabBarStyle: {
              paddingBottom: 0,
              height: 60,
              position: 'absolute',
              bottom: 30,
              left: 16,
              right: 16,
              zIndex: 100,
              borderBottomLeftRadius: 25,
              borderTopLeftRadius: 25,
              borderBottomRightRadius: 25,
              borderTopRightRadius: 25,
              backgroundColor: '#171717',
            },
          }}>
          {Tabs.map(a => {
            return (
              <Tab.Screen
                key={a.name}
                name={a.name}
                component={a.component}
                options={a.options}
              />
            );
          })}
        </Tab.Navigator>
      </NavigationContainer>
      <Toast />
    </Provider>
  );
}
