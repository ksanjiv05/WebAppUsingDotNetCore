import { __decorate } from "tslib";
import { Component } from '@angular/core';
let Login = class Login {
    constructor(data, router) {
        this.data = data;
        this.router = router;
        this.creads = {
            username: "",
            password: ""
        };
        this.errorMessage = "";
    }
    OnLogin() {
        this.errorMessage = "";
        this.data.Login(this.creads)
            .subscribe(success => {
            if (success) {
                if (this.data.order.items.length == 0) {
                    this.router.navigate([""]);
                }
                else {
                    this.router.navigate(["checkout"]);
                }
            }
        }, err => this.errorMessage = "Failed to login");
    }
};
Login = __decorate([
    Component({
        selector: "the-login",
        templateUrl: "Login.component.html",
        styleUrls: []
    })
], Login);
export { Login };
//# sourceMappingURL=Login.component.js.map