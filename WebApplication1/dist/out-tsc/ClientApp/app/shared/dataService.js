import { __decorate } from "tslib";
import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Order, OrderItem } from './Order';
let DataService = class DataService {
    constructor(http) {
        this.http = http;
        this.order = new Order();
        this.Products = []; //type safe  Product[]
        this.token = "";
        this.expiration = new Date();
    }
    LoginRequired() {
        console.log("====================================================" + this.token.length);
        return (this.token.length == 0 || this.expiration < new Date());
    }
    Login(creads) {
        return this.http.post("/account/createtoken", creads)
            .pipe(map((response) => {
            let tokenInfo = response;
            this.token = tokenInfo.token;
            this.expiration = tokenInfo.expiration;
            return true;
        }));
    }
    checkout() {
        if (!this.order.orderNumber) {
            this.order.orderNumber = new Date().getUTCMilliseconds().toString();
        }
        console.log("token " + this.token);
        return this.http.post("api/orders", this.order, {
            headers: new HttpHeaders().set("Authorization", "Bearer " + this.token)
        })
            .pipe(map(response => {
            this.order = new Order();
            return true;
        }));
    }
    loadProducts() {
        return this.http.get("api/products")
            .pipe(map((data) => {
            this.Products = data;
            return true;
        }));
    }
    addToOrder(newProduct) {
        var item = this.order.items.find(p => p.productId == newProduct.id);
        if (item) {
            item.quantity++;
        }
        else {
            item = new OrderItem();
            item.productId = newProduct.id;
            item.productArtist = newProduct.artist;
            item.productArtId = newProduct.artId;
            item.productCategory = newProduct.category;
            item.productSize = newProduct.size;
            item.productTitle = newProduct.title;
            item.unitPrice = newProduct.price;
            item.quantity = 1;
            this.order.items.push(item);
        }
    }
};
DataService = __decorate([
    Injectable()
], DataService);
export { DataService };
//# sourceMappingURL=dataService.js.map