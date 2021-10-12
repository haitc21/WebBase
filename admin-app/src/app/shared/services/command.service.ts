import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { environment } from '../../../environments/environment';
import { CommandModel } from '../models';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CommandService extends BaseService {
  private _sharedHeaders = new HttpHeaders();
  constructor(private http: HttpClient) {
    super();
    this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');
  }
  getAll() {
    return this.http.get<CommandModel[]>(`${environment.apiUrl}/api/commands`, { headers: this._sharedHeaders })
      .pipe(map((response: CommandModel[]) => {
        return response;
      }), catchError(this.handleError));
  }
}