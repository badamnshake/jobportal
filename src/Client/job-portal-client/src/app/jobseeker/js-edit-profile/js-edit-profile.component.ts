import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { Employer } from 'src/app/_models/employer';
import { JobSeeker } from 'src/app/_models/job-seeker';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';

@Component({
  selector: 'app-js-edit-profile',
  templateUrl: './js-edit-profile.component.html',
  styleUrls: ['./js-edit-profile.component.css'],
})
export class JsEditProfileComponent implements OnInit {
  submitButtonText: string;
  descriptionText: string;
  doesJsExist = true;

  dateOfBirth: string;

  jsForm: FormGroup;
  js: JobSeeker;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private jobSeekerService: JobSeekerService,
    private ngbDateParserFormatter: NgbDateParserFormatter
  ) {}

  ngOnInit(): void {
    this.initializeForm();
    this.jobSeekerService.getMyDetails().subscribe((response) => {
      if (!response) this.doesJsExist = false;
      else this.js = response;
      if (this.js) this.patchValuesIntoForm();
    });

    this.setDisplayTexts();
  }

  initializeForm() {
    this.jsForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      phone: ['', Validators.required],
      address: ['', Validators.required],
      totalExperience: [''],
      expectedSalaryAnnual: [''],
      dateOfBirth: [''],
    });
  }
  patchValuesIntoForm() {
    this.jsForm.patchValue({
      firstName: this.js.firstName,
      lastName: this.js.lastName,
      email: this.js.email,
      phone: this.js.phone,
      address: this.js.address,
      totalExperience: this.js.totalExperience,
      expectedSalaryAnnual: this.js.expectedSalaryAnnual,
      dateOfBirth: this.js.dateOfBirth,
    });
  }
  setDisplayTexts() {
    if (!this.doesJsExist) {
      this.submitButtonText = 'Create Profile';
      this.descriptionText = 'Add Details  & Create Profile';
    } else {
      this.submitButtonText = 'Update Details';
      this.descriptionText = 'Profile Settings';
    }
  }
  onDateSelect(event: NgbDateStruct) {
    this.dateOfBirth = new Date(
      this.ngbDateParserFormatter.format(event)
    ).toISOString();
  }
  updateOrCreateDetails() {
    this.jsForm.value.dateOfBirth = this.dateOfBirth;

    if (!this.doesJsExist) {
      this.jobSeekerService
        .createJobSeeker(this.jsForm.value)
        .subscribe(() => {});
    } else {
      this.jobSeekerService.updateJobSeeker(this.jsForm.value).subscribe({
        next: () => {},
        error: () => {
          console.log('failed to udpate');
        },
      });
    }
  }
}
