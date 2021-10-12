import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { environment } from '../../../environments/environment';
import { PermissionScreenModel, PermissionUpdateRequestModel } from '../models';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PermissionService extends BaseService {
  private _sharedHeaders = new HttpHeaders();
  constructor(private http: HttpClient) {
    super();
    this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
  }
  save(roleId: string, request: PermissionUpdateRequestModel) {
    return this.http.put(`${environment.apiUrl}/api/roles/${roleId}/permissions`, JSON.stringify(request),
      { headers: this._sharedHeaders })
      .pipe(catchError(this.handleError));
  }

  getFunctionWithCommands() {
    return this.http.get<PermissionScreenModel>(`${environment.apiUrl}/api/permissions`, { headers: this._sharedHeaders })
      .pipe(map((res: PermissionScreenModel) => res),catchError(this.handleError));
  }
}

