import { Button, Card, SearchBar } from '@rneui/themed';
import { Icon } from '@rneui/themed/dist/Icon';
import React, { useEffect, useState } from 'react';
import { Image, SafeAreaView, ScrollView, StyleSheet, Text, View } from 'react-native';
import Swiper from 'react-native-swiper';
import { useDispatch, useSelector } from 'react-redux';
import { changeSearch, clearSearch } from '../shared/store/search/searchReducer';
import { RootState } from '../shared/store/store';

const Home = () => {
  const search = useSelector((state: RootState) => state.search);
  const dispatch = useDispatch();
  useEffect(() => { }, [])
  return (
    <SafeAreaView style={style.container}>
      <SearchBar
        onChangeText={e => {
          dispatch(changeSearch(e));
        }}
        round={true}
        lightTheme={true}
        placeholder="請輸入你想找的商品"
        value={search}
        onCancel={() => {
          dispatch(clearSearch());
        }}
        onSubmitEditing={() => {
          console.warn('我要找' + search);
        }}
      />
      <ScrollView>
        <Swiper style={style.wrapper} autoplay={true} autoplayTimeout={5}>
          <View style={style.slide1}>
            <Text style={style.text}>Hello Swiper</Text>
          </View>
          <View style={style.slide2}>
            <Text style={style.text}>Beautiful</Text>
          </View>
          <View style={style.slide3}>
            <Text style={style.text}>And simple</Text>
          </View>
        </Swiper>
        <View style={style.Top10}>
          <Icon name="whatshot" color='red'></Icon><Text style={style.hotTitle}>十大熱賣商品</Text>
        </View>
        {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(() => {
          return (
            <Card>
              <Card.Title></Card.Title>
              <Card.Divider />
              <Image
                style={{ width: "100%", height: 150 }}
                resizeMode="contain"
                source={{ uri: "https://avatars0.githubusercontent.com/u/32242596?s=460&u=1ea285743fc4b083f95d6ee0be2e7bb8dcfc676e&v=4" }}
              />
              <Text style={{ textAlign: 'right', fontSize: 20, marginBottom: 10 }}>$50</Text>
              <Button
                title={'加入購物車'}
                // disabled={true}
                icon={{
                  name: 'shoppingcart',
                  type: 'antdesign',
                  size: 20,
                  color: 'white',
                }}
                containerStyle={{
                  marginLeft: 'auto',
                  width: 120,
                }}
              />
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
  slide1: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#9DD6EB'
  },
  slide2: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#97CAE5'
  },
  slide3: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#92BBD9'
  },
  text: {
    color: '#fff',
    fontSize: 30,
    fontWeight: 'bold'
  },
});
export default Home;
