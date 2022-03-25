import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Role } from '../../_models/role';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  role: Role;

  constructor(
    private accountService: AccountService,
    private fb: FormBuilder,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.loginForm = this.fb.group({
      email: ['', Validators.email],
      password: ['', [Validators.required, Validators.minLength(5)]],
    });
  }

  login() {
    this.accountService.login(this.loginForm.value).subscribe(() => {
      this.router.navigateByUrl('/');
      this.toastr.success('Logged In Successfully');
    });
  }
}
