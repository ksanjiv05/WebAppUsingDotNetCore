
import { Component, OnInit } from "@angular/core";
import { SelectorContext, templateJitUrl } from '@angular/compiler';
import { DataService } from '../shared/dataService';
import { Product } from '../shared/Product'

@Component({
    selector: "Product-List",
    templateUrl: "./ProductList.component.html",
    styleUrls: ["./ProductList.component.css"]

})




export class ProductList implements OnInit {

    constructor(private data: DataService)
    {
       
    }
    public Products: Product[] = [];

    ngOnInit(): void {
        this.data.loadProducts()
            .subscribe(success => {
                if (success) {
                    this.Products = this.data.Products;
                }
            });
    }

    public addProduct(Product: Product) {
        this.data.addToOrder(Product);


    }
}