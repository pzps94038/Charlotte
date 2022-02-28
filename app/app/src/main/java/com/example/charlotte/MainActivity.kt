package com.example.charlotte

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.example.charlotte.databinding.ActivityMainBinding
import com.example.charlotte.fragment.hotList.HotListFragment
import com.example.charlotte.fragment.memberCentre.MemberCentreFragment
import com.example.charlotte.fragment.shoppingCart.ShoppingCartFragment
import com.example.charlotte.fragment.siteMap.SiteMapFragment
import com.google.android.material.navigation.NavigationBarView

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        // 綁定切換Fragment事件
        binding.bottomNavigation.setOnItemSelectedListener(selectedListener)
    }
    // 切換Fragment
    private val selectedListener: NavigationBarView.OnItemSelectedListener = NavigationBarView.OnItemSelectedListener(){
        when(it.itemId){
            R.id.siteMap_btn -> {
                val transaction = supportFragmentManager.beginTransaction()
                transaction.replace(R.id.fragmentContainerView, SiteMapFragment.instance)
                transaction.disallowAddToBackStack()
                transaction.commit()
                true
            }
            R.id.hotList_btn -> {
                val transaction = supportFragmentManager.beginTransaction()
                transaction.replace(R.id.fragmentContainerView, HotListFragment.instance)
                transaction.disallowAddToBackStack()
                transaction.commit()
                true
            }

            R.id.shoppingCart_btn ->{
                val transaction = supportFragmentManager.beginTransaction()
                transaction.replace(R.id.fragmentContainerView, ShoppingCartFragment.instance)
                transaction.disallowAddToBackStack()
                transaction.commit()
                true
            }
            R.id.memberCentre_btn ->{
                val transaction = supportFragmentManager.beginTransaction()
                transaction.replace(R.id.fragmentContainerView, MemberCentreFragment.instance)
                transaction.disallowAddToBackStack()
                transaction.commit()
                true
            }
            else -> {
                val transaction = supportFragmentManager.beginTransaction()
                transaction.replace(R.id.fragmentContainerView, SiteMapFragment.instance)
                transaction.disallowAddToBackStack()
                transaction.commit()
                true
            }
        }
    }
}