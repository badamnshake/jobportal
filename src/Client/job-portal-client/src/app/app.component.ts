import { Component, OnInit } from '@angular/core';
import jwtDecode, { JwtPayload } from 'jwt-decode';
import { ToastrService } from 'ngx-toastr';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { JobSeekerService } from './_services/job-seeker.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'job-portal-client';

  constructor(
    private accountService: AccountService,
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    if (this.isTokenExpired()) {
      this.toastr.info('The session has Expired', 'You need to Login Again');
    } else {
      this.setCurrentUser();
    }
  }

  isTokenExpired() {
    let user: User = JSON.parse(localStorage.getItem('user'));
    let decodedJwt: JwtPayload = jwtDecode(user.token);
    return Date.now() >= decodedJwt.exp * 1000;
  }
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    this.accountService.setCurrentUser(user);
    this.accountService.currentRole$.subscribe((role) => {
      if (role == 'JobSeeker') {
        this.jobSeekerService.setVacanciesWhereJSApplied();
      }
    });
  }
}
