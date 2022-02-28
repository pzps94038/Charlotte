package com.example.charlotte.fragment.shoppingCart

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.charlotte.R
import com.example.charlotte.databinding.ActivityShoppingCartRowBinding
import com.example.charlotte.databinding.FragmentShoppingCartBinding

class ShoppingCartFragment : Fragment() {
    private var _binding: FragmentShoppingCartBinding? = null
    private val binding get() = _binding!!
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        // Inflate the layout for this fragment
        _binding = FragmentShoppingCartBinding.inflate(inflater, container, false)
        init()
        return binding.root
    }

    companion object {
        val instance: ShoppingCartFragment by lazy{
            ShoppingCartFragment()
        }
    }
    private fun init(){
        binding.shoppingCartRecyclerView.layoutManager = LinearLayoutManager(activity)
        binding.shoppingCartRecyclerView.setHasFixedSize(true)
        binding.shoppingCartRecyclerView.adapter = ViewAdapter()
    }
    private inner class ViewAdapter: RecyclerView.Adapter<ViewHolder>(){
        override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
            val binding = ActivityShoppingCartRowBinding.inflate(LayoutInflater.from(parent.context), parent, false)
            return ViewHolder(binding)
        }

        override fun onBindViewHolder(holder: ViewHolder, position: Int) {

        }

        override fun getItemCount(): Int {
            return 2
        }

    }
    inner class ViewHolder(binding: ActivityShoppingCartRowBinding): RecyclerView.ViewHolder(binding.root)
    {
//        val btn = binding.shoppingCartRowPlusBtn

    }
}