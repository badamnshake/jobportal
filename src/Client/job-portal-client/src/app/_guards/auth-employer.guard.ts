import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root',
})

/* -------------------------------------------------------------------------- */
/*            for protecting routes only which employer can access     
 it checks the roles and role is employer then it is activated       */
/* -------------------------------------------------------------------------- */
export class AuthEmployerGuard implements CanActivate {
  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}
  canActivate(): Observable<boolean> {
    return this.accountService.currentRole$.pipe(
      map((role) => {
        if (role == 'Employer') return true;
        this.toastr.error(
          'You must be logged in as Employer to access this route'
        );
        return false;
      })
    );
  }
}
