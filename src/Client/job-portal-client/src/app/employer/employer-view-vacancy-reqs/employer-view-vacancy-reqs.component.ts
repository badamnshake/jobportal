import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { JobSeeker } from 'src/app/_models/job-seeker';
import { EmployerService } from 'src/app/_services/employer.service';

@Component({
  selector: 'app-employer-view-vacancy-reqs',
  templateUrl: './employer-view-vacancy-reqs.component.html',
  styleUrls: ['./employer-view-vacancy-reqs.component.css'],
})
export class EmployerViewVacancyReqsComponent implements OnInit {
  jobSeekers: JobSeeker[];
  /// takes vacancy id in the params
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employerService: EmployerService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get('id'));
    if (!id) this.router.navigateByUrl('not-found');
    this.employerService.getJobSeekersWhoAppliedOn(id).subscribe((response) => {
      if (response.length == 0) {
        this.toastr.info('No one has Applied for this vacancy yet');
        this.router.navigateByUrl('employer-vacancy-list');
      } else {
        this.jobSeekers = response;
      }
    });
  }
}
