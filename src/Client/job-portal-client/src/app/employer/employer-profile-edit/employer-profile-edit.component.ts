import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Employer } from 'src/app/_models/employer';
import { EmployerService } from 'src/app/_services/employer.service';

@Component({
  selector: 'app-employer-profile-edit',
  templateUrl: './employer-profile-edit.component.html',
  styleUrls: ['./employer-profile-edit.component.css'],
})
export class EmployerProfileEditComponent implements OnInit {
  submitButtonText: string;
  descriptionText: string;
  doesEmpExist = true;

  employerForm: FormGroup;
  employer: Employer;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private employerService: EmployerService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.employerService.getEmployerMe().subscribe((response) => {
      this.employer = response;
      if (!response) this.doesEmpExist = false;
      if (this.employer) this.employerForm.patchValue(this.employer);
    });

    this.setDisplayTexts();
  }

  initializeForm() {
    this.employerForm = this.fb.group({
      organizationName: ['', Validators.required],
      organizationType: ['', Validators.required],
      companyEmail: ['', Validators.email],
      companyPhone: [''],
      noOfEmployees: [''],
      startYear: ['', Validators.required],
      about: ['', Validators.required],
    });
  }
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
  setDisplayTexts() {
    if (!this.doesEmpExist) {
      this.submitButtonText = 'Create Profile';
      this.descriptionText = 'Add Details  & Create Profile';
    } else {
      this.submitButtonText = 'Update Details';
      this.descriptionText = 'Profile Settings';
    }
  }
  updateOrCreateDetails() {
    if (!this.doesEmpExist) {
      this.employerService.createEmployer(this.employerForm.value).subscribe(() => {});
    } else {
      this.employerService.updateEmployer(this.employerForm.value).subscribe({
        next: () => {
          
        },
        error: () => {
          console.log('failed to udpate');
        },
      });
    }
  }
}
