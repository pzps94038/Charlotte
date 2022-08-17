import { ScrollView, Text, View, TextInput, StyleSheet, Image } from 'react-native';
import * as React from 'react';
import { shopCartService } from '../shared/services/shopcart-service';
import { useCallback, useEffect, useRef, useState } from 'react';
import { ShopCart as ShopCartModel } from '../shared/services/shopcart.model';
import { useFocusEffect } from '@react-navigation/native';
import { Button, Card, Icon } from '@rneui/themed';
import { useForm, Controller, useFieldArray, ValidationRule } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { SafeAreaView } from 'react-native-safe-area-context';
import { baseURL } from '../shared/api/Instance';
import DropDownPicker from 'react-native-dropdown-picker';
const ShopCart = () => {
  const [products, setProducts] = useState<ShopCartModel[]>([]);
  const [refresh, setRefresh] = useState<number>(Math.random());

  useFocusEffect(
    useCallback(() => {
      shopCartService.getShopCart().then(products => {
        setProducts(products);
        for (const [idx, product] of Object.entries(products)) {
          setValue(`shopCart.${idx}.productId`, product.productId, {
            shouldValidate: true,
            shouldDirty: true,
            shouldTouch: true,
          });
          setValue(`shopCart.${idx}.amount`, product.amount.toString(),
            {
              shouldValidate: true,
              shouldDirty: true,
              shouldTouch: true,
            })
        }
      });
    }, [refresh])
  )

  const validationSchema = Yup.object().shape({
    shopCart: Yup.array().of(
      Yup.object().shape({
        productId: Yup.number().required('此欄位必填'),
        amount: Yup.string().required('此欄位必填').matches(/^[1-9][0-9]*$/, '格式錯誤').test('inventory', '超過庫存量', (val, controller) => {
          const productId = controller.parent.productId;
          const inventory = products.find(a => a.productId === productId)?.inventory ?? 0
          return inventory >= parseInt(val ?? '0');
        }),
      })
    ),
  });

  const { control, handleSubmit, setValue, getValues, formState: { errors, touchedFields } } = useForm({
    resolver: yupResolver(validationSchema),
    reValidateMode: 'onBlur'
  });

  const { fields, append } = useFieldArray({
    control,
    name: "shopCart"
  });

  const onSubmit = (data: any) => { };

  const removeProduct = async (productId: number) => {
    await shopCartService.removeProduct(productId);
    setRefresh(Math.random());
  }

  const productPlus = async (product: ShopCartModel, value: string, idx: number) => {
    await shopCartService.fixProduct({
      ...product,
      amount: (parseInt(value) + 1)
    });
    setValue(`shopCart.${idx}.amount`, (parseInt(value) + 1).toString(),
      {
        shouldValidate: true,
        shouldDirty: true,
        shouldTouch: true,
      })
  }

  const productMinus = async (product: ShopCartModel, value: string, idx: number) => {
    if ((parseInt(value) - 1) > 0) {
      await shopCartService.fixProduct({
        ...product,
        amount: (parseInt(value) - 1)
      });
      setValue(`shopCart.${idx}.amount`, (parseInt(value) - 1).toString(),
        {
          shouldValidate: true,
          shouldDirty: true,
          shouldTouch: true,
        })
    }
  }

  return (
    <SafeAreaView>
      <ScrollView>
        <View style={style.container}>
          {
            products.map((a, idx) => {
              return (
                <Controller
                  key={a.productId}
                  control={control}
                  name={`shopCart.${idx}.amount`}
                  render={({ field, fieldState: { error } }) => (
                    <View style={{
                      width: '90%'
                    }}>
                      <Card containerStyle={{
                        position: 'relative',
                      }}>
                        <Icon
                          size={24}
                          name='close'
                          type='evilicon'
                          color='red'
                          containerStyle={{
                            position: 'absolute',
                            right: 0
                          }}
                          onPress={() => { removeProduct(a.productId) }}
                        />
                        <Card.Title style={{ fontSize: 18, textAlign: 'left' }}>
                          {a.productName}
                        </Card.Title>
                        <View style={{ flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between' }}>
                          <Image
                            style={{ width: "30%", height: 80 }}
                            resizeMode="contain"
                            source={{
                              uri: baseURL + a.productImgPath
                            }}
                          />
                          <View style={{ width: "60%", flexDirection: 'row', flexWrap: 'wrap', position: 'relative' }}>
                            <Button type="solid" containerStyle={{
                              borderRadius: 10,
                            }}
                              onPress={() => productMinus(a, field.value, idx)}>
                              <Icon name='minus'
                                type='antdesign' color="white" />
                            </Button>
                            <TextInput
                              {...field}
                              onChangeText={field.onChange}
                              keyboardType='numeric'
                              style={
                                {
                                  ...style.textInput,
                                  borderColor: error ? 'red' : '#d3d3d3'
                                }
                              }
                            />
                            <Button type="solid" containerStyle={{
                              borderRadius: 10,
                            }} onPress={() => productPlus(a, field.value, idx)}>
                              <Icon name='plus'
                                type='antdesign' color="white" />
                            </Button>
                            <Text style={{ position: 'absolute', right: 0, bottom: -20, color: '#cd6700' }}>${a.price}</Text>
                          </View>
                        </View>
                        {
                          <Text style={style.error}>
                            {errors?.['shopCart']?.[idx]?.['amount']?.['message']}
                          </Text>
                        }
                        <Text style={{
                          textAlign: 'right',
                          color: '#888888'
                        }}>
                          剩下{a.inventory}個商品
                        </Text>
                      </Card>

                    </View>
                  )}
                />
              )
            })
          }
          <Button title="Submit" onPress={handleSubmit(onSubmit)} />
        </View>
        <View style={{ height: 80 }}></View>
      </ScrollView>
    </SafeAreaView >
  );
};

const style = StyleSheet.create({
  title: {
    fontSize: 18
  },
  textInput: {
    height: 40,
    width: 60,
    marginHorizontal: 5,
    textAlign: 'center',
    borderWidth: 1,
    borderRadius: 10,
    backgroundColor: '#d3d3d3'
  },
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  error: {
    color: 'red',
    textAlign: 'right'
  }
})

export default ShopCart;
