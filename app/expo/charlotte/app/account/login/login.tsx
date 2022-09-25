import { CommonActions, useFocusEffect, useNavigation } from "@react-navigation/native";
import { Icon, Input } from "@rneui/themed";
import React, { useCallback, useState } from "react";
import { Text, Image, StyleSheet, View, ImageBackground, TouchableOpacity, Button, Pressable } from "react-native";
import { Nav } from "../../shared/router/nav";
import { SafeAreaView } from "react-native-safe-area-context";
import * as Yup from 'yup';
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import { ifSuccess } from "../../shared/services/shared.service";
import { login } from "../../shared/api/account/account.service";
import { LoginRequest } from "../../shared/api/account/account.model";
import { tokenService } from "../../shared/services/userInfo.service";
import Toast from "react-native-toast-message";

const Login = () => {
  const navigation = useNavigation<Nav>();
  const [hidePassword, setHidePassword] = useState(true);
  const validationSchema = Yup.object().shape({
    account: Yup.string().required('此欄位必填'),
    password: Yup.string().required('此欄位必填'),
  })
  const { control, handleSubmit, formState: { errors } } = useForm({
    defaultValues: {
      account: '',
      password: ''
    },
    resolver: yupResolver(validationSchema),
    reValidateMode: 'onBlur'
  });
  const submit = async (data: LoginRequest) => {
    const res = await login(data);
    if (ifSuccess(res)) {
      tokenService.setToken(res.data);
      Toast.show({
        type: "success",
        text1: res.message,
      });
      navigation.dispatch(
        CommonActions.reset({
          routes: [
            { name: 'UserManager' },
          ],
        }))
      navigation.navigate('Home');
    }
  };

  return (
    <SafeAreaView className="flex-1">
      <ImageBackground source={require('../../../assets/image/login.png')} style={{
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
      }}>
        <Image source={require('../../../assets/image/logo.png')} className="h-40 w-40" />
        {/* 帳號 */}
        <Controller
          name="account"
          control={control}
          render={({ field, fieldState: { error } }) => (
            <View className="w-full items-center">
              <View
                className="m-1 bg-orange-50 w-9/12 h-12 rounded-lg text-lg border-2"
                style={{
                  borderColor: error && 'red',
                }}>
                <Input
                  {...field}
                  autoCapitalize='none'
                  onChangeText={field.onChange}
                  leftIcon={
                    <Icon
                      type="material-community"
                      name='account' />
                  }
                  inputContainerStyle={{ borderBottomWidth: 0 }}
                  placeholder="請輸入帳號" />
              </View>
              {
                error && (
                  <Text className="w-9/12 text-rose-600">{errors?.account?.message}</Text>
                )
              }
            </View>
          )}
        />
        {/* 密碼 */}
        <Controller
          name="password"
          control={control}
          render={({ field, fieldState: { error } }) => (
            <View className="w-full items-center">
              <View
                className="m-1 bg-orange-50 w-9/12 h-12 rounded-lg text-lg border-2"
                style={{
                  borderColor: error && 'red',
                }}>
                <Input
                  {...field}
                  autoCapitalize='none'
                  onChangeText={field.onChange}
                  leftIcon={
                    <Icon
                      type="material-community"
                      name='lock' />
                  }
                  rightIcon={
                    <Icon
                      type="ionicon"
                      onPressIn={() => { setHidePassword(false) }}
                      onPressOut={() => { setHidePassword(true) }}
                      name={hidePassword ? "eye" : "eye-off"} />
                  }
                  secureTextEntry={hidePassword}
                  inputContainerStyle={{ borderBottomWidth: 0 }}
                  placeholder="請輸入密碼" />
              </View>
              {
                error && (
                  <Text className="w-9/12 text-rose-600">{errors?.password?.message}</Text>
                )
              }
            </View>
          )}
        />
        <Pressable onPress={handleSubmit(submit)} className="w-36 rounded h-10 flex-row  justify-center items-center bg-indigo-600 active:bg-indigo-900">
          <Text className="font-bold text-center text-white text-lg">登入</Text>
        </Pressable>
        <TouchableOpacity onPress={() => {
          navigation.navigate('Account', {
            screen: 'Register'
          })
        }}>
          <Text className="text-white font-bold mt-2">還沒有帳號嗎?</Text>
          <View className="flex-row justify-center">
            <Text className="text-white font-bold">來去</Text>
            <Text className="text-rose-600 font-bold">註冊!</Text>
          </View>
        </TouchableOpacity>
      </ImageBackground>
    </SafeAreaView>
  );
};

export default Login;
