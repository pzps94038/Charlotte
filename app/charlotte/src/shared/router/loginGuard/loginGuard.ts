import {tokenService} from '../../services/userInfo-service';
const loginGuard = async () => {
  const token = await tokenService.getToken();
  if (token) {
    return true;
  } else {
    return false;
  }
};
export default loginGuard;
