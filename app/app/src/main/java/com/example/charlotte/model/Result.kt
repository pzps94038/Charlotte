package com.example.charlotte.retrofitService
// 只有訊息
data class ResultMessage(
    val message: String,
    val code: Int,
)
// 訊息跟資料
data class ResultData<T>(
    val message: String,
    val code: Int,
    val data: T
)
