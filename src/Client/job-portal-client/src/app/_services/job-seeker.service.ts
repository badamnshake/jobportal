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

  /* 
   request : GET
   response: JobSeeker
   ------------------------------------------------------------------------ 
   gets the currently logged in job seekers info
  */
  getMyDetails() {
    return this.http.get<JobSeeker>(this.baseUrl + '/job-seeker/get/').pipe(
      map((response: JobSeeker) => {
        return response;
      })
    );
  }
  /* 
   request : GET
   response: number[] (array of vacancy Id's)
   ------------------------------------------------------------------------ 
   gets the currently logged in job seekers vacancy requests
   ** paginated **
  */
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
  /* 
   request : GET
   response: number[] (array of vacancy Id's)
   ------------------------------------------------------------------------ 
   gets the currently logged in job seekers vacancy requests
   **  non paginated **
   the difference between this and paginated version is that
   paginated is used for display and this for UI efficiency
  */
  getVacanciesWhereIAppliedAll() {
    return this.http
      .get<number[]>(
        this.baseUrl + '/vacancy-request/get-vacancies-where-i-applied-all'
      )
      .pipe(
        map((response) => {
          return response;
        })
      );
  }
  // it sets in the local storage where js has applied so From UI job seekers
  // can't apply to same vacancy again
  setVacanciesWhereJSApplied() {
    this.getVacanciesWhereIAppliedAll().subscribe((response) => {
      localStorage.setItem('vacanciesApplied', JSON.stringify(response));
    });
  }
  /* 
   request : POST
   response: number (job seeker Id)
   ------------------------------------------------------------------------ 
   creates job seekers profile on the server
  */
  createJobSeeker(model: JobSeeker) {
    return this.http.post(this.baseUrl + '/job-seeker/create', model).pipe(
      map((id: number) => {
        return id;
      })
    );
  }
  /* 
   request : PUT
   response: stats code 200
   ------------------------------------------------------------------------ 
   updates job seekers profile on the server
  */
  updateJobSeeker(model: JobSeeker) {
    return this.http
      .put(this.baseUrl + '/job-seeker/update', model)
      .pipe(map(() => {}));
  }

  /* 
   request : POST
   response: stats code 200
   ------------------------------------------------------------------------ 
   creates job seekers experience field
  */
  createExperience(model: Experience) {
    return this.http.post(this.baseUrl + '/experience/create', model).pipe(
      map(() => {
        return model;
      })
    );
  }
  /* 
   request : DELETE
   response: stats code 200
   ------------------------------------------------------------------------ 
   deletes job seekers experience field
  */
  deleteExperience(id: number) {
    return this.http.delete(this.baseUrl + `/experience/delete/${id}`).pipe(
      map(() => {
        return 0;
      })
    );
  }
  /* 
   request : POST
   response: stats code 200
   ------------------------------------------------------------------------ 
   creates job seekers qualfication field
  */
  createQualification(model: Qualification) {
    return this.http.post(this.baseUrl + '/qualification/create', model).pipe(
      map(() => {
        return model;
      })
    );
  }
  /* 
   request : DELETE
   response: stats code 200
   ------------------------------------------------------------------------ 
   deletes job seekers qualification field
  */
  deleteQualification(id: number) {
    return this.http.delete(this.baseUrl + `/qualification/delete/${id}`).pipe(
      map(() => {
        return 0;
      })
    );
  }
  /* 
   request : POST
   response: stats code 200
   ------------------------------------------------------------------------ 
   creates new vacancy request on a vacancy
  */
  createVacancyRequest(model: VacancyRequest) {
    return this.http.post(this.baseUrl + '/vacancy-request/create', model).pipe(
      map(() => {
        return model;
      })
    );
  }
  /* 
   request : DELETE
   response: stats code 200
   ------------------------------------------------------------------------ 
   deletes a  vacancy request on a vacancy
  */
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
