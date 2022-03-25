import { Component, OnInit, ViewChild } from '@angular/core';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
  NgbModal,
  NgbTypeahead,
} from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Pagination } from 'src/app/_models/pagination';
import { ToOrderBy, Vacancy } from 'src/app/_models/vacancy';
import { VacancyRequest } from 'src/app/_models/vacancy-request';
import { AccountService } from 'src/app/_services/account.service';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';
import { VacancyService } from 'src/app/_services/vacancy.service';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { EmployerProfileComponent } from 'src/app/employer/employer-profile/employer-profile.component';
import {
  debounceTime,
  distinctUntilChanged,
  filter,
  map,
  merge,
  Observable,
  OperatorFunction,
  Subject,
} from 'rxjs';

@Component({
  selector: 'app-vacancy-list',
  templateUrl: './vacancy-list.component.html',
  styleUrls: ['./vacancy-list.component.css'],
})
export class VacancyListComponent implements OnInit {
  vacanciesApplied: number[];
  faCalendar = faCalendar;
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
    public accountService: AccountService,
    private router: Router,
    private vacancyService: VacancyService,
    private jobSeekerService: JobSeekerService,
    private ngbDateParserFormatter: NgbDateParserFormatter,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loadVacanciesWhereJSApplied();
    this.resetFilters();
    this.loadVacancies();
  }
  @ViewChild('instance', { static: true }) instance: NgbTypeahead;
  focus$ = new Subject<string>();
  click$ = new Subject<string>();

  search: OperatorFunction<string, readonly string[]> = (
    text$: Observable<string>
  ) => {
    const debouncedText$ = text$.pipe(
      debounceTime(200),
      distinctUntilChanged()
    );
    const clicksWithClosedPopup$ = this.click$.pipe(
      filter(() => !this.instance.isPopupOpen())
    );
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, inputFocus$, clicksWithClosedPopup$).pipe(
      map((term) =>
        (term === ''
          ? states
          : states.filter(
              (v) => v.toLowerCase().indexOf(term.toLowerCase()) > -1
            )
        ).slice(0, 10)
      )
    );
  };

  loadVacanciesWhereJSApplied() {
    if (
      this.accountService.currentRole$.subscribe((role) => role == 'JobSeeker')
    ) {
      this.vacanciesApplied = JSON.parse(
        localStorage.getItem('vacanciesApplied')
      );
      if (this.vacanciesApplied == null) this.vacanciesApplied = [];
    } else this.vacanciesApplied = null;
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

  async applyForVacancy(id: number) {
    let vr: VacancyRequest = {
      vacancyId: id,
    };
    this.jobSeekerService.createVacancyRequest(vr).subscribe(() => {
      this.vacanciesApplied.push(id);
      localStorage.setItem(
        'vacanciesApplied',
        JSON.stringify(this.vacanciesApplied)
      );
      this.toastr.success('Applied For Vacancy');
    });
  }
  getEmployerDetails(id: number) {
    this.router.navigateByUrl(`employer-profile/${id}`);
  }

  pageChanged(event: number) {
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
    this.lastDate = null;
    this.publishedDate = null;
  }
  // this is used to give input in select element in html so user can easily select which thing to order by
  toOrderBy: { name: string; value: ToOrderBy }[] = [
    {
      name: `Min salary Ascending`,
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

const states = [
  'New York',
  'Ahmedabad',
  'Ohio',
  'Mumbai',
  'Bangalore',
  'California',
  'Delaware',
  'Michigan',
  'Pune',
  'Alabama',
  'Alaska',
  'American Samoa',
  'Arizona',
  'Arkansas',
  'Colorado',
  'Connecticut',
  'District Of Columbia',
  'Federated States Of Micronesia',
  'Florida',
  'Georgia',
  'Guam',
  'Hawaii',
  'Idaho',
  'Illinois',
  'Indiana',
  'Iowa',
  'Kansas',
  'Kentucky',
  'Louisiana',
  'Maine',
  'Marshall Islands',
  'Maryland',
  'Massachusetts',
  'Minnesota',
  'Mississippi',
  'Missouri',
  'Montana',
  'Nebraska',
  'Nevada',
  'New Hampshire',
  'New Jersey',
  'New Mexico',
  'North Carolina',
  'North Dakota',
  'Northern Mariana Islands',
  'Oklahoma',
  'Oregon',
  'Palau',
  'Pennsylvania',
  'Puerto Rico',
  'Rhode Island',
  'South Carolina',
  'South Dakota',
  'Tennessee',
  'Texas',
  'Utah',
  'Vermont',
  'Virgin Islands',
  'Virginia',
  'Washington',
  'West Virginia',
  'Wisconsin',
  'Wyoming',
];
