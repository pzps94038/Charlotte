package com.example.charlotte.dataBase.shoppingCart

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
@Database(entities = arrayOf(ShoppingCartProduct::class), version = 1)
abstract class ShoppingCartDataBase : RoomDatabase(){
    abstract fun shoppingCartDao(): IShoppingCartDao
    companion object{
        private val dbName: String = "shoppingCart.db"
        private var database: ShoppingCartDataBase? = null
        fun getDataBase(context: Context): ShoppingCartDataBase?{
            if(database == null){
                database = Room.databaseBuilder(context, ShoppingCartDataBase::class.java, dbName).build()
            }
            return database
        }
    }
}