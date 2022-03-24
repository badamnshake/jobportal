import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  NgbDate,
  NgbDateParserFormatter,
  NgbDateStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { JobSeeker } from 'src/app/_models/job-seeker';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';
import { factor, RxwebValidators } from '@rxweb/reactive-form-validators';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-js-edit-profile',
  templateUrl: './js-edit-profile.component.html',
  styleUrls: ['./js-edit-profile.component.css'],
})
export class JsEditProfileComponent implements OnInit {
  faCalendar = faCalendar;
  submitButtonText: string;
  descriptionText: string;
  doesJsExist = true;

  // dateOfBirth: string;
  dateOfBirth: string;

  jsForm: FormGroup;
  js: JobSeeker;
  constructor(
    private router: Router,
    private fb: FormBuilder,
    private jobSeekerService: JobSeekerService,
    private ngbDateParserFormatter: NgbDateParserFormatter,
    private toastr: ToastrService
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
      firstName: ['', [RxwebValidators.alpha(), Validators.required]],
      lastName: ['', [RxwebValidators.alpha(), Validators.required]],
      email: ['', Validators.email],
      phone: ['', RxwebValidators.digit()],
      address: ['', Validators.required],
      totalExperience: [0, RxwebValidators.digit()],
      expectedSalaryAnnual: [0, RxwebValidators.digit()],
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
      dateOfBirth: this.ngbDateParserFormatter.parse(
        this.js.dateOfBirth.toString()
      ),
    });
    this.dateOfBirth = new Date(
      this.ngbDateParserFormatter.format(this.jsForm.value.dateOfBirth)
    ).toISOString();
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
    // this.jsForm.value.dateOfBirth = this.dateOfBirth;
    this.jsForm.value.dateOfBirth = this.dateOfBirth;
    if (!this.doesJsExist) {
      this.jobSeekerService.createJobSeeker(this.jsForm.value).subscribe(() => {
        this.router.navigateByUrl('/js-profile');
        this.toastr.success('Profile Created');
      });
    } else {
      this.jobSeekerService.updateJobSeeker(this.jsForm.value).subscribe(() => {
        this.router.navigateByUrl('/js-profile');
        this.toastr.success('Profile Updated');
      });
    }
  }
  isDOBValid() {
    return this.dateOfBirth != null;
  }
}
