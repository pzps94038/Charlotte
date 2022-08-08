import { Button, Card, Icon, SearchBar } from "@rneui/themed";
import { useState, useEffect } from "react";
import * as React from 'react';
import { SafeAreaView, ScrollView, View, Image, Text, StyleSheet } from "react-native";
import Swiper from "react-native-swiper";
import Toast from "react-native-toast-message";
import { useSelector, useDispatch } from "react-redux";
import { baseURL } from "../shared/api/Instance";
import { Top10 } from "../shared/api/top10/top10.interface";
import { getTop10 } from "../shared/api/top10/top10Service";
import { shopCartService } from "../shared/services/shopcart-service";
import { changeSearch, clearSearch } from "../shared/store/search/searchReducer";
import { RootState } from "../shared/store/store";

const Home = () => {
  const search = useSelector((state: RootState) => state.search);
  const [top10, setTop10] = useState<Top10[]>([]);
  const dispatch = useDispatch();

  const addCart = async (product: Top10) => {
    await shopCartService.addProduct({
      productId: product.productId,
      amount: 1,
      productImgPath: product.productImgPath,
      productName: product.productName,
      price: product.sellPrice
    });
    Toast.show({
      type: 'success',
      text1: `${product.productName}已加入購物車`,
    });
  }
  useEffect(() => {
    (async () => {
      const result = await getTop10();
      setTop10(result.data);
    })()
  }, [])
  return (
    <SafeAreaView style={style.container}>
      <SearchBar
        onChangeText={(e: string) => dispatch(changeSearch(e))}
        round={true}
        lightTheme={true}
        placeholder="請輸入你想找的商品"
        value={search}
        onCancel={() => {
          dispatch(clearSearch());
        }}
        onSubmitEditing={() => {
          console.log('我要找' + search);
        }}
      />
      <ScrollView>
        <Swiper style={style.wrapper} autoplay={true} autoplayTimeout={5}>
          <View style={style.slide}>
            <Image
              style={{ width: "100%" }}
              resizeMode="cover"
              source={require('../../assets/image/banner1.jpg')}
            />
          </View>
          <View style={style.slide}>
            <Image
              style={{ width: "100%" }}
              resizeMode="cover"
              source={require('../../assets/image/banner2.jpg')}
            />
          </View>
          <View style={style.slide}>
            <Image
              style={{ width: "100%" }}
              resizeMode="cover"
              source={require('../../assets/image/banner3.jpg')}
            />
          </View>
        </Swiper>
        <View style={style.Top10}>
          <Icon name="whatshot" color='red'></Icon><Text style={style.hotTitle}>十大熱賣商品</Text>
        </View>
        {top10.map((a) => {
          return (
            <Card key={a.productId}>
              <Card.Title style={{ textAlign: 'left', marginLeft: 5, fontSize: 18 }}>{a.productName}</Card.Title>
              <Card.Divider />
              <Image
                style={{ width: "100%", height: 150 }}
                resizeMode="contain"
                source={{ uri: baseURL + a.productImgPath }}
              />
              <Text style={{ textAlign: 'right', fontSize: 20, marginBottom: 10 }}>${a.sellPrice}</Text>
              {
                a.inventory > 0 ?
                  <Button title={'加入購物車'} icon={{ name: 'shoppingcart', type: 'antdesign', size: 20, color: 'white', }}
                    containerStyle={{
                      marginLeft: 'auto',
                      width: 120,
                    }}
                    onPress={() => addCart(a)}
                  />
                  :
                  <Button title={'已售完'}
                    disabled={true}
                    containerStyle={{
                      marginLeft: 'auto',
                      width: 120,
                    }}
                  />
              }
            </Card>
          )
        })}
        <View style={{ height: 80 }}></View>
      </ScrollView>
    </SafeAreaView >
  );
};
const style = StyleSheet.create({
  container: {
    flex: 1,
  },
  wrapper: {
    height: 280,
  },
  Top10: {
    marginTop: 20,
    flexDirection: 'row',
    justifyContent: 'center',
  },
  hotTitle: {
    textAlign: 'center',
    fontSize: 20
  },
  slide: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'flex-start',
  },
  text: {
    color: '#fff',
    fontSize: 30,
    fontWeight: 'bold'
  },
});
export default Home;
