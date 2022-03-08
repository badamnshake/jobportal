import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import jwtDecode from 'jwt-decode';
import { Employer } from '../_models/employer';
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
  private currentEmpIdSource = new ReplaySubject<number>(1);
  currentEmpId$ = this.currentEmpIdSource.asObservable();

  constructor(private http: HttpClient) {}

  getEmployerFromId(id: number) {
    return this.http
      .get<Employer>(this.baseUrl + `/employer/get-details/${id}`)
      .pipe(
        map((response: Employer) => {
          const employer = response;
          console.log(employer);
          if (employer) {
            localStorage.setItem('empId', id.toString());
            this.setCurrentEmpId(id);
          }
          return response;
        })
      );
  }
  getEmployerFromEmail(email: string) {
    let queryParams = new HttpParams();
    queryParams = queryParams.append('email', email);
    return this.http
      .get<Employer>(this.baseUrl + '/employer/get-details/', {
        params: queryParams,
      })
      .pipe(
        map((response: Employer) => {
          const employer = response;
          console.log(employer);
          if (employer) {
            localStorage.setItem('empId', employer.id.toString());
            this.setCurrentEmpId(employer.id);
          }
          return response;
        })
      );
  }
  createEmployer(model: Employer) {
    return this.http
      .post(this.baseUrl + '/employer/create-details', model)
      .pipe(
        map((id: number) => {
          if (id) {
            localStorage.setItem('empId', id.toString());
            this.setCurrentEmpId(id);
          }
          return id;
        })
      );
  }
  updateEmployer(model: Employer) {
    return this.http
      .post(this.baseUrl + '/employer/update-details', model)
      .pipe(
        map(() => {
          console.log('hello');
        })
      );
  }

  setCurrentEmpId(id: number) {
    this.currentEmpIdSource.next(id);
  }
  removeCurrentEmpId(id: number) {
    this.currentEmpIdSource.next(null);
  }

}
