import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import Login from './login/login';
import Register from './register/register';
import UserManagerRouter from './user-manager/user-manager-router';
const AccountRouter = () => {
  const AccountStack = createNativeStackNavigator();
  return (
    <AccountStack.Navigator initialRouteName="UserManager">
      <AccountStack.Screen
        name="UserManager"
        component={UserManagerRouter}
        options={{
          headerShown: false,
          headerTitle: "帳戶資訊"
        }}
      />
      <AccountStack.Screen
        name="Login"
        component={Login}
        options={{ headerShown: false }}
      />
      <AccountStack.Screen
        name="Register"
        component={Register}
        options={{ headerShown: false }}
      />
    </AccountStack.Navigator>
  );
};
export default AccountRouter;
