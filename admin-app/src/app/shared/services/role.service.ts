import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { PaginationModel, PermissionModel, RoleModel } from '../models';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends BaseService {
  private _sharedHeaders = new HttpHeaders();
  constructor(private _http: HttpClient) {
      super();
      this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
  }
  add(entity: RoleModel) {
      return this._http.post(`${environment.apiUrl}/api/roles`, JSON.stringify(entity), { headers: this._sharedHeaders })
          .pipe(catchError(this.handleError));
  }

  update(id: string, entity: RoleModel) {
      return this._http.put(`${environment.apiUrl}/api/roles/${id}`, JSON.stringify(entity), { headers: this._sharedHeaders })
          .pipe(catchError(this.handleError));
  }

  getDetail(id: string) {
      return this._http.get<RoleModel>(`${environment.apiUrl}/api/roles/${id}`, { headers: this._sharedHeaders })
          .pipe(catchError(this.handleError));
  }

  getAllPaging(filter: string, pageIndex: number, pageSize: number) {
      return this._http.get<PaginationModel<RoleModel>>(`${environment.apiUrl}/api/roles/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${filter}`, { headers: this._sharedHeaders })
          .pipe(map((response: PaginationModel<RoleModel>) => {
              return response;
          }), catchError(this.handleError));
  }

  delete(id: string) {
      return this._http.delete(environment.apiUrl + '/api/roles/' + id, { headers: this._sharedHeaders })
          .pipe(
              catchError(this.handleError)
          );
  }

  getAll() {
      return this._http.get<RoleModel[]>(`${environment.apiUrl}/api/roles`, { headers: this._sharedHeaders })
          .pipe(map((response: RoleModel[]) => {
              return response;
          }), catchError(this.handleError));
  }
  getRolePermissions(roleId: string) {
      return this._http.get<PermissionModel[]>(`${environment.apiUrl}/api/roles/${roleId}/permissions`, { headers: this._sharedHeaders })
          .pipe(catchError(this.handleError));
  }
}
