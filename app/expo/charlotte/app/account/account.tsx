import React from 'react';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import Login from './login/login';
import Register from './register/register';
const Account = () => {
  const AccountStack = createNativeStackNavigator();
  return (
    <AccountStack.Navigator>
      <AccountStack.Screen
        name="Login"
        component={Login}
        options={{headerShown: false}}
      />
      <AccountStack.Screen
        name="Register"
        component={Register}
        options={{headerShown: false}}
      />
    </AccountStack.Navigator>
  );
};
export default Account;
