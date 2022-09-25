import Toast from "react-native-toast-message";
import { ResultModel } from "../api/api.model";

export const ifSuccess = (response: ResultModel<any>) => {
  if (response.code !== 200) {
    Toast.show({
      type: "error",
      text1: response.message,
    });
    return false;
  } else return true;
};
