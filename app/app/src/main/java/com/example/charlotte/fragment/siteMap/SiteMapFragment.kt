package com.example.charlotte.fragment.siteMap

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.example.charlotte.R


// TODO: Rename parameter arguments, choose names that match
// the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
private const val ARG_PARAM1 = "param1"
private const val ARG_PARAM2 = "param2"

/**
 * A simple [Fragment] subclass.
 * Use the [SiteMapFragment.newInstance] factory method to
 * create an instance of this fragment.
 */
class SiteMapFragment : Fragment() {
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        return inflater.inflate(R.layout.fragment_site_map, container, false)
    }
    // 晚點生成 使用靜態類別參考同個fragment
    companion object {
       val instance : SiteMapFragment by lazy{
           SiteMapFragment()
       }
    }
}