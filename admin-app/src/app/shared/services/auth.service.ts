import { Injectable } from '@angular/core';
import { Profile, User, UserManager, UserManagerSettings } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseService {

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private manager = new UserManager(getClientSettings());
  private user: User | null;
  
  
  constructor() {
    super();
    this.manager.getUser().then(user => {
      this.user = user;
      this._authNavStatusSource.next(this.isAuthenticated());
    });
  }

  login() {
    return this.manager.signinRedirect();
  }

  async completeAuthentication() {
    this.user = await this.manager.signinRedirectCallback();
    this._authNavStatusSource.next(this.isAuthenticated());
  }

  isAuthenticated(): boolean {
    return this.user != null && !this.user.expired;
  }

  get authorizationHeaderValue(): string {
    if (this.user) {
      return `${this.user.token_type} ${this.user.access_token}`;
    }
    return null;
  }

  get name(): string {
    return this.user != null ? this.user.profile.name : '';
  }

  get profile(): Profile {
    return this.user != null ? this.user.profile : null;
  }
  async signout() {
    console.log({'id_token_hint' : this.user.id_token});
    console.log({'post_logout_redirect_uri' : this.manager.settings.post_logout_redirect_uri});
    console.log({'client_id' : this.manager.settings.client_id});
    await this.manager.signoutRedirect({'id_token_hint' : this.user.id_token});
  }

}
export function getClientSettings(): UserManagerSettings {
  return {
    authority: environment.authorityUrl, // đường dẫn đến link SSO
    client_id: environment.clientId, // quy định trong config IdentityServer
    redirect_uri: environment.adminUrl + '/auth-callback',
    post_logout_redirect_uri: environment.adminUrl,
    response_type: 'code',
    scope: 'api.webbase openid profile',
    filterProtocolClaims: true,
    loadUserInfo: true,
    automaticSilentRenew: true,
    silent_redirect_uri: environment.adminUrl + '/silent-refresh.html'
  };
}