import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Employer } from '../_models/employer';
import { JobSeeker } from '../_models/job-seeker';
import { PaginatedResult } from '../_models/pagination';
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
  paginatedResultJSOnAVacancy: PaginatedResult<JobSeeker[]> =
    new PaginatedResult();
  paginatedResultEmployerPostedVacancy: PaginatedResult<Vacancy[]> =
    new PaginatedResult();

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
  getVacanciesPostedByMe(page?: number, itemsPerPage?: number) {
    let queryParams = new HttpParams();
    if (page != null && itemsPerPage != null) {
      queryParams = queryParams.append('pageNumber', page.toString());
      queryParams = queryParams.append('pageSize', itemsPerPage.toString());
    }
    return this.http
      .get<Vacancy[]>(this.baseUrl + '/vacancy/get-vacancies-posted-by-me', {
        observe: 'response',
        params: queryParams,
      })
      .pipe(
        map((response) => {
          this.paginatedResultEmployerPostedVacancy.result = response.body;

          if (response.headers.get('Pagination') !== null) {
            this.paginatedResultEmployerPostedVacancy.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return this.paginatedResultEmployerPostedVacancy;
        })
      );
  }
  createEmployer(model: any) {
    return this.http.post(this.baseUrl + '/employer/create', model).pipe(
      map(() => {
        return true;
      })
    );
  }
  updateEmployer(model: Employer) {
    return this.http.put(this.baseUrl + '/employer/update', model).pipe(
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
  updateVacancy(model: Vacancy) {
    return this.http.put(this.baseUrl + '/vacancy/update', model);
  }
  deleteVacancy(id: number) {
    return this.http.delete(this.baseUrl + '/vacancy/delete/' + id);
  }
  getJobSeekersWhoAppliedOn(
    vacancyId: number,
    page?: number,
    itemsPerPage?: number
  ) {
    let queryParams = new HttpParams();
    if (page != null && itemsPerPage != null) {
      queryParams = queryParams.append('pageNumber', page.toString());
      queryParams = queryParams.append('pageSize', itemsPerPage.toString());
    }
    return this.http
      .get<JobSeeker[]>(
        this.baseUrl + `/get-job-seekers-who-applied-on-vacancy/${vacancyId}`,
        {
          observe: 'response',
          params: queryParams,
        }
      )
      .pipe(
        map((response) => {
          this.paginatedResultJSOnAVacancy.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            this.paginatedResultJSOnAVacancy.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return this.paginatedResultJSOnAVacancy;
        })
      );
  }
}
