import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css'],
})
export class ChangePasswordComponent implements OnInit {
  changePasswordForm: FormGroup;

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
    this.changePasswordForm = this.fb.group({
      newPassword: ['', [Validators.minLength(5), Validators.required]],
    });
  }

  changePassword() {
    this.accountService.changePassword(this.changePasswordForm.value).subscribe({
      next: () => {
        this.router.navigateByUrl('/');
        this.toastr.success('Changed Password Successfully');
      },
      error: (e) => {
        this.toastr.error(e.error);
      },
    });
  }
}
