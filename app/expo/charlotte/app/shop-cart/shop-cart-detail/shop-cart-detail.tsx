import { Text, View, TextInput, Image, TouchableOpacity } from 'react-native';
import * as React from 'react';
import { useCallback, useState } from 'react';
import { useFocusEffect, useNavigation } from '@react-navigation/native';
import { Button, Card, Icon } from '@rneui/themed';
import { useForm, Controller, useFieldArray } from "react-hook-form";
import { yupResolver } from '@hookform/resolvers/yup';
import * as Yup from 'yup';
import { baseURL } from '../../shared/api/Instance';
import { Nav } from '../../shared/router/nav';
import { ShopCart } from '../../shared/services/shopcart.model';
import { shopCartService } from '../../shared/services/shopcart.service';
import ScrollViewTab from '../../shared/component/scroll-view-tab';

const ShopCartDetail = () => {
    const navigation = useNavigation<Nav>();
    const [products, setProducts] = useState<ShopCart[]>([]);
    const [refresh, setRefresh] = useState<number>(Math.random());
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

    const onSubmit = () => {
        navigation.navigate('Confirm');
    };
    const removeProduct = async (productId: number) => {
        await shopCartService.removeProduct(productId);
        setRefresh(Math.random());
    }

    const productPlus = async (product: ShopCart, value: string, idx: number) => {
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

    const productMinus = async (product: ShopCart, value: string, idx: number) => {
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
        <ScrollViewTab>
            <View>
                {
                    products.length > 0 ?
                        products.map((a, idx) => {
                            return (
                                <Controller
                                    key={a.productId}
                                    control={control}
                                    name={`shopCart.${idx}.amount`}
                                    render={({ field, fieldState: { error } }) => (
                                        <View>
                                            <Card>
                                                <View className='flex-row justify-between'>
                                                    <Text className="text-left text-base">
                                                        {a.productName}
                                                    </Text>
                                                    <TouchableOpacity onPress={() => { removeProduct(a.productId) }}>
                                                        <Icon
                                                            size={24}
                                                            name='close'
                                                            type='evilicon'
                                                            color='red'
                                                        />
                                                    </TouchableOpacity>
                                                </View>
                                                <View className='flex-row justify-between items-center'>
                                                    <Image
                                                        className='w-32 h-32'
                                                        resizeMode="contain"
                                                        source={{
                                                            uri: baseURL + a.productImgPath
                                                        }}
                                                    />
                                                    <View className="flex-row">
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
                                                            className="mx-2 bg-orange-50 w-14 h-10 text-center rounded-lg text-xs border-2"
                                                            style={
                                                                {
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
                                                        <Text className="absolute right-0 top-12 text-rose-600">單價：${a.price}</Text>
                                                    </View>
                                                </View>
                                                {
                                                    <Text className="text-rose-600 text-right" style={{
                                                        marginVertical: error ? 10 : 0
                                                    }}>
                                                        {errors?.['shopCart']?.[idx]?.['amount']?.['message']}
                                                    </Text>
                                                }
                                                <Text className="text-right text-zinc-500">
                                                    剩下{a.inventory}個商品
                                                </Text>
                                            </Card>
                                        </View>
                                    )}
                                />
                            )
                        })
                        :
                        <View className="flex-column items-center justify-center h-full mt-16">
                            <Icon name='cart-outline' type='ionicon' size={100} />
                            <Text className="font-bold text-base my-3">購物車是空的～</Text>
                            <Button type="solid" radius={10}
                                title="來去逛逛"
                                onPress={() => navigation.navigate('Shop')}>
                            </Button>
                        </View>
                }
                {
                    products.length > 0 && (
                        <View className="flex justify-center items-center mt-3">
                            <Button title="結帳" radius={10} onPress={handleSubmit(onSubmit)} />
                        </View>
                    )
                }
            </View>
        </ScrollViewTab>
    );
};

export default ShopCartDetail;
