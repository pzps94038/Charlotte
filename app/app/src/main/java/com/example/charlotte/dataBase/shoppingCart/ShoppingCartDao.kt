package com.example.charlotte.dataBase.shoppingCart

import androidx.room.*

interface IShoppingCartDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun addCart()
    @Update
    fun updateCart(productAmount: Int)
    @Delete()
    fun deleteCart(productID: Int)
    @Query("Select * from ShoppingCartProduct")
    fun getShoppingCartProucts(): MutableList<ShoppingCartProduct>
}