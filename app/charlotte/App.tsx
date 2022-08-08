/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * Generated with the TypeScript template
 * https://github.com/react-native-community/react-native-template-typescript
 *
 * @format
 */

import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import Tabs from './src/shared/router/tabs/tabs';
import { Provider } from 'react-redux';
import { store } from './src/shared/store/store';
import Toast from 'react-native-toast-message';
const App = () => {
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
              height: 60,
              position: 'absolute',
              bottom: 16,
              left: 16,
              right: 16,
              zIndex: 100,
              borderBottomLeftRadius: 25,
              borderTopLeftRadius: 25,
              borderBottomRightRadius: 25,
              borderTopRightRadius: 25,
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
};

export default App;
