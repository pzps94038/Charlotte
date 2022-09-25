import { useNavigation } from "@react-navigation/native";
import { Button, Icon, Input } from "@rneui/themed";
import React, { useState } from "react";
import { ImageBackground, Text, TouchableOpacity, View, StyleSheet, Platform, Pressable } from "react-native";
import { Nav } from "../../shared/router/nav";
import { SafeAreaView } from "react-native-safe-area-context";
import DateTimePickerModal from "react-native-modal-datetime-picker";
import * as Yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { Controller, useForm } from "react-hook-form";
import { RegisterRequest } from "../../shared/api/account/account.model";
import { register } from "../../shared/api/account/account.service";
import { ifSuccess } from "../../shared/services/shared.service";
import Toast from "react-native-toast-message";
const Register = () => {
  const navigation = useNavigation<Nav>();
  const [isDatePickerVisible, setDatePickerVisibility] = useState(false);
  const [hidePassword, setHidePassword] = useState(true);
  const [hideConfirmPassword, setHideConfirmPassword] = useState(true);
  const handleConfirmDate = (date: Date) => {
    setValue(
      'birthday',
      `${date.getFullYear()}-${((date.getMonth() + 1).toString()).padStart(2, '0')}-${(date.getDate()).toString().padStart(2, '0')}`,
      {
        shouldValidate: true,
        shouldDirty: true,
        shouldTouch: true,
      }
    )
    setDatePickerVisibility(false);
  };

  const validationSchema = Yup.object().shape({
    userName: Yup.string().required('此欄位必填'),
    account: Yup.string().required('此欄位必填'),
    password: Yup.string().required('此欄位必填'),
    confirmPassword: Yup.string().required('此欄位必填').test('confirmPassword', '兩個密碼不相同', (val, controller) => {
      const password = controller.parent.password;
      return val === password;
    }),
    email: Yup.string().required('此欄位必填').email('Email格式錯誤'),
    birthday: Yup.string().required('此欄位必填'),
    address: Yup.string()
  })
  const { control, handleSubmit, setValue, formState: { errors } } = useForm({
    defaultValues: {
      userName: '',
      account: '',
      password: '',
      confirmPassword: '',
      email: '',
      birthday: '',
      address: '',
    },
    resolver: yupResolver(validationSchema),
  });
  const submit = async (data: RegisterRequest) => {
    const res = await register(data);
    if (ifSuccess(res)) {
      Toast.show({
        type: "success",
        text1: '註冊成功趕快去登入！～',
      });
      navigation.navigate('Account', {
        screen: 'Login'
      })
    }
  };

  return (
    <SafeAreaView className="flex-1">
      <ImageBackground source={require('../../../assets/image/login.png')} style={{
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
      }}>
        {/* 姓名 */}
        <Controller
          name="userName"
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
                      type="material"
                      name='badge' />
                  }
                  inputContainerStyle={{ borderBottomWidth: 0 }}
                  placeholder="請輸入姓名" />
              </View>
              {
                error && (
                  <Text className="w-9/12 text-rose-600">{errors?.userName?.message}</Text>
                )
              }
            </View>
          )}
        />
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
                  onChangeText={field.onChange}
                  autoCapitalize='none'
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
        {/* 確認密碼 */}
        <Controller
          name="confirmPassword"
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
                      onPressIn={() => { setHideConfirmPassword(false) }}
                      onPressOut={() => { setHideConfirmPassword(true) }}
                      name={hideConfirmPassword ? "eye" : "eye-off"} />
                  }
                  secureTextEntry={hideConfirmPassword}
                  inputContainerStyle={{ borderBottomWidth: 0 }}
                  placeholder="請輸入確認密碼" />
              </View>
              {
                error && (
                  <Text className="w-9/12 text-rose-600">{errors?.confirmPassword?.message}</Text>
                )
              }
            </View>
          )}
        />
        {/* Email */}
        <Controller
          name="email"
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
                  onChangeText={field.onChange}
                  leftIcon={
                    <Icon
                      type="material-community"
                      name='email' />
                  }
                  autoCapitalize='none'
                  inputContainerStyle={{ borderBottomWidth: 0 }}
                  placeholder="請輸入電子郵件" />
              </View>
              {
                error && (
                  <Text className="w-9/12 text-rose-600">{errors?.email?.message}</Text>
                )
              }
            </View>
          )}
        />
        {/* 生日dateTimePicker */}
        <DateTimePickerModal
          isVisible={isDatePickerVisible}
          mode="date"
          maximumDate={new Date()}
          onConfirm={handleConfirmDate}
          onCancel={() => setDatePickerVisibility(false)}
          cancelTextIOS="取消"
          confirmTextIOS="確認"
          locale="zh-tw"
        />
        {/* 生日 */}
        <Controller
          name="birthday"
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
                  onChangeText={field.onChange}
                  editable={false}
                  onPressIn={() => setDatePickerVisibility(true)}
                  leftIcon={
                    <Icon
                      onPressIn={() => setDatePickerVisibility(true)}
                      type="ionicon"
                      name='calendar-outline' />
                  }
                  inputContainerStyle={{ borderBottomWidth: 0 }}
                  placeholder="請輸入生日" />
              </View>
              {
                error && (
                  <Text className="w-9/12 text-rose-600">{errors?.birthday?.message}</Text>
                )
              }
            </View>
          )}
        />
        {/* 地址 */}
        <Controller
          name="address"
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
                      type="ionicon"
                      name='home' />
                  }
                  inputContainerStyle={{ borderBottomWidth: 0 }}
                  placeholder="請輸入地址(非必填)" />
              </View>
              {
                error && (
                  <Text className="w-9/12 text-rose-600">{errors?.address?.message}</Text>
                )
              }
            </View>
          )}
        />
        <Pressable onPress={handleSubmit(submit)} className="my-2 w-36 rounded h-10 flex-row  justify-center items-center bg-indigo-600 active:bg-indigo-900">
          <Text className="font-bold text-center text-white text-lg">註冊</Text>
        </Pressable>
        <TouchableOpacity onPress={() => {
          navigation.navigate('Account', {
            screen: 'Login'
          })
        }}>
          <Text className="text-white font-bold">已有帳號了嗎?</Text>
          <View className="flex-row justify-center">
            <Text className="text-white font-bold">返回</Text>
            <Text className="text-rose-600 font-bold">登入!</Text>
          </View>
        </TouchableOpacity>
      </ImageBackground>
    </SafeAreaView>
  );
};

export default Register;
