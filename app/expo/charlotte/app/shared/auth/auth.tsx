import { useFocusEffect, useNavigation } from "@react-navigation/native"
import React, { useCallback, useState } from "react"
import { SafeAreaView, ScrollView, View } from "react-native"
import { Nav } from "../router/nav";
import { tokenService } from "../services/userInfo.service";
type Props = {
    children: JSX.Element[] | JSX.Element,
    navigation: {
        setOptions: (options: any) => void
    }
};
const LoginAuth = ({ children, navigation }: Props) => {
    const [isLogin, setIsLogin] = useState<boolean>(false);
    const nav = useNavigation<Nav>();
    useFocusEffect(
        useCallback(() => {
            (async () => {
                const token = await tokenService.getToken();
                if (token) {
                    navigation.setOptions({
                        headerShown: true
                    })
                    setIsLogin(true);
                } else {
                    setIsLogin(false);
                    nav.navigate('Account', {
                        screen: 'Login',
                    });
                }
            })();
        }, [])
    )
    return (
        <SafeAreaView>
            <ScrollView>
                {
                    (isLogin && {
                        ...children
                    })
                }
            </ScrollView>
        </SafeAreaView>
    )
}
export default LoginAuth;