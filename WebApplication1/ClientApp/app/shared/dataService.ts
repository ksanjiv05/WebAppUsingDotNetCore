import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Product } from './Product';
import { Order, OrderItem } from './Order';
import { Data } from '@angular/router';

@Injectable()

export class DataService {

    public order: Order = new Order();
    constructor(private http: HttpClient)
    { }

    public Products: Product[] = [];//type safe  Product[]

    private token: string = "";
    private expiration: Data = new Date();

    public LoginRequired() {
       
        console.log("===================================================="+this.token.length);
        return (this.token.length == 0 || this.expiration < new  Date())
    }
    public Login(creads): Observable<boolean>{
        return this.http.post("/account/createtoken", creads)
            .pipe(
                map((response: any) => {
                    let tokenInfo = response;
                    this.token = tokenInfo.token;
                    this.expiration = tokenInfo.expiration;
                    return true;
                }));
    }

    public checkout(): Observable<boolean>
    {
        if (!this.order.orderNumber) {
            this.order.orderNumber = new Date().getUTCMilliseconds().toString();
        }
        console.log("token " +this.token);
        return this.http.post("api/orders", this.order, {
            headers: new HttpHeaders().set("Authorization", "Bearer " + this.token)
            })
            .pipe(
            map(response => {
                    this.order = new Order();
                    return true;
                })
            )
    }

    loadProducts(): Observable<boolean> {//type safe Observable<boolean>

        return this.http.get("api/products")
            .pipe(
                map((data: any[]) => {
                    this.Products = data;
                    return true;
                })
            )
    }

    public addToOrder(newProduct: Product) {


        var item: OrderItem = this.order.items.find(p => p.productId == newProduct.id)

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
   

    //public Products = [{
    //    title: "first product",
    //    price: 89
    //},
    //{
    //    title: "2nd product",
    //    price: 800
    //},
    //{
    //    title: "3rd product",
    //    price: 98
    //},
    //{
    //    title: "4th product",
    //    price: 87
    //}];
}