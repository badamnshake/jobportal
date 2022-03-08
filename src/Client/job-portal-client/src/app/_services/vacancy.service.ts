import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { Vacancy } from '../_models/vacancy';

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
  getVacanciesFromDate(page?: number, itemsPerPage?: number) {
    let queryParams = new HttpParams();
    if (page != null && itemsPerPage != null) {
      queryParams = queryParams.append('pageNumber', page.toString());
      queryParams = queryParams.append('pageSize', itemsPerPage.toString());
    }
    return this.http
      .get<Vacancy[]>(this.baseUrl + '/vacancy/get-details/date', {
        observe: 'response',
        params: queryParams,
      })
      .pipe(
        map((response) => {
          this.paginatedResult.result = response.body;
          if(response.headers.get('Pagination') !== null) {
            this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'))
          }
          return this.paginatedResult;
        })
      );
  }
  getVacanciesFromOrganizationName(email: string) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('email', email);
    return this.http
      .get<Vacancy[]>(this.baseUrl + '/vacancy/get-details/orgName', {
        params: queryParams,
      })
      .pipe(
        map((response: Vacancy[]) => {
          return response;
        })
      );
  }
  getVacanciesFromLocation(email: string) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('email', email);
    return this.http
      .get<Vacancy[]>(this.baseUrl + '/vacancy/get-details/location', {
        params: queryParams,
      })
      .pipe(
        map((response: Vacancy[]) => {
          return response;
        })
      );
  }
  getVacanciesFromSalary(email: string) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('email', email);
    return this.http
      .get<Vacancy[]>(this.baseUrl + '/vacancy/get-details/salary', {
        params: queryParams,
      })
      .pipe(
        map((response: Vacancy[]) => {
          return response;
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
}
