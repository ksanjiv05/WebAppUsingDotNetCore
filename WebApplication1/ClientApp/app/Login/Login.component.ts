import { Component } from '@angular/core';
import { DataService } from '../shared/dataService';
import { Router } from '@angular/router';

@Component({
    selector: "the-login",
    templateUrl: "Login.component.html",
    styleUrls: []

})

export class Login {
    constructor(private data: DataService, private router: Router)
    { }

    public creads = {
        username:"",
        password:""
    }
    public errorMessage: string = "";
    public OnLogin()
    {
        this.errorMessage = "";
        this.data.Login(this.creads)
            .subscribe(success => {
                if (success) {
                    if (this.data.order.items.length == 0) {
                        this.router.navigate([""]);
                    } else {
                        this.router.navigate(["checkout"]);
                    }
                }
            }, err => this.errorMessage = "Failed to login");
    }
    
}


