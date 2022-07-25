import {SearchBar} from '@rneui/themed';
import React from 'react';
import {SafeAreaView, StyleSheet, Text} from 'react-native';
import {useDispatch, useSelector} from 'react-redux';
import {changeSearch, clearSearch} from '../shared/store/search/searchReducer';
import {RootState} from '../shared/store/store';

const Home = () => {
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
          console.warn('我要找' + search);
        }}
      />
      <Text>{search}</Text>
    </SafeAreaView>
  );
};
const style = StyleSheet.create({
  container: {
    flex: 1,
  },
});
export default Home;
