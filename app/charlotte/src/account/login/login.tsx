import {useNavigation} from '@react-navigation/native';
import {Button} from '@rneui/themed';
import React from 'react';
import {SafeAreaView, Text} from 'react-native';
import {Nav} from '../../shared/router/nav';

const Login = () => {
  const navigation = useNavigation<Nav>();
  return (
    <SafeAreaView>
      <Text>Login</Text>
      <Button
        onPress={() => {
          navigation.navigate('Register');
        }}>
        goToRegister
      </Button>
    </SafeAreaView>
  );
};
export default Login;
