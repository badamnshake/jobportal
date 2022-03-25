import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';
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
  vacancies: Vacancy[] = [];
  vacanciesIdApplied: number[];

  pagination: Pagination;
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private vacancyService: VacancyService,
    private jobSeekerService: JobSeekerService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadVacancies();
  }

  navigateToVacancyList() {
    this.router.navigateByUrl('/vacancy-list', {
      state: {
        userExists: false,
      },
    });
  }

  loadVacancies() {
    let vacancy: Vacancy;
    this.vacancies = [];

    this.jobSeekerService
      .getVacanciesWhereIApplied(this.pageNumber, this.pageSize)
      .subscribe((response) => {
        if (response.pagination.totalItems == 0) {
          this.toastr.info(
            'Please Apply to Vacancies First',
            'You havent applied to Any Vacancy Yet !!'
          );
          this.navigateToVacancyList();
        }
        this.vacanciesIdApplied = response.result;
        this.pagination = response.pagination;
        this.vacanciesIdApplied.forEach((element) => {
          this.vacancyService.getVacancyFromId(element).subscribe({
            next: (response) => {
              vacancy = response;
              this.vacancies.push(vacancy);
            },
            error: (error) => {
              if (error.status == 400) {
                console.log('hello');

                this.jobSeekerService
                  .deleteVacancyRequest(element)
                  .subscribe(() => {});
              }
            },
          });
        });
      });
  }

  pageChanged(event: number) {
    this.pageNumber = event;
    this.loadVacancies();
  }
}
