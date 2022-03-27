import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root',
})
/* -------------------------------------------------------------------------- */
/*            for protecting routes only which job seeker can access     
 it checks the roles and role is job seeker then it is activated       */
/* -------------------------------------------------------------------------- */
export class AuthJobSeekerGuard implements CanActivate {
  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean>  
     {
    return this.accountService.currentRole$.pipe(
      map((role) => {
        if (role == 'JobSeeker') return true;
        this.toastr.error(
          'You must be logged in as JobSeeker to access this route'
        );
        return false;
      })
    );
  }
}
