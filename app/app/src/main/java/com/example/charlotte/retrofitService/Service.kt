package com.example.charlotte.retrofitService

import retrofit2.Call
import retrofit2.http.*

interface Service {
    // 登入
    @POST("")
    @Headers("Content-Type: application/json")
    fun login(@Url url: String, @Body account: String, @Body password: String): Call<ResultData<Token>>
}