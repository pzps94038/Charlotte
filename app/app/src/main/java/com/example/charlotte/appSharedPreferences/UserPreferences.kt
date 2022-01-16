package com.example.charlotte.appSharedPreferences

import android.content.Context
import android.content.SharedPreferences

class UserPreferences {
    companion object{
        private const val TokenStr: String = "Token"
        private const val accessTokenStr: String = "accessToken"
        private const val refreshTokenStr: String = "refreshToken"

        // 取Token
        fun getUserToken(context: Context): Token{
            val tokenPre :SharedPreferences = context.getSharedPreferences(TokenStr, Context.MODE_PRIVATE)
            val accessToken: String? = tokenPre.getString(accessTokenStr,"")
            val refreshToken: String? = tokenPre.getString(refreshTokenStr, "")
            return Token(accessToken, refreshToken)
        }

        // 設置Token
        fun setUserToken(context: Context, accessToken: String, refreshToken: String){
            val tokenPre :SharedPreferences = context.getSharedPreferences(TokenStr, Context.MODE_PRIVATE)
            tokenPre.edit().putString(accessTokenStr, accessToken)
                           .putString(refreshTokenStr, refreshToken)
                           .apply()
        }
    }
}