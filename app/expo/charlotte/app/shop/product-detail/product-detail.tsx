import { useFocusEffect } from "@react-navigation/native";
import React, { useCallback, useState } from "react";
import { View, Text, ScrollView, Image, StyleSheet, TouchableOpacity, TextInput } from "react-native";
import { getProduct } from "../../shared/api/product/prodcut.service";
import RenderHtml from 'react-native-render-html';
import { useWindowDimensions } from 'react-native';
import ScrollViewTab from "../../shared/component/scroll-view-tab";
import { ProductResponse as Product } from "../../shared/api/product/product.model";
import { ifSuccess } from "../../shared/services/shared.service";
import { baseURL } from "../../shared/api/Instance";
import { BottomSheet, Button, Card, Divider, Icon } from "@rneui/themed";
import { shopCartService } from "../../shared/services/shopcart.service";
import Toast from "react-native-toast-message";
import * as Yup from 'yup';
import { yupResolver } from "@hookform/resolvers/yup";
import { Controller, useForm } from "react-hook-form";
const ProductDetail = ({ route }: any) => {
    const { productId } = route.params;
    const { width } = useWindowDimensions();
    const [source, setSource] = useState<{
        html: string
    }>({ html: '' });
    const [product, setProduct] = useState<Product>();
    const [isVisible, setIsVisible] = useState(false);
    const validationSchema = Yup.object().shape({
        amount: Yup.string().required('此欄位必填').matches(/^[1-9][0-9]*$/, '格式錯誤').test('inventory', '超過庫存量', (val, controller) => {
            const inventory = product?.inventory ?? 0
            return inventory >= parseInt(val ?? '0');
        }),
    });
    const { control, handleSubmit, setValue, reset, formState: { errors, touchedFields } } = useForm({
        defaultValues: {
            amount: "1"
        },
        resolver: yupResolver(validationSchema),
        reValidateMode: 'onBlur'
    });
    const productPlus = (value: string) => {
        setValue('amount', (parseInt(value) + 1).toString(),
            {
                shouldValidate: true,
                shouldDirty: true,
                shouldTouch: true,
            })
    }

    const productMinus = (value: string) => {
        if ((parseInt(value) - 1) > 0) {
            setValue('amount', (parseInt(value) - 1).toString(),
                {
                    shouldValidate: true,
                    shouldDirty: true,
                    shouldTouch: true,
                })
        }
    }
    const onSubmit = async ({ amount }: { amount: string }) => {
        await shopCartService.addProduct({
            productId: product!.productId,
            amount: parseInt(amount),
            productImgPath: product!.productImgPath,
            productName: product!.productName,
            price: product!.sellPrice,
            inventory: product!.inventory
        });
        Toast.show({
            type: 'success',
            text1: `${product!.productName}已加入購物車`,
        });
        hiddenSheet();
    };

    const hiddenSheet = () => {
        setIsVisible(false);
        reset({ amount: '1' });
    }

    useFocusEffect(useCallback(() => {
        (async () => {
            const res = await getProduct(productId);
            if (ifSuccess(res)) {
                const data = res.data;
                setProduct(data);
                setSource({
                    html: data.productDescription
                })
            }
        })()
    }, []))
    return (
        <ScrollViewTab>
            <Image
                style={{ width: "100%", height: 300 }}
                resizeMode="contain"
                source={{ uri: baseURL + product?.productImgPath }}
            />
            <Card>
                <Text className="text-base">產品名稱：{product?.productName}</Text>
                <Text className="text-base">售價：<Text className="text-rose-600">${product?.sellPrice}</Text></Text>
                <Text className="text-base">產品類型：{product?.type}</Text>
                <Text className="text-base">剩餘庫存：{product?.inventory}</Text>
                {
                    product?.inventory! > 0 ?
                        <Button radius={10} title={'加入購物車'} icon={{ name: 'shoppingcart', type: 'antdesign', size: 20, color: 'white', }}
                            containerStyle={{
                                marginLeft: 'auto',
                                width: 140,
                            }}
                            onPress={() => setIsVisible(true)}
                        />
                        :
                        <Button title={'已售完'}
                            disabled={true}
                            radius={10}
                            containerStyle={{
                                marginLeft: 'auto',
                                width: 140,
                            }}
                        />
                }
                <Text className="text-base">產品說明：</Text>
                <RenderHtml
                    contentWidth={width}
                    source={source}
                />
            </Card>
            <BottomSheet onBackdropPress={() => hiddenSheet()} isVisible={isVisible}>
                <Card containerStyle={{
                    borderRadius: 10,
                }}>
                    <TouchableOpacity onPress={() => hiddenSheet()} style={{
                        marginLeft: 'auto',
                    }}>
                        <Icon
                            size={30}
                            name='close'
                            type='evilicon'
                            color='red'
                        />
                    </TouchableOpacity>
                    <View className="mb-5">
                        <Image
                            className=" w-1/2 h-32"
                            resizeMode="contain"
                            source={{ uri: baseURL + product?.productImgPath }}
                        />
                        <View  >
                            <Text className="text-base">{product?.productName}</Text>
                            <Text className="text-base text-rose-600 font-bold">${product?.sellPrice}</Text>
                        </View>

                    </View>
                    <Card.Divider />
                    <View className="flex-row justify-between items-center my-5">
                        <Text>購買數量</Text>
                        <Controller
                            control={control}
                            name="amount"
                            render={({ field, fieldState: { error } }) => {
                                return (
                                    <View className="flex-row">
                                        <Button type="solid" containerStyle={{
                                            borderRadius: 10,
                                        }}
                                            onPress={() => productMinus(field.value)} >
                                            <Icon name='minus'
                                                type='antdesign' color="white" />
                                        </Button>
                                        <TextInput
                                            {...field}
                                            className="mx-2 bg-orange-50 w-14 h-10 text-center rounded-lg text-xs border-2"
                                            onChangeText={field.onChange}
                                            keyboardType='numeric'
                                            style={
                                                {
                                                    borderColor: error ? 'red' : '#d3d3d3'
                                                }
                                            }
                                        />
                                        <Button type="solid" containerStyle={{
                                            borderRadius: 10,
                                        }} onPress={() => productPlus(field.value)}>
                                            <Icon name='plus'
                                                type='antdesign' color="white" />
                                        </Button>
                                        <Text className=" absolute right-0 top-11 text-rose-600">{error?.message}</Text>
                                    </View>
                                )
                            }}
                        />

                    </View>
                    <Button title="加入購物車" containerStyle={{
                        marginVertical: 10
                    }} radius={10} onPress={handleSubmit(onSubmit)} />
                </Card>
            </BottomSheet>
        </ScrollViewTab>
    )
}
export default ProductDetail;

