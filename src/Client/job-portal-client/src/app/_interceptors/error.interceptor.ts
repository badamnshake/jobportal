import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error) => {
        if (error) {
          console.log(error);
        }
        if (error) {
          switch (error.status) {
            case 400:
              // // if (error.error.errors) {
              // //   const modalStateErrors = [];
              // //   for (const key in error.error.errrors) {
              // //     if (error.error.errors[key]) {
              // //       modalStateErrors.push(error.error.errors[key]);
              // //     }
              // //   }
              // //   throw modalStateErrors.flat();
              // } else {
              this.toastr.error(error.error,'Item Not Found');
              // }
              break;
            case 401:
              this.toastr.error(error.error,'Unauthorized');
              break;
            case 500:
              this.toastr.error(error.error,'Server Error');
              break;
            case 404:
              this.router.navigateByUrl('/not-found');
              break;
            default:
              this.toastr.error('Something unexpected went wrong');
              console.log(error);
              break;
          }
        }
        return throwError(() => new Error(error.message));
      })
    );
  }
}
