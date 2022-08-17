import { useNavigation } from "@react-navigation/native";
import { Button } from "@rneui/themed";
import React from "react";
import { Text } from "react-native";
import { Nav } from "../../shared/router/nav";
import { SafeAreaView } from "react-native-safe-area-context";
const Register = () => {
  const navigation = useNavigation<Nav>();
  return (
    <SafeAreaView>
      <Text>Register</Text>
      <Button
        onPress={() => {
          navigation.navigate('Account', {
            screen: 'Login'
          });
        }}>
        goToLogin
      </Button>
    </SafeAreaView>
  );
};
export default Register;
