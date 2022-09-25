import React from "react";
import { ScrollView, View, Text } from "react-native";

type Props = {
    children: JSX.Element[] | JSX.Element,
    className?: string
};
const ScrollViewTab = ({ children, className }: Props) => {
    return (
        <ScrollView className={className}>
            {
                children
            }
            <View className="h-24" />
        </ScrollView>
    )
}
export default ScrollViewTab;