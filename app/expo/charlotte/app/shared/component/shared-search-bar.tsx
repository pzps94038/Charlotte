
import { useDispatch, useSelector } from "react-redux";
import { changeSearch, clearSearch } from "../store/search/search.reducer";
import { SearchBar } from "@rneui/themed";
import React from "react";
import { RootState } from "../store/store";
import { useNavigation } from "@react-navigation/native";
import { Nav } from "../router/nav";
const SharedSearchBar = ({ searchEvent }: {
    searchEvent?: () => Promise<void>
}) => {
    const navigation = useNavigation<Nav>();
    const dispatch = useDispatch();
    const search = useSelector((state: RootState) => state.search);
    const submit = () => {
        if (search !== "") {
            navigation.navigate('Shop', {
                screen: 'Search'
            });
        }
    }
    return (
        <SearchBar
            onChangeText={(e: string) => dispatch(changeSearch(e))}
            round={true}
            lightTheme={true}
            placeholder="請輸入你想找的商品"
            value={search}
            onCancel={() => {
                dispatch(clearSearch());
            }}
            onSubmitEditing={() => searchEvent ? searchEvent() : submit()}
        />
    );
}
export default SharedSearchBar;

