import { __decorate } from "tslib";
import { Component } from '@angular/core';
let Cart = class Cart {
    constructor(data, route) {
        this.data = data;
        this.route = route;
    }
    OnCheckout() {
        alert("checkout : ---  ");
        if (this.data.LoginRequired()) {
            alert("login");
            this.route.navigate(["login"]);
        }
        else {
            this.route.navigate(["checkout"]);
            alert("checkout");
        }
    }
};
Cart = __decorate([
    Component({
        selector: "cart-app",
        templateUrl: 'Cart.component.html',
        styleUrls: []
    })
], Cart);
export { Cart };
//# sourceMappingURL=Cart.component.js.map