import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  doesJSExist = true;
  loaded = false;
  // --------------------------------
  jobSeeker: JobSeeker;

  constructor(
    private router: Router,
    private jobSeekerService: JobSeekerService
  ) {}

  ngOnInit(): void {
    this.jobSeekerService.getMyDetails().subscribe((response) => {
      this.checkForJSAndSetValues(response);
    });
    this.setDisplayTexts();
    this.loaded = true;
  }

  checkForJSAndSetValues(response: JobSeeker) {
    this.jobSeeker = response;
    if (!this.jobSeeker) this.doesJSExist = false;
  }

  setDisplayTexts() {
    if (!this.doesJSExist) {
      this.submitButtonText = 'Create Profile';
      this.descriptionText = 'Add Details  & Create Profile';
    } else {
      this.submitButtonText = 'Update Details';
      this.descriptionText = 'Profile Settings';
    }
  }
  updateOrCreateDetails() {
    if (!this.doesJSExist) {
      this.jobSeekerService.createJobSeeker(this.jobSeeker).subscribe(() => {});
    } else {
      this.jobSeekerService.updateJobSeeker(this.jobSeeker).subscribe({
        next: () => {},
        error: () => {
          console.log('failed to udpate');
        },
      });
    }
  }
}
