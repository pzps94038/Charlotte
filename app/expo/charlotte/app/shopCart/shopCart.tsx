import { SafeAreaView, ScrollView, Text, View, TextInput } from 'react-native';
import * as React from 'react';
import { shopCartService } from '../shared/services/shopcart-service';
import { useRef, useState } from 'react';
import { ShopCart as ShopCartModel } from '../shared/services/shopcart.model';
import { useFocusEffect } from '@react-navigation/native';
import { Button } from '@rneui/themed';
import { useForm, Controller, useFieldArray, ValidationRule } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';

const ShopCart = () => {
  const [products, setProducts] = useState<ShopCartModel[]>([]);
  const validationSchema = Yup.object().shape({
    test: Yup.array().of(
      Yup.object().shape({
        amount: Yup.string().required('此欄位必填').matches(/^[1-9][0-9]*$/, '格式錯誤'),
      })
    ),
  });

  const { control, handleSubmit, setValue, resetField, formState: { errors, touchedFields } } = useForm({
    resolver: yupResolver(validationSchema)
  });

  const { fields, append } = useFieldArray({
    control,
    name: "test"
  });

  const onSubmit = (data: any) => { console.log({ data, touchedFields }); };
  useFocusEffect(React.useCallback(() => {
    shopCartService.getShopCart().then(products => {
      setProducts(products);
      for (const [idx, product] of Object.entries(products)) {
        setValue(`test.${idx}.amount`, product.amount.toString())
      }
    });
  }, []))

  return (
    <SafeAreaView>
      <ScrollView>
        <View style={{
          flex: 1,
          justifyContent: 'center',
          alignItems: 'center',
        }}>
          {
            products.map((a, idx) => {
              return (
                <Controller
                  key={a.productId}
                  control={control}
                  name={`test.${idx}.amount`}
                  render={({ field, fieldState: { error } }) => (
                    <View>
                      <TextInput
                        {...field}
                        keyboardType='numeric'
                        style={
                          {
                            width: 50,
                            height: 50,
                            backgroundColor: error ? 'red' : '#FFFF88'
                          }
                        }
                      />
                      {
                        <Text>
                          {errors?.['test']?.[idx]?.['amount']?.['message']}
                        </Text>
                      }
                    </View>
                  )}
                />
              )
            })
          }
          <Button title="設值" onPress={() => {
            setValue('test.0', { amount: '50' }, {
              shouldValidate: true,
              shouldDirty: true,
              shouldTouch: true,
            });
          }} />
          <Button title="Submit" onPress={handleSubmit(onSubmit)} />
        </View>
      </ScrollView>
    </SafeAreaView >
  );
};

export default ShopCart;
