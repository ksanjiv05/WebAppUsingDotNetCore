import { __decorate } from "tslib";
import { Component } from "@angular/core";
let ProductList = class ProductList {
    constructor(data) {
        this.data = data;
        this.Products = [];
    }
    ngOnInit() {
        this.data.loadProducts()
            .subscribe(success => {
            if (success) {
                this.Products = this.data.Products;
            }
        });
    }
    addProduct(Product) {
        this.data.addToOrder(Product);
    }
};
ProductList = __decorate([
    Component({
        selector: "Product-List",
        templateUrl: "./ProductList.component.html",
        styleUrls: ["./ProductList.component.css"]
    })
], ProductList);
export { ProductList };
//# sourceMappingURL=ProductList.component.js.map