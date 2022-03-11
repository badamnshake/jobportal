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
      this.fetchVacancyListAfterLoggedIn(this.employerService);
    this.busyService.idle();
    this.loaded = true;
    
  }

  DateToString( date:Date) {
    return formatDate(date, "short", "gmt");
  }

  fetchVacancyListAfterLoggedIn(employerService: EmployerService) {
    let empId: number = null;
    this.employerService.currentEmpId$.subscribe((id) => {
      empId = id;
    });
    if (empId != null) {
      this.employerService.getEmployerFromId(empId).subscribe((response) => {
        this.vacancyList =  response.vacancies;
      });
    } else {
      this.employerService
        .getEmployerFromEmail(JSON.parse(localStorage.getItem('user')).email)
        .subscribe((response) => {
          if (response == null) {
            console.log('need to create some vacancies');
          } else {
            this.vacancyList = response.vacancies;
          }
        });
    }
    
  }
}
