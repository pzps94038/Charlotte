package com.example.charlotte

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Toast
import com.example.charlotte.appSharedPreferences.UserPreferences
import com.example.charlotte.databinding.ActivityLoginBinding
import com.example.charlotte.retrofitService.Manager
import com.example.charlotte.retrofitService.ResultData
import com.example.charlotte.retrofitService.Service
import com.example.charlotte.retrofitService.Token
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response


class LoginActivity : AppCompatActivity() {
    private lateinit var binding: ActivityLoginBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityLoginBinding.inflate(layoutInflater)
        setContentView(binding.root)
        init()
    }
    //region 初始化
    private fun init(){
        binding.loginBtn.setOnClickListener(loginListener)
    }
    //endregion
    //region 登入按鈕監聽
    private val loginListener: View.OnClickListener = View.OnClickListener(){
        val account: String = binding.account.text.toString()
        val password: String = binding.password.text.toString()
        Log.d("帳號",account)
        if(account == "" || password == ""){
            Toast.makeText(this@LoginActivity,R.string.AccountOrPasswordCannotBeEmpty,Toast.LENGTH_SHORT).show()
        }else{
//            loginPost(account, password)
            startActivity(Intent(this@LoginActivity,MainActivity::class.java))
        }
    }
    //endregion
    //region 登入(Post)
    private fun loginPost(account: String, password: String){
        CoroutineScope(Dispatchers.IO).launch {
            val service: Service = Manager().retrofit.create(Service::class.java)
            service.login(account, password).enqueue(object: Callback<ResultData<Token>>{
                override fun onResponse(
                    call: Call<ResultData<Token>>,
                    response: Response<ResultData<Token>>
                ) {
                    response.body()?.apply {
                        if(this.code == 200){
                            val accountToken = this.data.accessToken
                            val refreshToken = this.data.refreshToken
                            UserPreferences.setUserToken(this@LoginActivity, accountToken, refreshToken)
                            startActivity(Intent(this@LoginActivity,MainActivity::class.java))
                        }
                    }

                }

                override fun onFailure(call: Call<ResultData<Token>>, t: Throwable) {
                    // TODO("Not yet implemented")
                }
            })
        }
    }
    //endregion
}