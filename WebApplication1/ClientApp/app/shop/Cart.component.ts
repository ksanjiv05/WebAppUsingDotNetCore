import { Component } from '@angular/core'
import { DataService } from '../shared/dataService'
import { Router } from '@angular/router';

@Component({
    selector:"cart-app",
    templateUrl:'Cart.component.html',
    styleUrls:[]
})

export class Cart {
    constructor(public data: DataService, private route: Router) {

    }
    public OnCheckout() {

        
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
}