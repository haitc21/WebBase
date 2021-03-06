import { NgxSpinnerService } from 'ngx-spinner';
import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../router.animations';
import { AuthService } from '../shared/services';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [routerTransition()]
})
export class LoginComponent implements OnInit {
    constructor(
        private _authService: AuthService,
        private _spinner: NgxSpinnerService
    ) { }

    ngOnInit() {
        this._spinner.show();
        this._authService.login();
    }

    // login() {
    //     this._spinner.show();
    //     this._authService.login();
    // }

}
