package com.example.charlotte.retrofitService
// 只有訊息
data class ResultMessage(
    val message: String,
    val code: String,
)
// 訊息跟資料
data class ResultData<T>(
    val message: String,
    val code: String,
    val data: T
)
// Token
data class Token(
    val accessToken: String,
    val refreshToken: String
)