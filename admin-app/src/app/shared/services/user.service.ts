import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BaseService } from './base.service';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { UserModel, PaginationModel } from '../models';
import { UtilitiesService } from './utilitie.service';

@Injectable({ providedIn: 'root' })
/* @internal 
* Quản lý User
*/
export class UserService extends BaseService {
    private _sharedHeaders = new HttpHeaders();

    constructor(private _http: HttpClient,
        private _utilitiesService: UtilitiesService) {
        super();
        this._sharedHeaders = this._sharedHeaders.set('Content-Type', 'application/json');

    }

    getAll() {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        return this._http.get<UserModel[]>(`${environment.apiUrl}/api/users`, httpOptions)
            .pipe(map((response: UserModel[]) => {
                return response;
            }), catchError(this.handleError));
    }

    add(entity: UserModel) {
        return this._http.post(`${environment.apiUrl}/api/users`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }

    update(id: string, entity: UserModel) {
        return this._http.put(`${environment.apiUrl}/api/users/${id}`, JSON.stringify(entity), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }

    getDetail(id) {
        return this._http.get<UserModel>(`${environment.apiUrl}/api/users/${id}`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }

    getAllPaging(filter, pageIndex, pageSize) {
        return this._http.get<PaginationModel<UserModel>>(`${environment.apiUrl}/api/users/filter?pageIndex=${pageIndex}&pageSize=${pageSize}&filter=${filter}`, { headers: this._sharedHeaders })
            .pipe(map((response: PaginationModel<UserModel>) => {
                return response;
            }), catchError(this.handleError));
    }

    delete(id) {
        return this._http.delete(environment.apiUrl + '/api/users/' + id, { headers: this._sharedHeaders })
            .pipe(
                catchError(this.handleError)
            );
    }

    getMenuByUser(userId: string) {
        return this._http.get<Function[]>(`${environment.apiUrl}/api/users/${userId}/menu`, { headers: this._sharedHeaders })
            .pipe(map(response => {
                const functions = this._utilitiesService.UnflatteringForLeftMenu(response);
                return functions;
            }), catchError(this.handleError));
    }

    getUserRoles(userId: string) {
        return this._http.get<string[]>(`${environment.apiUrl}/api/users/${userId}/roles`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
    getRolesUserNotHas(userId: string) {
        return this._http.get<string[]>(`${environment.apiUrl}/api/users/${userId}/notroles`, { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }

    removeRolesFromUser(id, roleNames: string[]) {
        let rolesQuery = '';
        for (const roleName of roleNames) {
            rolesQuery += 'roleNames' + '=' + roleName + '&';
        }
        return this._http.delete(environment.apiUrl + '/api/users/' + id + '/roles?' + rolesQuery, { headers: this._sharedHeaders })
            .pipe(
                catchError(this.handleError)
            );
    }

    assignRolesToUser(userId: string, assignRolesToUser: any) {
        return this._http.post(`${environment.apiUrl}/api/users/${userId}/roles`,
            JSON.stringify(assignRolesToUser), { headers: this._sharedHeaders })
            .pipe(catchError(this.handleError));
    }
}
