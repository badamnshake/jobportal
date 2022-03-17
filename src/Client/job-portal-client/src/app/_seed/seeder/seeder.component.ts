import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { lastValueFrom } from 'rxjs';
import { Employer } from 'src/app/_models/employer';
import { AccountService } from 'src/app/_services/account.service';
import { EmployerService } from 'src/app/_services/employer.service';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';
import { VacancyService } from 'src/app/_services/vacancy.service';
import employerData from '../Data/empppy.json';

@Component({
  selector: 'app-seeder',
  templateUrl: './seeder.component.html',
  styleUrls: ['./seeder.component.css'],
})
export class SeederComponent implements OnInit {
  constructor(
    private as: AccountService,
    private es: EmployerService,
    private vs: VacancyService,
    private js: JobSeekerService,
    private toastrService: ToastrService
  ) {}

  async ngOnInit(): Promise<void> {
    this.as.logout();

    for (let i = 1; i < employerData.length; i++) {
      const obj = employerData[i];
      await this.registerUser(obj);
      await this.registerEmployerDetails(obj);
      await this.addVacancies(obj);
      this.toastrService.success(`Added Employer number ${i}`);
    }
    this.as.logout();
  }
  async registerUser(obj: typeof employerData[0]) {
    let model = {
      userName: obj.userName,
      fullName: obj.fullName,
      email: obj.email,
      password: obj.password,
      phone: obj.phone,
      userType: obj.userType,
    };
    await lastValueFrom(this.as.register(model));
  }
  async registerEmployerDetails(obj: typeof employerData[0]) {
    await lastValueFrom(this.es.createEmployer(obj.Employer));
  }
  async addVacancies(obj: typeof employerData[0]) {
    for (let i = 0; i < obj.Employer.vacancies.length; i++) {
      const element = obj.Employer.vacancies[i];
      await lastValueFrom(this.es.createVacancy(element));
    }
  }
}
