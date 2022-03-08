import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../../_models/user';
import { AccountService } from '../../_services/account.service';
import {Role} from "../../_models/role";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  model: any = {};
  loggedIn: boolean = false;

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe((response) => {
      this.router.navigateByUrl('/members');
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
  // employer navs
  btnViewEmployerProfile() {
    this.router.navigateByUrl('/employer-profile');
  }
  btnEditEmployerProfile() {
    this.router.navigateByUrl('/employer-profile-edit');
  }
  btnViewEmployerPostedVacancies() {
    this.router.navigateByUrl('/employer-vacancy-list');
  }
  btnViewEmployerVacancyRequests() {
    this.router.navigateByUrl('/');
  }
  // employer navs
  btnViewJSProfile() {
    this.router.navigateByUrl('/');
  }
  btnEditJSProfile() {
    this.router.navigateByUrl('/');
  }
  btnViewJSAppliedVacReq() {
    this.router.navigateByUrl('/');
  }
}
