import { useFocusEffect, useNavigation } from "@react-navigation/native";
import { ListItem } from "@rneui/base";
import { Icon } from "@rneui/themed";
import React, { useState, useCallback } from "react";
import { ScrollView, View } from "react-native";
import { getProductTypes } from "../../shared/api/product/prodcut.service";
import { ProductTypesResponse } from "../../shared/api/product/product.model";
import { Nav } from "../../shared/router/nav";
import { ifSuccess } from "../../shared/services/shared.service";

const ProductType = () => {
    const [productTypes, setProductTypes] = useState<ProductTypesResponse[]>([]);
    const navigation = useNavigation<Nav>();
    useFocusEffect(useCallback(() => {
        (async () => {
            const res = await getProductTypes();
            if (ifSuccess(res)) {
                setProductTypes(res.data);
            }
        })()
    }, []))
    return (
        <ScrollView>
            <View>
                {
                    productTypes.map(a => (
                        <ListItem key={a.productTypeId} bottomDivider onPress={(
                        ) => {
                            navigation.navigate('Product', {
                                typeId: a.productTypeId
                            });
                        }}>
                            <Icon name={a.iconName} type={a.iconType} />
                            <ListItem.Content>
                                <ListItem.Title>{a.type}</ListItem.Title>
                            </ListItem.Content>
                            <ListItem.Chevron />
                        </ListItem>

                    ))
                }
            </View>
        </ScrollView>
    )
}
export default ProductType;
