import {Icon} from '@rneui/base';
import {Animated, StyleSheet, TouchableOpacity} from 'react-native';
import {BottomTabButton} from './tabs';
import React, {useEffect, useRef} from 'react';
const TabButton = (props: BottomTabButton) => {
  const focus = props?.accessibilityState?.selected ?? false;
  let fadeAnim = useRef(new Animated.Value(0)).current;
  useEffect(() => {
    // console.log('執行');
    if (focus) {
      Animated.timing(fadeAnim, {
        toValue: 1,
        useNativeDriver: true,
        duration: 500,
      }).start();
      console.log('haha是我啦');
    } else {
      Animated.timing(fadeAnim, {
        toValue: 0,
        useNativeDriver: true,
        duration: 500,
      }).start();
      console.log('goodbye');
    }
  }, [focus, fadeAnim]);
  return (
    <TouchableOpacity onPress={props.onPress} style={styles.container}>
      <Animated.View
        style={{
          opacity: fadeAnim,
        }}>
        <Icon name={props.name} color={focus ? '#4B0091' : '#8E8E8F'} />
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
