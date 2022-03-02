package com.example.charlotte.retrofitService

import com.example.charlotte.Model.Token
import retrofit2.Call
import retrofit2.http.*

interface Service {
    // 登入
    @POST(ApiUrl.LoginUrl)
    fun login(@Body user: User): Call<ResultData<Token>>
    //刷新Token
    @POST(ApiUrl.RefreshTokenUrl)
    fun getNewToken(@Body refreshToken: String?): Call<ResultData<Token>>
}
class User(account: String, password: String){
    private val account: String = account
    private val password: String = password
}