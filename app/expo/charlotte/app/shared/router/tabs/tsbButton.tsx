import { Animated, StyleSheet, TouchableOpacity, Text } from 'react-native';
import { BottomTabButton } from './tabs';
import { useEffect, useRef } from 'react';
import { Icon } from '@rneui/themed';
import * as React from 'react';
const TabButton = (props: BottomTabButton) => {
  const focus = props.accessibilityState?.selected ?? false;
  // 設定切換值
  let focusState = useRef(new Animated.Value(focus ? 0 : 1)).current;
  // 設定切換狀態
  const rotateInterpolate = focusState.interpolate({
    inputRange: [0, 1],
    outputRange: ['0deg', '360deg'],
  });
  const scaleInterpolate = focusState.interpolate({
    inputRange: [0, 1],
    outputRange: [1, 1.5],
  });
  useEffect(() => {
    // 調整值設定動畫時間
    if (focus) {
      Animated.timing(focusState, {
        toValue: 1,
        useNativeDriver: true,
        duration: 500,
      }).start();
    } else {
      Animated.timing(focusState, {
        toValue: 0,
        useNativeDriver: true,
        duration: 500,
      }).start();
    }
  }, [focus, focusState]);
  return (
    <TouchableOpacity onPress={props.onPress} style={styles.container}>
      <Animated.View
        style={{
          transform: [{ rotate: rotateInterpolate }, { scale: scaleInterpolate }],
        }}>
        <Icon name={props.name} color={focus ? '#E3E3E3' : '#8E8E8F'} />
      </Animated.View>
    </TouchableOpacity>
  );
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});
export default TabButton;
