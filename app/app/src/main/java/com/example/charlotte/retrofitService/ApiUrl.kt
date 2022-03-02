package com.example.charlotte.retrofitService

class ApiUrl {
    companion object{
        const val BaseUrl: String = "http://10.0.2.2:8081/" // 基底網址
        private const val BaseApiUrl: String = BaseUrl + "api/" // Api基底網址
        const val LoginUrl: String = BaseApiUrl + "Login" // 登入
        const val RegisterUrl: String = BaseApiUrl + "Register" // 註冊
        const val RefreshTokenUrl: String = BaseApiUrl + "RefreshToken" // 刷新Token接口
    }

}