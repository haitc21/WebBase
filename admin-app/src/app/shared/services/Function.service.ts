import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { CommandAssignModel, CommandInFunctionModel, FunctionModel } from '../models';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class FunctionService extends BaseService {
  private _sharedHeaders = new HttpHeaders();
  constructor(private http: HttpClient) {
      super();
      this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
  }
  add(entity: Function) {
      return this.http.post(`${environment.apiUrl}/api/functions`, JSON.stringify(entity), { headers: this._sharedHeaders })
          .pipe(catchError(this.handleError));
  }

  update(id: string, entity: Function) {
      return this.http.put(`${environment.apiUrl}/api/functions/${id}`, JSON.stringify(entity), { headers: this._sharedHeaders })
          .pipe(catchError(this.handleError));
  }

  getDetail(id) {
      return this.http.get<Function>(`${environment.apiUrl}/api/functions/${id}`, { headers: this._sharedHeaders })
          .pipe(catchError(this.handleError));
  }

  delete(id) {
      return this.http.delete(environment.apiUrl + '/api/functions/' + id, { headers: this._sharedHeaders })
          .pipe(
              catchError(this.handleError)
          );
  }

  getAll() {
      return this.http.get<FunctionModel[]>(`${environment.apiUrl}/api/functions`, { headers: this._sharedHeaders })
          .pipe(map((response: FunctionModel[]) => {
              return response;
          }), catchError(this.handleError));
  }

  getAllByParentId(parentId) {
      let url = '';
      if (parentId) {
          url = `${environment.apiUrl}/api/functions/${parentId}/parents`;
      } else {
          url = `${environment.apiUrl}/api/functions/`;
      }

      return this.http.get<FunctionModel[]>(url, { headers: this._sharedHeaders })
          .pipe(map((response: FunctionModel[]) => {
              return response;
          }), catchError(this.handleError));
  }

  getAllCommandsByFunctionId(functionId: string) {
      return this.http.get<CommandInFunctionModel>(`${environment.apiUrl}/api/functions/${functionId}/commands`, { headers: this._sharedHeaders })
          .pipe(map((response: CommandInFunctionModel) => {
              return response;
          }), catchError(this.handleError));
  }
  addCommandsToFunction(functionId, commandAssign: CommandAssignModel) {
      return this.http.post(`${environment.apiUrl}/api/functions/${functionId}/commands/`, JSON.stringify(commandAssign)
          , { headers: this._sharedHeaders })
          .pipe(
              catchError(this.handleError)
          );
  }
}
