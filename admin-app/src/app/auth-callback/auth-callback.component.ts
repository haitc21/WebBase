import { AuthService } from './../shared';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.scss']
})
export class AuthCallbackComponent implements OnInit {

  error: boolean;

  constructor(private _authService: AuthService,
    private _router: Router,
    private _route: ActivatedRoute) { }

  async ngOnInit() {

    // check for error
    if (this._route.snapshot.queryParams.error) {
      this.error = true;
      return;
    }

    await this._authService.completeAuthentication();
    this._router.navigate(['/']);
  }

}
