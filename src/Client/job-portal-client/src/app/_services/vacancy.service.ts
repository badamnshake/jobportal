import { HttpBackend, HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { ToOrderBy, Vacancy } from '../_models/vacancy';

@Injectable({
  providedIn: 'root',
})
export class VacancyService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<Vacancy[]> = new PaginatedResult();
  private httpBackend: HttpClient;

  constructor(private http: HttpClient,
    private handler: HttpBackend
    ) {

    this.httpBackend = new HttpClient(handler);
    }

  /* 
   request : GET
   response: vacancy
   ------------------------------------------------------------------------ 
   gets a vacancy from ID
  */
  getVacancyFromId(id: number) {
    return this.httpBackend.get<Vacancy>(this.baseUrl + `/vacancy/get/${id}`).pipe(
      map((response: Vacancy) => {
        return response;
      }),
    );
  }
  /* 
   request : GET
   response: vacancy[]
   ------------------------------------------------------------------------ 
   with filtration and pagination 
   it gets various vacancies
   query params received are used as filters
   ///
   when no filters applied a filled anyFilters passed in to server
   as it increases performance
  */
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
    let anyFilters = true;
    let queryParams = new HttpParams();
    if (page != null && itemsPerPage != null) {
      // adding pagination
      queryParams = queryParams.append('pageNumber', page.toString());
      queryParams = queryParams.append('pageSize', itemsPerPage.toString());

      if (
        location == null &&
        minSalary == null &&
        maxSalary == null &&
        lastDateToApply == null &&
        orderBy == null &&
        publishedDate == null 
      )
        anyFilters = false;
      if (location != null)
        queryParams = queryParams.append('location', location.trim());
      if (minSalary != null)
        queryParams = queryParams.append('minSalary', minSalary.toString());
      if (maxSalary != null)
        queryParams = queryParams.append('maxSalary', maxSalary.toString());
      if (lastDateToApply != null)
        queryParams = queryParams.append(
          'lastDateToApply',
          lastDateToApply.toISOString()
        );
      if (publishedDate != null)
        queryParams = queryParams.append(
          'publishedDate',
          publishedDate.toISOString()
        );
      if (orderBy != null) {
        let x: number = orderBy;
        queryParams = queryParams.append('orderBy', x.toString());
      }
      queryParams = queryParams.append('anyFilters', anyFilters);
    }
    return this.http
      .get<Vacancy[]>(this.baseUrl + '/vacancy/get-vacancies/', {
        observe: 'response',
        params: queryParams,
      })
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
}
