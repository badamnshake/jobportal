import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root',
})
/* -------------------------------------------------------------------------- */
/*            for protecting routes only which logged in user can access     */
/* -------------------------------------------------------------------------- */
export class AuthLoginGuard implements CanActivate {
  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}
  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((user) => {
        if (user) return true;
        this.toastr.error('You must be logged in to access this Route');
        return false;
      })
    );
  }
}
