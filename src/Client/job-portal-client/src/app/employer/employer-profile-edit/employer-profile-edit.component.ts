import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RxwebValidators } from '@rxweb/reactive-form-validators';
import { ToastrService } from 'ngx-toastr';
import { Employer } from 'src/app/_models/employer';
import { EmployerService } from 'src/app/_services/employer.service';

@Component({
  selector: 'app-employer-profile-edit',
  templateUrl: './employer-profile-edit.component.html',
  styleUrls: ['./employer-profile-edit.component.css'],
})
export class EmployerProfileEditComponent implements OnInit {
  employerForm: FormGroup;
  employer: Employer;
  doesProfileExist = false;
  constructor(
    private router: Router,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private employerService: EmployerService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.employerService.getEmployerMe().subscribe((response) => {
      this.employer = response;
      if (response) {
        this.doesProfileExist = true;
        this.employerForm.patchValue(this.employer);
      }
    });
  }

  initializeForm() {
    this.employerForm = this.fb.group({
      organizationName: ['', Validators.required],
      organizationType: ['', Validators.required],
      companyEmail: ['', [Validators.email, Validators.required]],
      companyPhone: ['', [RxwebValidators.digit(), Validators.required]],
      noOfEmployees: ['', [RxwebValidators.digit(), Validators.required]],
      startYear: ['', [RxwebValidators.digit(), Validators.required]],
      about: ['', Validators.required],
    });
  }
  // it patches employer details into the form 
  patchValuesIntoForm() {
    this.employerForm.patchValue({
      organizationName: this.employer.organizationName,
      organizationType: this.employer.organizationType,
      companyEmail: this.employer.companyEmail,
      companyPhone: this.employer.companyPhone,
      noOfEmployees: this.employer.noOfEmployees,
      startYear: this.employer.startYear,
      about: this.employer.about,
    });
  }
  updateDetails() {
    if (!this.doesProfileExist) {
      this.employerService
        .createEmployer(this.employerForm.value)
        .subscribe(() => {
          this.router.navigateByUrl('/').then(() => {
            this.toastr.success('Profile Created');
          });
        });
    } else {
      this.employerService
        .updateEmployer(this.employerForm.value)
        .subscribe(() => {
          this.router.navigateByUrl('/').then(() => {
            this.toastr.success('Profile Updated');
          });
        });
    }
  }
}
