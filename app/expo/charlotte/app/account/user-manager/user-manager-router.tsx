import { useFocusEffect, useNavigation } from "@react-navigation/native"
import { createNativeStackNavigator } from "@react-navigation/native-stack"
import { Button } from "@rneui/base"
import React from "react"
import { View, Text } from "react-native"
import LoginAuth from "../../shared/auth/auth"
import { Nav } from "../../shared/router/nav"
import { tokenService } from "../../shared/services/userInfo.service"
import Order from "./order/order"
import UserInfo from "./user-info/user-info"
import UserSetting from "./user-setting/user-setting"

const UserManagerRouter = ({ navigation }: any) => {
    const UserManagerStack = createNativeStackNavigator();
    const nav = useNavigation<Nav>();
    const logout = async () => {
        await tokenService.removeToken();
        navigation.navigate('Account', {
            screen: 'Login'
        })
    }
    return (
        <UserManagerStack.Navigator initialRouteName="UserInfo">
            <UserManagerStack.Screen
                name="UserInfo"
                component={UserInfo}
                options={{
                    headerShown: false,
                    headerTitle: "帳戶資訊"
                }}
            />
            <UserManagerStack.Screen
                name="UserSetting"
                component={UserSetting}
                options={{
                    headerTitle: "用戶設定"
                }}
            />
            <UserManagerStack.Screen
                name="Order"
                component={Order}
                options={{
                    headerTitle: "歷史訂單"
                }}
            />
        </UserManagerStack.Navigator>
    )
}
export default UserManagerRouter;
