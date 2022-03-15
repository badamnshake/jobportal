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
  loaded = false;

  employerForm: FormGroup;
  employer: Employer;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private employerService: EmployerService
  ) {}

  ngOnInit(): void {
    this.employerService.getEmployerMe().subscribe((response) => {
      if (!response) this.doesEmpExist = false;
      else this.employer = response;
    });

    this.initializeForm();

    if (this.employer) this.employerForm.patchValue(this.employer);
    this.setDisplayTexts();
    this.loaded = true;
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
      this.employerService.createEmployer(this.employer).subscribe(() => {});
    } else {
      this.employerService.updateEmployer(this.employer).subscribe({
        next: () => {},
        error: () => {
          console.log('failed to udpate');
        },
      });
    }
  }
}
