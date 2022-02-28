package com.example.charlotte.dataBase.shoppingCart

import androidx.annotation.NonNull
import androidx.room.Entity

@Entity
class ShoppingCartProduct(@NonNull val productID: Int,
                          @NonNull val productName: String,
                          @NonNull var productAmount: Int) {

}