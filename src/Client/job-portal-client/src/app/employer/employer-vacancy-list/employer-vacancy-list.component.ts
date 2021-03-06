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

  // employer can see his posted vacancy
  // delete edit and view vac requests from this
  loadVacancies() {
    this.employerService
      .getVacanciesPostedByMe(this.pageNumber, this.pageSize)
      .subscribe((response) => {
        if (response.pagination.totalItems == 0) {
          this.toastr.info('You havent created Vacancy Yet! Let\'s Create One');
          this.router.navigateByUrl('/employer-vacancy-create');
        }
        this.vacancies = response.result;
        this.pagination = response.pagination;
      });
  }

  pageChanged(event: number) {
    this.pageNumber = event;
    this.loadVacancies();
  }
}
