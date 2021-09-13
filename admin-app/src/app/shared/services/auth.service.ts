import { Injectable } from '@angular/core';
import { Profile, User, UserManager, UserManagerSettings } from 'oidc-client';
import { BehaviorSubject } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ProfileModel } from '../models';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
/** @internal
 * Quản lý đăng nhập, đăng xuất
 * Authen và autho bằng SSO với Server IS4
 */
export class AuthService extends BaseService {

  // Observable navItem source
  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  // Observable navItem stream
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private _userManager = new UserManager(getClientSettings());
  private _user: User | null;
  
  
  constructor() {
    super();
    this._userManager.getUser().then(user => {
      this._user = user;
      this._authNavStatusSource.next(this.isAuthenticated());
    });
  }

  login() {
    return this._userManager.signinRedirect();
  }

  async completeAuthentication() {
    this._user = await this._userManager.signinRedirectCallback();
    this._authNavStatusSource.next(this.isAuthenticated());
  }

  isAuthenticated(): boolean {
    return this._user != null && !this._user.expired;
  }

  get authorizationHeaderValue(): string {
    if (this._user) {
      return `${this._user.token_type} ${this._user.access_token}`;
    }
    return null;
  }

  get name(): string {
    return this._user != null ? this._user.profile.name : '';
  }

  get profile(): ProfileModel {
    if (this._user != null) {
      let resul = new ProfileModel(
        this._user.profile.sub,
        this._user.profile.name,
        this._user.profile.email,
        this._user.profile.role,
        this._user.profile.Permissions
      );
      console.log(resul);
      return resul;
    }
    return null;
  }
  async signout() {
    await this._userManager.signoutRedirect({'id_token_hint' : this._user.id_token});
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