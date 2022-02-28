package com.example.charlotte.dataBase.shoppingCart

import androidx.room.*
@Dao
interface IShoppingCartDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    fun addCart(product: ShoppingCartProduct)
    @Update
    fun updateCart(product: ShoppingCartProduct)
    @Delete
    fun deleteCart(product: ShoppingCartProduct)
    @Query("Select * from ShoppingCartProduct")
    fun getShoppingCartProucts(): MutableList<ShoppingCartProduct>
}