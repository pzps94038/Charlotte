package com.example.charlotte.retrofitService


import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.core.content.ContextCompat.startActivity
import com.example.charlotte.LoginActivity
import com.example.charlotte.Model.Token
import com.example.charlotte.appSharedPreferences.UserPreferences
import okhttp3.Interceptor
import okhttp3.OkHttpClient
import okhttp3.Response
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class Manager(context: Context) {
    private val token : Token = UserPreferences.getUserToken(context)
    private val accessToken : String = token.accessToken
    private val refreshToken : String = token.refreshToken
    // api攔截器
    private val client = OkHttpClient.Builder().addInterceptor( object : Interceptor{
        override fun intercept(chain: Interceptor.Chain): Response {
            val req = chain.request().newBuilder()
                .addHeader("Content-Type", "application/json")
                .addHeader("Authorization", "Bearer $accessToken")
            val res = chain.proceed(req.build())
            if(res.code() == 401) {
                val newToken = getNewToken(refreshToken, context)
                // 身分驗證過期
                if(newToken == ""){
                    Toast.makeText(context, "身分驗證以過期", Toast.LENGTH_SHORT).show()
                    startActivity(context, Intent(context,LoginActivity::class.java), Bundle())
                }else{
                    val newReq = req.addHeader("Authorization", "Bearer $newToken")
                    return chain.proceed(newReq.build())
                }
            }
            return res
        }

    }).build()
    val retrofit: Retrofit = Retrofit.Builder()
                .baseUrl(ApiUrl.BaseUrl)
                .addConverterFactory(GsonConverterFactory.create())
                .client(client)
                .build()
    // 刷新Token
    private fun getNewToken(refreshToken: String, context: Context): String{
        var token = ""
        retrofit.create(Service::class.java).getNewToken(refreshToken).enqueue(
            object: Callback<ResultData<Token>>{
                override fun onResponse(
                    call: Call<ResultData<Token>>,
                    response: retrofit2.Response<ResultData<Token>>
                ) {
                    response.body()?.apply {
                        UserPreferences.setUserToken(context, this.data.accessToken, this.data.refreshToken)
                        token =  this.data.accessToken
                    }
                }
                override fun onFailure(call: Call<ResultData<Token>>, t: Throwable) {}
            }
        )
        return token
    }
}

