import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Employer } from 'src/app/_models/employer';
import { Pagination } from 'src/app/_models/pagination';
import { Vacancy } from 'src/app/_models/vacancy';
import { EmployerService } from '../../_services/employer.service';

@Component({
  selector: 'app-employer-vacancy-list',
  templateUrl: './employer-vacancy-list.component.html',
  styleUrls: ['./employer-vacancy-list.component.css'],
})
export class EmployerVacancyListComponent implements OnInit {
  employer: Employer;
  id: number;
  vacancies: Vacancy[];

  pagination: Pagination;
  pageNumber = 1;
  pageSize = 5;

  constructor(
    private router: Router,
    private employerService: EmployerService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadVacancies();
  }

  loadVacancies() {
    this.employerService
      .getVacanciesPostedByMe(this.pageNumber, this.pageSize)
      .subscribe((response) => {
        if (response.pagination.totalItems == 0) {
          this.toastr.error('You havent applied to anything');
          this.router.navigateByUrl('/');
        }
        this.vacancies = response.result;
        this.pagination = response.pagination;
      });
  }

  editVacancy(id: number) {
    this.router.navigateByUrl(`/employer-vacancy-edit/${id}`);
  }
  viewVacancyRequest(id: number) {
    this.router.navigateByUrl(`/employer-view-vacancy-reqs/${id}`);
  }
  pageChanged(event: number) {
    this.pageNumber = event;
    this.loadVacancies();
  }
}
