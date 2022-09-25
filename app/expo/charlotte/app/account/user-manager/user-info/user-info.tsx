import { useNavigation } from "@react-navigation/native";
import { Button, Icon, ListItem } from "@rneui/themed";
import React from "react"
import { View } from "react-native";
import LoginAuth from "../../../shared/auth/auth";
import { Nav } from "../../../shared/router/nav";
import { tokenService } from "../../../shared/services/userInfo.service";

const UserInfo = ({ navigation }: any) => {
    const nav = useNavigation<Nav>();
    const logout = async () => {
        await tokenService.removeToken();
        nav.navigate('Account', {
            screen: 'Login'
        })
    }
    return (
        <LoginAuth navigation={navigation}>
            <View>
                <ListItem bottomDivider onPress={() => nav.navigate('UserSetting')}>
                    <Icon name='settings' type='feather' />
                    <ListItem.Content>
                        <ListItem.Title>用戶設定</ListItem.Title>
                    </ListItem.Content>
                    <ListItem.Chevron />
                </ListItem>
                <ListItem bottomDivider onPress={() => nav.navigate('Order')}>
                    <Icon name='file' type='feather' />
                    <ListItem.Content>
                        <ListItem.Title>歷史訂單</ListItem.Title>
                    </ListItem.Content>
                    <ListItem.Chevron />
                </ListItem>
                <View className="flex-row justify-center items-center">
                    <Button radius={10} type="clear" onPress={() => logout()} title='登出' />
                </View>

            </View>
        </LoginAuth>

    )
}
export default UserInfo;
