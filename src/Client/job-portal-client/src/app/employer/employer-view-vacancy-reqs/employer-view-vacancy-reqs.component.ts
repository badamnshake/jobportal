import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { JobSeeker } from 'src/app/_models/job-seeker';
import { Pagination } from 'src/app/_models/pagination';
import { EmployerService } from 'src/app/_services/employer.service';

@Component({
  selector: 'app-employer-view-vacancy-reqs',
  templateUrl: './employer-view-vacancy-reqs.component.html',
  styleUrls: ['./employer-view-vacancy-reqs.component.css'],
})
export class EmployerViewVacancyReqsComponent implements OnInit {
  isCollapsed = true;
  jobSeekers: JobSeeker[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 3;
  vacancyId: number;
  /// takes vacancy id in the params
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employerService: EmployerService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.vacancyId = parseInt(this.route.snapshot.paramMap.get('id'));
    if (!this.vacancyId) this.router.navigateByUrl('not-found');
    this.loadJobSeekersWhoAppliedOnVacancies();
  }
  loadJobSeekersWhoAppliedOnVacancies() {
    this.employerService
      .getJobSeekersWhoAppliedOn(this.vacancyId, this.pageNumber, this.pageSize)
      .subscribe((response) => {
        if (response.pagination.totalItems == 0) {
          this.toastr.info('No one has Applied for this vacancy yet');
          this.router.navigateByUrl('employer-vacancy-list');
        } else {
          this.jobSeekers = response.result;
          this.pagination = response.pagination;
        }
      });
  }
  pageChanged(event: number) {
    this.pageNumber = event;
    this.loadJobSeekersWhoAppliedOnVacancies();
  }
}
