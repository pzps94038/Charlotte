import { useNavigation } from "@react-navigation/native";
import { Button } from "@rneui/themed";
import React from "react";
import { Text, TextInput } from "react-native";
import { Nav } from "../../shared/router/nav";
import { SafeAreaView } from "react-native-safe-area-context";
import * as Yup from 'yup';
import { Controller, useForm } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
const Login = () => {
  const navigation = useNavigation<Nav>();
  const validationSchema =
    Yup.object().shape({
      account: Yup.string().required('此欄位必填'),
      password: Yup.string().required('此欄位必填')
    });
  const { control, handleSubmit, setValue, resetField, getValues, formState: { errors, touchedFields } } = useForm({
    resolver: yupResolver(validationSchema)
  });
  const onSubmit = (data: any) => { console.log({ data }); };
  return (
    <SafeAreaView>
      <Controller
        control={control}
        name="account"
        render={({ field: { value, onChange, onBlur }, fieldState: { error } }) => (
          <TextInput
            onChangeText={onChange}
            onBlur={onBlur}
            value={value}
            style={
              {
                width: '100%',
                height: 50,
                backgroundColor: error ? 'red' : '#FFFF88'
              }
            }
          />
        )}
      />
      <Controller
        control={control}
        name="password"
        render={({ field: { value, onChange, onBlur }, fieldState: { error } }) => (
          <TextInput
            onChange={onChange}
            onBlur={onBlur}
            value={value}
            style={
              {
                width: '100%',
                height: 50,
                backgroundColor: error ? 'red' : '#FFFF88'
              }
            }
          />
        )}
      />
      <Text>Login</Text>
      <Button
        onPress={() => {
          console.log(getValues())
        }}>
        goToRegister
      </Button>
    </SafeAreaView>
  );
};
export default Login;
