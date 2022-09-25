import React, { useCallback, useState } from "react";
import { SafeAreaView } from "react-native-safe-area-context";
import { Text, Image, View, TextInput, TouchableOpacity } from "react-native";
import ScrollViewTab from "../../shared/component/scroll-view-tab";
import { useFocusEffect, useNavigation } from "@react-navigation/native";
import { ShopCart } from "../../shared/services/shopcart.model";
import { shopCartService } from "../../shared/services/shopcart.service";
import { BottomSheet, Button, Card, Icon, Input, ListItem } from "@rneui/themed";
import { baseURL } from "../../shared/api/Instance";
import { Nav } from "../../shared/router/nav";
import LoginAuth from "../../shared/auth/auth";

const Confirm = () => {
    const navigation = useNavigation<Nav>();
    const [products, setProducts] = useState<ShopCart[]>([]);
    const [isVisible, setIsVisible] = useState(false);
    useFocusEffect(
        useCallback(() => {
            shopCartService.getShopCart().then(products => setProducts(products));
        }, [])
    )
    return (
        <LoginAuth navigation={navigation}>
            <ScrollViewTab>
                <View>{
                    products.map(a => (
                        <ListItem key={a.productId} bottomDivider>
                            <View className="flex-row justify-between w-full">
                                <View className="flex-row items-center">
                                    <Image
                                        className='w-10 h-10'
                                        resizeMode="contain"
                                        source={{
                                            uri: baseURL + a.productImgPath
                                        }}
                                    />
                                    <ListItem.Title>{a.productName}</ListItem.Title>
                                </View>
                                <View className="flex-column items-start w-1/3">
                                    <Text>數量：{a.amount}</Text>
                                    <Text>單價：{a.price}</Text>
                                    <Text>總價：{a.amount * a.price}</Text>
                                </View>
                            </View>
                        </ListItem>
                    ))}
                </View>
                <ListItem bottomDivider>
                    <View className="w-full flex-row justify-between items-center">
                        <Text className="text-left">訂單金額：</Text>
                        <Text className="text-right text-sm font-bold mr-3 text-rose-600">${products.reduce((pre, cur) => {
                            return pre + (cur.amount * cur.price)
                        }, 0)}
                        </Text>
                    </View>
                </ListItem>
                <ListItem bottomDivider style={{
                    // borderColor: 'red',
                    // borderWidth: 1
                }} onPress={(
                ) => setIsVisible(true)}>
                    <View className="flex-row justify-between w-full">
                        <Text>請選擇付款方式</Text>
                        <ListItem.Chevron />
                    </View>
                </ListItem>
                <ListItem bottomDivider style={{
                    marginTop: 10
                }}>
                    <View className="w-full">
                        <View className="flex-row items-center">
                            <Icon
                                color="orange"
                                type="entypo"
                                name='news' />
                            <Text className="text-left ml-2">付款詳情</Text>
                        </View>
                        <View className="flex-row mt-3 w-full items-center py-2">
                            <Text className="text-left ml-2 w-2/12 text-xs">收件人：</Text>
                            <TextInput
                                autoCapitalize='none'
                                className="border-b w-10/12"
                                placeholder="請輸入收件人" />
                        </View>
                        <View className="flex-row mt-3 w-full items-center py-2">
                            <Text className="text-left ml-2 w-2/12 text-xs">地址：</Text>
                            <TextInput
                                autoCapitalize='none'
                                className="border-b w-10/12"
                                placeholder="請輸入地址" />
                        </View>
                    </View>
                </ListItem>
                <View className="flex-column items-center justify-center my-3">
                    <Button type="solid" radius={10}
                        title="去買單"
                        onPress={() => console.log('買單')}>
                    </Button>
                </View>
                <BottomSheet onBackdropPress={() => setIsVisible(false)} isVisible={isVisible}>
                    <Card containerStyle={{ paddingBottom: 300, borderRadius: 10 }}>
                        <Text>付款方式</Text>
                    </Card>
                </BottomSheet>
            </ScrollViewTab>
        </LoginAuth>

    )
}
export default Confirm;
