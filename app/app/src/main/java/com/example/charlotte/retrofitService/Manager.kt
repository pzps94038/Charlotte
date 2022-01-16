package com.example.charlotte.retrofitService

import android.content.Context
import com.example.charlotte.appSharedPreferences.Token
import com.example.charlotte.appSharedPreferences.UserPreferences
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class Manager() {
    val retrofit: Retrofit
    private val baseUrl: String = "http://10.0.2.2:7777/"
    init{
        retrofit = Retrofit.Builder()
                    .baseUrl(baseUrl)
                    .addConverterFactory(GsonConverterFactory.create())
                    .build()
    }
}