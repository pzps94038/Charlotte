import AsyncStorage from '@react-native-async-storage/async-storage';
import {Token} from './userInfo.model';
export const tokenService = {
  getToken: async () => {
    const token = await AsyncStorage.getItem('token');
    if (token === null) {
      return null;
    } else {
      return JSON.parse(token) as Token;
    }
  },
  setToken: async (token: Token) => {
    await AsyncStorage.setItem('token', JSON.stringify(token));
  },
};
