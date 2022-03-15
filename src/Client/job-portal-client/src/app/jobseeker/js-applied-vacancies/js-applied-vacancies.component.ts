import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  vacancies: Vacancy[];

  pageNumber = 1;
  pageSize = 5;
  totalItems: number;
  totalPages: number;

  constructor(
    private vacancyService: VacancyService,
    private jobSeekerService: JobSeekerService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // get vacancy requests fetch
    this.jobSeekerService.getMyDetails().subscribe((response) => {
      if (response == null) this.navigateToCreateProfile();
      this.vacancyRequests = response.vacancyRequests;
    });
    // set pagination
    this.totalItems = this.vacancyRequests.length;
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);
    // load items
    this.loadVacancies();
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
    this.vacancies = null;
    let vacId: number;
    let vacReqs = this.vacancyRequests;
    for (
      let i = this.pageNumber - 1;
      i < vacReqs.length && i < this.pageNumber + 5;
      i++
    ) {
      vacId = vacReqs[i].vacancyId;
      this.vacancyService.getVacancyFromId(vacId).subscribe((response) => {
        vacancy = response;
      });
      this.vacancies.push(vacancy);
    }
  }

  pageChanged(event: number) {
    this.pageNumber = event;
    this.loadVacancies();
  }
}
