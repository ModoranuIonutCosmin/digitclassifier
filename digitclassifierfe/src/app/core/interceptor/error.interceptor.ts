import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticationService } from '../../services/auth.service';
import {AlertService} from "../../services/alert.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private authenticationService: AuthenticationService,private alertService:AlertService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      console.log(err)
      let error = err.statusText;
      if (err.status === 401) {
        // auto logout if 401 response returned from api
        this.authenticationService.logout();
        location.reload();
        return throwError(error);
      }
      if (err.status === 409) {
      error=err.error.detail
        return throwError(error);
      }
      if (err.status === 403) {
        error=err.error.title
        return throwError(error);
      }
      if (err.status ===0) {
        error="Server is down"

      }
     this.alertService.error(error);
      return throwError(error);
    }))
  }
}
