import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.jobSeekerService.getMyDetails().subscribe((response) => {
      if (response == null) this.navigateToEditProfile();
      this.jobSeeker = response;
    });
  }
  navigateToEditProfile() {
    this.router.navigateByUrl('js-edit-profile');
  }
  navigateToCreateExp() {
    this.router.navigateByUrl('js-create-experience');
  }
  navigateToCreateQual() {
    this.router.navigateByUrl('js-create-qualification');
  }

}
