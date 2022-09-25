import React from "react";
import { View, Text } from 'react-native';
import LoginAuth from "../../../shared/auth/auth";
const Order = ({ navigation }: any) => {
    return (
        <LoginAuth navigation={navigation}>
            <View>
                <Text>訂單</Text>
            </View>
        </LoginAuth>
    )
}
export default Order;
