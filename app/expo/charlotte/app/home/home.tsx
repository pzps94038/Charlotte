import { Card, Icon, SearchBar } from "@rneui/themed";
import { useState } from "react";
import * as React from 'react';
import { ScrollView, View, Image, Text, TouchableOpacity } from "react-native";
import Swiper from "react-native-swiper";
import { useSelector, useDispatch } from "react-redux";
import { baseURL } from "../shared/api/Instance";
import { Top10Response } from "../shared/api/top10/top10.model";
import { getTop10 } from "../shared/api/top10/top10.service";
import { changeSearch, clearSearch } from "../shared/store/search/search.reducer";
import { RootState } from "../shared/store/store";
import { useFocusEffect, useNavigation } from "@react-navigation/native";
import { SafeAreaView } from "react-native-safe-area-context";
import { Nav } from "../shared/router/nav";
import ScrollViewTab from "../shared/component/scroll-view-tab";
import { ifSuccess } from "../shared/services/shared.service";
import { CommonActions } from '@react-navigation/native';
import SharedSearchBar from "../shared/component/shared-search-bar";
const Home = () => {

  const [top10, setTop10] = useState<Top10Response[]>([]);

  const navigation = useNavigation<Nav>();
  useFocusEffect(React.useCallback(() => {
    (async () => {
      const result = await getTop10();
      if (ifSuccess(result)) {
        setTop10(result.data);
      }
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

  return (
    <SafeAreaView>
      <ScrollViewTab className="flex-1">
        <SharedSearchBar />
        <ScrollView>
          <Swiper className="h-72" autoplay={true} autoplayTimeout={5}>
            <View className="flex flex-row justify-center">
              <Image
                style={{ width: "100%" }}
                resizeMode="cover"
                source={require('../../assets/image/banner1.jpg')}
              />
            </View>
            <View className="flex flex-row justify-center">
              <Image
                style={{ width: "100%" }}
                resizeMode="cover"
                source={require('../../assets/image/banner2.jpg')}
              />
            </View>
            <View className="flex flex-row justify-center">
              <Image
                style={{ width: "100%" }}
                resizeMode="cover"
                source={require('../../assets/image/banner3.jpg')}
              />
            </View>
          </Swiper>
          <View className="flex flex-row justify-center mt-2.5">
            <Icon name="whatshot" color='red'></Icon><Text className="text-center text-lg">十大熱賣商品</Text>
          </View>
          {top10.map((a) => {
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
        </ScrollView>
      </ScrollViewTab >
    </SafeAreaView>

  );
};

export default Home;
