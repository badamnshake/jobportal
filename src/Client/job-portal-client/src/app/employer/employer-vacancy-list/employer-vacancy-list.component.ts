import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Employer } from 'src/app/_models/employer';
import { Vacancy } from 'src/app/_models/vacancy';
import { BusyService } from 'src/app/_services/busy.service';
import { EmployerService } from '../../_services/employer.service';

@Component({
  selector: 'app-employer-vacancy-list',
  templateUrl: './employer-vacancy-list.component.html',
  styleUrls: ['./employer-vacancy-list.component.css'],
})
export class EmployerVacancyListComponent implements OnInit {
  employer: Employer;
  id: number;
  vacancyList: Vacancy[];
  ownsVacancy = false;
  loaded = false;

  constructor(
    private route: ActivatedRoute,
    private employerService: EmployerService,
    private busyService: BusyService
  ) {}

  ngOnInit(): void {
    this.busyService.busy();
    this.fetchVacancyListAfterLoggedIn();
    this.busyService.idle();
    this.loaded = true;
  }

  DateToString(date: Date) {
    return formatDate(date, 'short', 'gmt');
  }

  fetchVacancyListAfterLoggedIn() {
    this.employerService.getEmployerMe().subscribe((response) => {
      if (response == null) {
        console.log('need to create some vacancies');
      } else {
        this.vacancyList = response.vacancies;
      }
    });
  }
}
