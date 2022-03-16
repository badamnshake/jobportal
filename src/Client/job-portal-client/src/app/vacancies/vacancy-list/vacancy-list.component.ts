import { Component, OnInit } from '@angular/core';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { Pagination } from 'src/app/_models/pagination';
import { ToOrderBy, Vacancy } from 'src/app/_models/vacancy';
import { VacancyService } from 'src/app/_services/vacancy.service';

@Component({
  selector: 'app-vacancy-list',
  templateUrl: './vacancy-list.component.html',
  styleUrls: ['./vacancy-list.component.css'],
})
export class VacancyListComponent implements OnInit {
  vacancies: Vacancy[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 5;
  lastDate: Date;
  publishedDate: Date;
  filters: getVacancyFilters = {
    location: null,
    minSalary: null,
    maxSalary: null,
    lastDateToApply: null,
    publishedDate: null,
    orderBy: null,
  };

  constructor(
    private vacancyService: VacancyService,
    private ngbDateParserFormatter: NgbDateParserFormatter
  ) {}

  ngOnInit(): void {
    this.resetFilters();
    this.loadVacancies();
  }

  loadVacancies() {
    this.filters.lastDateToApply = this.lastDate;
    this.filters.publishedDate = this.publishedDate;
    this.vacancyService
      .getVacancies(
        this.pageNumber,
        this.pageSize,
        this.filters.location,
        this.filters.minSalary,
        this.filters.maxSalary,
        this.filters.lastDateToApply,
        this.filters.publishedDate,
        this.filters.orderBy
      )
      .subscribe((response) => {
        this.vacancies = response.result;

        this.pagination = response.pagination;
      });
  }

  pageChanged(event: any) {
    this.pageNumber = event;
    this.loadVacancies();
  }

  onLastDateSelect(event: NgbDateStruct) {
    this.lastDate = new Date(this.ngbDateParserFormatter.format(event));
  }
  onPublishedDateSelect(event: NgbDateStruct) {
    this.publishedDate = new Date(this.ngbDateParserFormatter.format(event));
  }

  resetFilters() {
    this.filters.location = null;
    this.filters.minSalary = null;
    this.filters.maxSalary = null;
    this.filters.lastDateToApply = null;
    this.filters.publishedDate = null;
    this.filters.orderBy = null;
  }
  // this is used to give input in select element in html so user can easily select which thing to order by
  toOrderBy: { name: string; value: ToOrderBy }[] = [
    {
      name: 'Min salary Ascending',
      value: ToOrderBy.MinSalaryAscending,
    },
    {
      name: 'Max salary Descending',
      value: ToOrderBy.MaxSalaryDescending,
    },
    {
      name: 'Min salary Descending',
      value: ToOrderBy.MinSalaryDescending,
    },
    {
      name: 'Published Date',
      value: ToOrderBy.PublishedDate,
    },
    {
      name: 'Last Date To Apply',
      value: ToOrderBy.LastDateToApply,
    },
  ];
}

interface getVacancyFilters {
  location?: string;
  minSalary?: number;
  maxSalary?: number;
  lastDateToApply?: Date;
  publishedDate?: Date;
  orderBy?: ToOrderBy;
}
