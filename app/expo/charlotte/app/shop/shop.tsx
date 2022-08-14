import * as React from 'react';
import { ScrollView, StyleSheet, Text } from 'react-native';
import { useSelector, useDispatch } from 'react-redux';
import { changeSearch, clearSearch } from '../shared/store/search/searchReducer';
import { RootState } from '../shared/store/store';
import { SearchBar } from '@rneui/themed';
import { SafeAreaView } from "react-native-safe-area-context";
const Shop = () => {
  const search = useSelector((state: RootState) => state.search);
  const dispatch = useDispatch();
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
          //TODO 搜尋功能
        }}
      />
      <ScrollView>
        <Text>123</Text>
      </ScrollView>
    </SafeAreaView>
  );
};
const style = StyleSheet.create({
  container: {
    flex: 1,
  },
});
export default Shop;
