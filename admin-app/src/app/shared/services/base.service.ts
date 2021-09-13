import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
/** 
 * @internal
 * Handle errors cho các service kế thừa nó
 * qua hàm handleError
 *  */
export class BaseService {
protected handleError(errorResponse: any) {
  if (errorResponse.error.message) {
      return throwError(errorResponse.error.message || 'Server error');
  }

  if (errorResponse.error.errors) {
      let modelStateErrors = '';

      // for now just concatenate the error descriptions, alternative we could simply pass the entire error response upstream
      for (const errorMsg of errorResponse.error.errors) {
          modelStateErrors += errorMsg + '<br/>';
      }
      return throwError(modelStateErrors || 'Server error');
  }
  return throwError('Server error');
}
}
