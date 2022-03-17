import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Pagination } from 'src/app/_models/pagination';
import { Vacancy } from 'src/app/_models/vacancy';
import { VacancyRequest } from 'src/app/_models/vacancy-request';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';
import { VacancyService } from 'src/app/_services/vacancy.service';

@Component({
  selector: 'app-js-applied-vacancies',
  templateUrl: './js-applied-vacancies.component.html',
  styleUrls: ['./js-applied-vacancies.component.css'],
})
export class JsAppliedVacanciesComponent implements OnInit {
  vacancyRequests: VacancyRequest[];
  vacanciesIdApplied: number[];
  vacancies: Vacancy[] = [];

  pageNumber = 1;
  pageSize = 5;
  totalItems: number;
  totalPages: number;

  constructor(
    private vacancyService: VacancyService,
    private jobSeekerService: JobSeekerService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    // get vacancy requests fetch
    this.jobSeekerService.getVacanciesWhereIApplied().subscribe((response) => {
      if (response.length == 0) this.navigateToCreateProfile();
      this.vacanciesIdApplied = response;
      this.totalItems = this.vacanciesIdApplied.length;
      this.totalPages = Math.ceil(this.totalItems / this.pageSize);
      this.loadVacancies();
    });
    // set pagination
    // load items
  }

  navigateToCreateProfile() {
    this.router.navigateByUrl('js-edit-profile', {
      state: {
        userExists: false,
      },
    });
  }

  loadVacancies() {
    let vacancy: Vacancy;
    this.vacancies = [];
    let vacId: number;
    let vacReqs = this.vacanciesIdApplied;

    for (
      let i = this.pageNumber - 1;
      i < vacReqs.length && i < this.pageNumber + 5;
      i++
    ) {

      vacId = vacReqs[i];
      this.vacancyService.getVacancyFromId(vacId).subscribe((response) => {
        vacancy = response;
        this.vacancies.push(vacancy);
      });
    }
  }

  deleteVacancyRequest(vacId: number) {
    this.jobSeekerService.deleteVacancyRequest(vacId).subscribe(() => {
      this.toastr.success('Vacancy Request Deleted');
      window.location.reload();
    });
  }

  pageChanged(event: number) {
    this.pageNumber = event;
    this.loadVacancies();
  }
}
