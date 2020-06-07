import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient } from  '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductList } from './shop/ProductList.component'
import { DataService } from './shared/dataService';
import { Cart } from './shop/Cart.component';
import { RouterModule } from '@angular/router';
import { Shop } from './shop/Shop.component';
import { Checkout } from './checkout/checkout.component';
import { Login } from './Login/Login.component';
import { FormsModule } from '@angular/forms';


const Route = [

    { path: "", component: Shop },
    { path: "checkout", component: Checkout },
    { path:"login",component :Login}
];

@NgModule({
  declarations: [
        AppComponent,
        ProductList,
        Cart,
        Shop,
        Checkout,
        Login
       
  ],
  imports: [
      BrowserModule,
      //AppRoutingModule,
      HttpClientModule,
      RouterModule.forRoot(Route, {
          useHash: true,
          enableTracing: true//for route debugging
      })
      ,
      FormsModule
  ],
    providers: [
        DataService
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
