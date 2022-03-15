import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { JobSeeker } from 'src/app/_models/job-seeker';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';

@Component({
  selector: 'app-js-profile',
  templateUrl: './js-profile.component.html',
  styleUrls: ['./js-profile.component.css'],
})
export class JsProfileComponent implements OnInit {
  jobSeeker: JobSeeker;

  constructor(
    private router: Router,
    private jobSeekerService: JobSeekerService
  ) {}

  ngOnInit(): void {
    this.jobSeekerService.getMyDetails().subscribe((response) => {
      if (response == null) this.navigateToCreateProfile();
      this.jobSeeker = response;
    });
  }
  navigateToCreateProfile() {
    this.router.navigateByUrl('js-edit-profile', {
      state: {
        userExists: false,
      },
    });
  }
}
