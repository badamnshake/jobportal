import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, max, min } from 'rxjs';
import { environment } from 'src/environments/environment';
import { JobSeeker } from '../_models/job-seeker';
import { PaginatedResult } from '../_models/pagination';
import { ToOrderBy, Vacancy } from '../_models/vacancy';

@Injectable({
  providedIn: 'root',
})
export class VacancyService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<Vacancy[]> = new PaginatedResult();

  constructor(private http: HttpClient) {}

  getVacancyFromId(id: number) {
    return this.http
      .get<Vacancy>(this.baseUrl + `/vacancy/get-details/${id}`)
      .pipe(
        map((response: Vacancy) => {
          return response;
        })
      );
  }
  getVacancies(
    page?: number,
    itemsPerPage?: number,
    location?: string,
    minSalary?: number,
    maxSalary?: number,
    lastDateToApply?: Date,
    publishedDate?: Date,
    orderBy?: ToOrderBy
  ) {
    /// adding query params per passed and requirement
    let queryParams = new HttpParams();
    if (page != null && itemsPerPage != null) {
      // adding pagination
      queryParams = queryParams.append('pageNumber', page.toString());
      queryParams = queryParams.append('pageSize', itemsPerPage.toString());
      if (location != null)
        queryParams = queryParams.append('location', location.trim());
      if (minSalary != null)
        queryParams = queryParams.append('minSalary', minSalary.toString());
      if (maxSalary != null)
        queryParams = queryParams.append('maxSalary', maxSalary.toString());
      if (lastDateToApply != null)
        queryParams = queryParams.append(
          'lastDateToApply',
          lastDateToApply.toString()
        );
      if (publishedDate != null)
        queryParams = queryParams.append(
          'publishedDate',
          publishedDate.toString()
        );
      if (orderBy != null) {
        let x: number = orderBy;
        queryParams = queryParams.append('orderBy', x.toString());
      }
    }
    return this.http
      .get<Vacancy[]>(this.baseUrl + '/vacancy/get-vacancies/', {
        observe: 'response',
        params: queryParams,
      })
      .pipe(
        map((response) => {
          console.log(response.body);

          this.paginatedResult.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            this.paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination')
            );
          }
          return this.paginatedResult;
        })
      );
  }
  createVacancy(model: any) {
    return this.http.post(this.baseUrl + '/vacancy/create-details', model).pipe(
      map((id: number) => {
        if (id) {
        }
        return id;
      })
    );
  }
  updateVacancy(model: any) {
    return this.http.post(this.baseUrl + '/vacancy/update-details', model).pipe(
      map(() => {
        console.log('hello');
      })
    );
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
