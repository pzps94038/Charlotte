package com.example.charlotte.fragment.siteMap

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.ImageView
import com.example.charlotte.R
import com.example.charlotte.databinding.FragmentShoppingCartBinding
import com.example.charlotte.databinding.FragmentSiteMapBinding
import com.synnapps.carouselview.ImageClickListener
import com.synnapps.carouselview.ImageListener

class SiteMapFragment : Fragment() {
    private val imageArr : ArrayList<Int> = ArrayList()
    private var _binding: FragmentSiteMapBinding? = null
    private val binding get() = _binding!!
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        _binding = FragmentSiteMapBinding.inflate(inflater, container, false)
        init()
        return binding.root
    }
    // 晚點生成 使用靜態類別參考同個fragment
    companion object {
       val instance : SiteMapFragment by lazy{
           SiteMapFragment()
       }
    }
    private fun init(){
        setCarouselView()
    }
    private fun setCarouselView(){
        imageArr.add(R.drawable.coffee_noun_002_07436)
        binding.carouselView.pageCount = imageArr.size
        binding.carouselView.setImageListener(imageLister)
    }
    private var imageLister = ImageListener { position, imageView -> imageView?.setImageResource(imageArr[position]) }
}