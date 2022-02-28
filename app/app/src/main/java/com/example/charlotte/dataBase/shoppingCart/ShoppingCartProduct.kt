package com.example.charlotte.dataBase.shoppingCart

import androidx.annotation.NonNull
import androidx.room.Entity
import androidx.room.PrimaryKey

@Entity
class ShoppingCartProduct(@PrimaryKey val productID: Int,
                          @NonNull val productName: String,
                          @NonNull var productAmount: Int) {

}