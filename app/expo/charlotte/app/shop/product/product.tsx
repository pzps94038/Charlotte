import React, { useCallback, useState } from "react";
import { ScrollView, Text, View, Image, TouchableOpacity } from "react-native";
import { useFocusEffect, useNavigation } from "@react-navigation/native";
import { getProducts } from "../../shared/api/product/prodcut.service";
import { ifSuccess } from "../../shared/services/shared.service";
import { ProductResponse } from "../../shared/api/product/product.model";
import { baseURL } from "../../shared/api/Instance";
import { Button, Card, Icon } from "@rneui/themed";
import { Nav } from "../../shared/router/nav";
const Product = ({ route }: any) => {
    const [products, setProducts] = useState<ProductResponse[]>([]);
    const navigation = useNavigation<Nav>();
    const { typeId } = route.params;
    useFocusEffect(useCallback(() => {
        (async () => {
            const res = await getProducts(typeId);
            ifSuccess(res) ? setProducts(res.data) : setProducts([]);
        })()
    }, []))
    return (
        <View className="flex-1">
            {products.length > 0 ?
                <ScrollView>
                    {
                        products.map(a => {
                            return (
                                <TouchableOpacity key={a.productId} onPress={() => {
                                    navigation.navigate('ProductDetail', {
                                        productId: a.productId
                                    });
                                }}>
                                    <Card>

                                        <Image
                                            className=" w-full h-44"
                                            resizeMode="contain"
                                            source={{ uri: baseURL + a.productImgPath }}
                                        />
                                        <Text className="my-1 text-base font-bold">
                                            商品名稱：{a.productName}
                                        </Text>
                                        <Text className="my-1 text-sm font-bold">
                                            售價：
                                            <Text className="text-orange-400">{`$${a.sellPrice}`}</Text>
                                        </Text>
                                        <Text className="my-1 text-sm font-bold">
                                            剩餘庫存量：
                                            <Text>{`${a.inventory}`}</Text>
                                        </Text>
                                    </Card>
                                </TouchableOpacity>
                            )
                        })
                    }
                </ScrollView>
                :
                <View className="flex-1 justify-center items-center mb-36">
                    <Icon name="page-search" type="foundation" size={100} />
                    <Text className="text-center font-semibold text-lg">沒有找到任何商品</Text>
                </View>
            }

        </View>
    )
}
export default Product;
