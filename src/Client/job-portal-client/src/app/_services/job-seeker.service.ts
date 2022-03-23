import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Experience } from '../_models/experience';
import { JobSeeker } from '../_models/job-seeker';
import { PaginatedResult } from '../_models/pagination';
import { Qualification } from '../_models/qualification';
import { VacancyRequest } from '../_models/vacancy-request';

@Injectable({
  providedIn: 'root',
})
export class JobSeekerService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<number[]> = new PaginatedResult();

  constructor(private http: HttpClient) {}

  getMyDetails() {
    return this.http.get<JobSeeker>(this.baseUrl + '/job-seeker/get/').pipe(
      map((response: JobSeeker) => {
        return response;
      })
    );
  }
  getVacanciesWhereIApplied(page?: number, itemsPerPage?: number) {
    let queryParams = new HttpParams();
    if (page != null && itemsPerPage != null) {
      queryParams = queryParams.append('pageNumber', page.toString());
      queryParams = queryParams.append('pageSize', itemsPerPage.toString());
    }
    return this.http
      .get<number[]>(
        this.baseUrl + '/vacancy-request/get-vacancies-where-i-applied',
        {
          observe: 'response',
          params: queryParams,
        }
      )
      .pipe(
        map((response) => {
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
  setVacanciesWhereJSApplied() {
    this.getVacanciesWhereIApplied().subscribe((response) => {
      localStorage.setItem('vacanciesApplied', JSON.stringify(response.result));
    });
  }
  createJobSeeker(model: JobSeeker) {
    return this.http.post(this.baseUrl + '/job-seeker/create', model).pipe(
      map((id: number) => {
        return id;
      })
    );
  }
  updateJobSeeker(model: JobSeeker) {
    return this.http
      .put(this.baseUrl + '/job-seeker/update', model)
      .pipe(map(() => {}));
  }

  createExperience(model: Experience) {
    return this.http.post(this.baseUrl + '/experience/create', model).pipe(
      map(() => {
        return model;
      })
    );
  }
  deleteExperience(id: number) {
    return this.http.delete(this.baseUrl + `/experience/delete/${id}`).pipe(
      map(() => {
        return 0;
      })
    );
  }
  createQualification(model: Qualification) {
    return this.http.post(this.baseUrl + '/qualification/create', model).pipe(
      map(() => {
        return model;
      })
    );
  }
  deleteQualification(id: number) {
    return this.http.delete(this.baseUrl + `/qualification/delete/${id}`).pipe(
      map(() => {
        return 0;
      })
    );
  }
  createVacancyRequest(model: VacancyRequest) {
    return this.http.post(this.baseUrl + '/vacancy-request/create', model).pipe(
      map(() => {
        return model;
      })
    );
  }
  deleteVacancyRequest(vacId: number) {
    return this.http
      .delete(this.baseUrl + `/vacancy-request/delete/${vacId}`)
      .pipe(
        map(() => {
          return 0;
        })
      );
  }
}
