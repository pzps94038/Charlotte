package com.example.charlotte

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.charlotte.appSharedPreferences.UserPreferences
import com.example.charlotte.databinding.ActivityLoginBinding
import com.example.charlotte.model.Token
import com.example.charlotte.retrofitService.Manager
import com.example.charlotte.retrofitService.ResultData
import com.example.charlotte.retrofitService.Service
import com.example.charlotte.retrofitService.User
import kotlinx.coroutines.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import kotlin.coroutines.CoroutineContext


class LoginActivity : AppCompatActivity(){
    private lateinit var binding: ActivityLoginBinding
    private var scope = object :CoroutineScope{
        override val coroutineContext: CoroutineContext
            get() = Job()
    }
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityLoginBinding.inflate(layoutInflater)
        setContentView(binding.root)
        init()
    }
    /**
     * 初始化事件綁定
     */
    private fun init(){
        binding.loginBtn.setOnClickListener(loginListener)
    }
    /**
     * 登入按鈕監聽事件
     */
    private val loginListener: View.OnClickListener = View.OnClickListener(){
        val account: String = binding.account.text.toString()
        val password: String = binding.password.text.toString()
        if(account == "" || password == "") Toast.makeText(this@LoginActivity,R.string.AccountOrPasswordCannotBeEmpty,Toast.LENGTH_SHORT).show()
        else loginPost(account, password)
    }
    /**
     * 登入功能
     * @param account = 帳號
     * @param password = 密碼
     */
    private fun loginPost(account: String, password: String){
        binding.progress.isIndeterminate = true
        scope.launch(Dispatchers.IO){
            val service: Service = Manager(this@LoginActivity).retrofit.create(Service::class.java)
            val user = User(account, password)
            service.login( user).enqueue(object: Callback<ResultData<Token>>{
                override fun onResponse(
                    call: Call<ResultData<Token>>,
                    response: Response<ResultData<Token>>
                ) {
                    response.body()?.apply {
                        if(this.code == 200){
                            val accessToken = this.data.accessToken
                            val refreshToken = this.data.refreshToken
                            UserPreferences.setUserToken(this@LoginActivity, accessToken, refreshToken)
                            startActivity(Intent(this@LoginActivity,MainActivity::class.java))
                        }else Toast.makeText(this@LoginActivity, this.message, Toast.LENGTH_SHORT).show()
                        binding.progress.isIndeterminate = false
                    }
                }
                override fun onFailure(call: Call<ResultData<Token>>, t: Throwable) {
                    binding.progress.isIndeterminate = false
                }
            })
        }
    }
    override fun onDestroy() {
        super.onDestroy()
        scope.cancel()
    }
}