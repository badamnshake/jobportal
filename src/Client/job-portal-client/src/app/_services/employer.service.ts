import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Employer } from '../_models/employer';
import { JobSeeker } from '../_models/job-seeker';
import { Vacancy } from '../_models/vacancy';

@Injectable({
  providedIn: 'root',
})
export class EmployerService {
  // get emp from email
  // get emp from id
  // create details
  // update details
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getEmployerFromId(id: number) {
    return this.http.get<Employer>(this.baseUrl + `/employer/get/${id}`).pipe(
      map((response: Employer) => {
        return response;
      })
    );
  }
  getEmployerMe() {
    return this.http.get<Employer>(this.baseUrl + '/employer/get/').pipe(
      map((response: Employer) => {
        return response;
      })
    );
  }
  createEmployer(model: Employer) {
    return this.http.post(this.baseUrl + '/employer/create', model).pipe(
      map(() => {
        return true;
      })
    );
  }
  updateEmployer(model: Employer) {
    return this.http.post(this.baseUrl + '/employer/update', model).pipe(
      map(() => {
        return true;
      })
    );
  }
  // related to vacancies
  createVacancy(model: any) {
    return this.http.post(this.baseUrl + '/vacancy/create', model).pipe(
      map((vac: Vacancy) => {
        return vac;
      })
    );
  }
  updateVacancy(model: any) {
    return this.http
      .post(this.baseUrl + '/vacancy/update', model)
  }
  getJobSeekersWhoAppliedOn(vacancyId: number) {
    return this.http
      .get<JobSeeker[]>(
        this.baseUrl + `/get-job-seekers-who-applied-on-vacancy/${vacancyId}`
      )
      .pipe(
        map((response) => {
          return response;
        })
      );
  }
}
