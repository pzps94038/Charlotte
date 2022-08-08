import { useNavigation } from "@react-navigation/native";
import { Button } from "@rneui/themed";
import React from "react";
import { SafeAreaView, Text } from "react-native";
import FadeIn from "../../shared/animated/fade";
import { Nav } from "../../shared/router/nav";

const Register = () => {
  const navigation = useNavigation<Nav>();
  return (
    <SafeAreaView>
      <FadeIn>
        <Text>Register</Text>
        <Button
          onPress={() => {
            navigation.navigate('Login');
          }}>
          goToLogin
        </Button>
      </FadeIn>
    </SafeAreaView>
  );
};
export default Register;
