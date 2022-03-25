import {
  HttpBackend,
  HttpClient,
  HttpHeaders,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Employer } from '../_models/employer';
import { JobSeeker } from '../_models/job-seeker';
import { PaginatedResult } from '../_models/pagination';
import { Vacancy } from '../_models/vacancy';
import { AccountService } from './account.service';

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
  private httpBackend: HttpClient;

  constructor(
    private accountService: AccountService,
    private http: HttpClient,
    private handler: HttpBackend
  ) {
    this.httpBackend = new HttpClient(handler);
  }

  getEmployerFromId(id: number) {
    return this.http.get<Employer>(this.baseUrl + `/employer/get/${id}`).pipe(
      map((response: Employer) => {
        return response;
      })
    );
  }
  getEmployerMe() {
    let token: string;
    this.accountService.currentUser$.subscribe((user) => {
      token = user.token;
    });

    return this.httpBackend
      .get<Employer>(this.baseUrl + '/employer/get', {
        headers: new HttpHeaders({
          Authorization: `Bearer ${token}`,
        }),
      })
      .pipe(
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
