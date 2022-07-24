import React, {useEffect, useRef} from 'react';
import {Animated} from 'react-native';
// children 子節點
const FadeIn = ({children}: {children: React.ReactNode}) => {
  console.warn('工作啦')
  // 不透明 -> 顯示
  // 耗費0.5秒
  const fadeAnim = useRef(new Animated.Value(0)).current;
  useEffect(() => {
    Animated.timing(fadeAnim, {
      toValue: 1,
      useNativeDriver: true,
      duration: 500,
    }).start();
  }, [fadeAnim]);
  return (
    <Animated.View
      style={{
        opacity: fadeAnim,
      }}>
      {children}
    </Animated.View>
  );
};
export default FadeIn;
