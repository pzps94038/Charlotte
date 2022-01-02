package com.example.charlotte

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Toast
import androidx.appcompat.app.ActionBar
import com.example.charlotte.databinding.ActivityLoginBinding


class Login_Activity : AppCompatActivity() {
    private lateinit var binding: ActivityLoginBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        // 隱藏
        val actionBar: ActionBar? = supportActionBar
        actionBar?.hide()
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
            Toast.makeText(this@Login_Activity,R.string.AccountOrPasswordCannotBeEmpty,Toast.LENGTH_SHORT)
                 .show()
        }
    }
    //endregion
}