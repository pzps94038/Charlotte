import { useFocusEffect, useNavigation } from "@react-navigation/native";
import { Button } from "@rneui/themed";
import React from "react";
import { ScrollView, Text, TextInput, Image, StyleSheet, View, ImageBackground, TouchableOpacity } from "react-native";
import { Nav } from "../../shared/router/nav";
import { SafeAreaView } from "react-native-safe-area-context";
import * as Yup from 'yup';
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import FadeIn from "../../shared/animated/fade";
const Login = () => {
  const navigation = useNavigation<Nav>();
  useFocusEffect(() => {
    // setTimeout(() => {
    //   // 如果登入了轉址到會員中心
    //   navigation.navigate('Account', {
    //     screen: 'Register'
    //   });
    // }, 1000)
  })

  return (
    <SafeAreaView style={styles.container}>
      <ImageBackground source={require('../../../assets/image/login.png')} style={styles.image}>
        <Image source={require('../../../assets/image/logo.png')} style={
          styles.logo
        } />
        <TextInput style={styles.textInput} placeholder="請輸入帳號"></TextInput>
        <TextInput style={styles.textInput} placeholder="請輸入密碼"></TextInput>
        <Button
          title="登入"
          titleStyle={{ fontWeight: '700' }}
          buttonStyle={styles.button}
          containerStyle={styles.buttonContainer}
        />
        <TouchableOpacity style={{
          alignItems: 'center',
        }} onPress={() => {
          navigation.navigate('Account', {
            screen: 'Register'
          })
        }}>
          <Text style={{ color: 'white', fontWeight: '800' }}>還沒有帳號嗎?</Text>
          <View style={{
            flexDirection: 'row'
          }}>
            <Text style={{ color: 'white', fontWeight: '800' }}>來去</Text>
            <Text style={{ color: 'red', fontWeight: '800' }}>註冊!</Text>
          </View>
        </TouchableOpacity>
      </ImageBackground>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: 'row',
  },
  image: {
    flex: 1,
    resizeMode: 'cover',
    justifyContent: 'center',
    alignItems: 'center',
    // flexDirection: 'row',
  },
  logo: {
    width: 150,
    height: 150,
  },
  textInput: {
    margin: 10,
    backgroundColor: '#FFFFdd',
    width: '80%',
    height: 50,
    borderRadius: 15,
    textAlign: 'center',
    fontSize: 15
  },
  button: {
    backgroundColor: 'rgba(92, 99,216, 1)',
    borderColor: 'transparent',
    borderWidth: 0,
    borderRadius: 5,
    paddingVertical: 10,
  },
  buttonContainer: {
    width: 200,
    marginHorizontal: 50,
    marginVertical: 10,
  }
});

export default Login;
