import { useFocusEffect, useNavigation } from "@react-navigation/native";
import { Card, Icon, SearchBar } from "@rneui/themed";
import React, { useCallback, useState } from "react"
import { ScrollView, Text, TouchableOpacity, Image, View } from "react-native"
import { SafeAreaView } from "react-native-safe-area-context";
import { useDispatch, useSelector } from "react-redux";
import { baseURL } from "../../shared/api/Instance";
import { search as searchApi } from "../../shared/api/product/prodcut.service";
import { SearchResponse } from "../../shared/api/product/product.model";
import ScrollViewTab from "../../shared/component/scroll-view-tab";
import SharedSearchBar from "../../shared/component/shared-search-bar";
import { Nav } from "../../shared/router/nav";
import { ifSuccess } from "../../shared/services/shared.service";
import { changeSearch, clearSearch } from "../../shared/store/search/search.reducer";
import { RootState } from "../../shared/store/store";

const Search = () => {
    const [products, setProducts] = useState<SearchResponse[]>([]);
    const search = useSelector((state: RootState) => state.search);
    const navigation = useNavigation<Nav>();
    useFocusEffect(useCallback(() => {
        (async () => {
            await reSearch();
        })()
    }, []))
    const navigationProduct = (productId: number) => {
        navigation.navigate('Shop', {
            screen: 'ProductDetail',
            params: {
                productId
            }
        })
    }
    const reSearch = async () => {
        if (search !== "") {
            const res = await searchApi(search);
            if (ifSuccess(res))
                setProducts(res.data);
        }
    }

    return (
        <SafeAreaView>
            <SharedSearchBar searchEvent={reSearch} />
            {
                products.length > 0 ? (
                    <ScrollViewTab>
                        {products.map((a) => {
                            return (
                                <TouchableOpacity key={a.productId} onPress={() => navigationProduct(a.productId)}>
                                    <Card>
                                        <Card.Title>
                                            <Text className="text-left text-sm">{a.productName}</Text>
                                        </Card.Title>
                                        <Card.Divider />
                                        <Image
                                            style={{ width: "100%", height: 150 }}
                                            source={{ uri: baseURL + a.productImgPath }}
                                        />
                                    </Card>
                                </TouchableOpacity>
                            )
                        })}
                    </ScrollViewTab>
                ) :
                    (
                        <View className="flex-1 justify-center items-center h-full">
                            <Icon name="page-search" type="foundation" size={100} />
                            <Text className="text-center font-semibold text-lg mb-40">沒有找到任何商品</Text>
                        </View>
                    )
            }

        </SafeAreaView>
    )
}
export default Search;
