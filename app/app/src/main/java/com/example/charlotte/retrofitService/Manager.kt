package com.example.charlotte.retrofitService

import android.content.Context
import com.example.charlotte.appSharedPreferences.Token
import com.example.charlotte.appSharedPreferences.UserPreferences
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class Manager() {
    val retrofit: Retrofit = Retrofit.Builder()
                .baseUrl(ApiUrl.BaseUrl)
                .addConverterFactory(GsonConverterFactory.create())
                .build()
}