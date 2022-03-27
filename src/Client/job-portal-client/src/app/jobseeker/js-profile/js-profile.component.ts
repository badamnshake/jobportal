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
      if (response == null) this.navigateToCreateProfile();
      this.jobSeeker = response;
    });
  }
  navigateToCreateProfile() {
    this.router.navigateByUrl('js-edit-profile');
  }
  navigateToCreateExp() {
    this.router.navigateByUrl('js-create-experience');
  }
  navigateToCreateQual() {
    this.router.navigateByUrl('js-create-qualification');
  }

  // delete or create exp
  deleteExp(id: number) {
    if (confirm('Are you sure you want to delete this Experience')) {
      this.jobSeekerService.deleteExperience(id).subscribe(() => {
        this.toastr.success('Experince Deleted');
        window.location.reload();
      });
    }
  }
  // delete qualification
  deleteQual(id: number) {
    if (confirm('Are you sure you want to delete this Qualification')) {
      this.jobSeekerService.deleteQualification(id).subscribe(() => {
        this.toastr.success('Qualification Deleted');
        window.location.reload();
      });
    }
  }
}
