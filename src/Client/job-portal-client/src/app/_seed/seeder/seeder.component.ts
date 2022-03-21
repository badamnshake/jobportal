import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { lastValueFrom } from 'rxjs';
import { Employer } from 'src/app/_models/employer';
import { JobSeeker } from 'src/app/_models/job-seeker';
import { AccountService } from 'src/app/_services/account.service';
import { EmployerService } from 'src/app/_services/employer.service';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';
import { VacancyService } from 'src/app/_services/vacancy.service';
import employerData from '../Data/empppy.json';
import jsData from '../Data/jsData.json';

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
    // await this.addJSAndDetails();
    //await addEmployerAndDetails()
  }

  async addJSAndDetails() {
    this.as.logout();
    for (let i = 82; i < jsData.length; i++) {
      const obj = jsData[i];
      
      await this.registerUser(obj);
      await this.registerJsDetails(obj.JobSeeker);
      this.toastrService.success(`Added JobSeeker number ${i}`);
    }
    this.as.logout();
  }
  async registerJsDetails(js: JobSeeker) {
    await lastValueFrom(this.js.createJobSeeker(js));
    // add q's
    for (let i = 0; i < js.qualifications.length; i++)
      await lastValueFrom(this.js.createQualification(js.qualifications[i]));
    // add e's
    for (let i = 0; i < js.experiences.length; i++)
      await lastValueFrom(this.js.createExperience(js.experiences[i]));
    //
    // add vacancy requests
    for (let i = 0; i < js.vacancyRequests.length; i++)
      await lastValueFrom(this.js.createVacancyRequest(js.vacancyRequests[i]));
  }
  async addEmployerAndDetails() {
    this.as.logout();
    for (let i = 1; i < employerData.length; i++) {
      const obj = employerData[i];
      await this.registerUser(obj);
      await this.registerEmployerDetails(obj);
      this.toastrService.success(`Added Employer number ${i}`);
    }
    this.as.logout();
  }
  async registerUser(obj: any) {
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
    // add vacancies
    for (let i = 0; i < obj.Employer.vacancies.length; i++) {
      const element = obj.Employer.vacancies[i];
      await lastValueFrom(this.es.createVacancy(element));
    }
  }
}
