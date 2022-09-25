import React from "react";
import { View, Text } from 'react-native';
import LoginAuth from "../../../shared/auth/auth";
const UserSetting = ({ navigation }: any) => {
    return (
        <LoginAuth navigation={navigation}>
            <View>
                <Text>用戶設定</Text>
            </View>
        </LoginAuth>

    )
}
export default UserSetting;
