import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RxwebValidators } from '@rxweb/reactive-form-validators';
import { ToastrService } from 'ngx-toastr';
import { Role } from '../../_models/role';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
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
    this.registerForm = this.fb.group({
      userName: ['', [Validators.required, RxwebValidators.alpha()]],
      fullName: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]],
      password: ['', [Validators.required, Validators.minLength(5)]],
      phone: ['', [Validators.required, RxwebValidators.digit()]],
      userType: this.role,
    });
  }

  register() {
    // backend takes 0 or 1 to set the role
    this.registerForm.value.userType =
      this.registerForm.value.userType == 'JobSeeker' ? 0 : 1;

    this.accountService.register(this.registerForm.value).subscribe(() => {
      this.router.navigateByUrl('/');
    });
  }
}
