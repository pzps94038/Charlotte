import React, { useState } from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import Login from './login/login';
import Register from './register/register';
import { useFocusEffect } from '@react-navigation/native';
import { tokenService } from '../shared/services/userInfo-service';
const Account = () => {
  const [auth, setAuth] = useState<boolean>(false);
  const AccountStack = createNativeStackNavigator();
  useFocusEffect(() => {
    tokenService.getToken().then(token => token ? setAuth(true) : setAuth(false));
  })
  return (
    <AccountStack.Navigator initialRouteName="Login">
      {/* {
        auth &&
        [
          
        ]
      } */}
      <AccountStack.Screen
        name="Register"
        component={Register}
        options={{ headerShown: false }}
      />
      <AccountStack.Screen
        name="Login"
        component={Login}
        options={{ headerShown: false }}
      />
    </AccountStack.Navigator>
  );
};
export default Account;
