import {SearchBar} from '@rneui/themed';
import React, {useState} from 'react';
import {SafeAreaView, StyleSheet} from 'react-native';

const Home = () => {
  let [search, setSearch] = useState('');
  return (
    <SafeAreaView style={style.container}>
      <SearchBar
        onChangeText={setSearch}
        round={true}
        lightTheme={true}
        placeholder="請輸入你想找的商品"
        value={search}
        onSubmitEditing={() => {
          console.warn('我要找' + search);
        }}
      />
    </SafeAreaView>
  );
};
const style = StyleSheet.create({
  container: {
    flex: 1,
  },
});
export default Home;
