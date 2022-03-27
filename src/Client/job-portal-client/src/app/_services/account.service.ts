import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { Role } from '../_models/role';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  private currentRoleSource = new ReplaySubject<Role>(1);
  currentUser$ = this.currentUserSource.asObservable();
  currentRole$ = this.currentRoleSource.asObservable();

  constructor(private http: HttpClient) {}

  /* 
   request : POST
   response: User 
   ------------------------------------------------------------------------ 
   login request which logs in the user and gets JWT token back
   if login fails the response is not ok, interceptor
  */
  login(model: any) {
    return this.http.post<User>(this.baseUrl + '/user/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
        }
      })
    );
  }
  /* 
   request : POST
   response: User 
   ------------------------------------------------------------------------ 
   register request which registers the user and gets JWT token back,
   validation error or same email error can occur which will be raised
   as toast by interceptor
  */
  register(model: any) {
    return this.http.post(this.baseUrl + '/user/register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
        }
      })
    );
  }
  /* 
   request : DELETE
   response: 200(OK) 
   ------------------------------------------------------------------------ 
   deletes the user from db and cascades the info db
  */

  delete() {
    return this.http.delete(this.baseUrl + '/user/delete').pipe(
      map(() => {
        this.logout();
      })
    );
  }
  /* 
   ------------------------------------------------------------------------ 
   sets the current user in local storage for future purpose
   as well as sets the variable and observables for other components to use
   also sets the role observable
  */
  setCurrentUser(user: User) {
    if (!user) {
      this.logout();
      return;
    }
    this.currentUserSource.next(user);
    let decodedJwt: any = jwtDecode(user.token);
    this.currentRoleSource.next(decodedJwt.role);
  }
  /* 
   ------------------------------------------------------------------------ 
   logs out the user
  */
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentRoleSource.next(null);
  }
  /* 
   request : PUT
   response: User
   ------------------------------------------------------------------------ 
   updates the users password,
   gets the new token back from the server
  */
  changePassword(model: any) {
    return this.http
      .put<User>(this.baseUrl + '/user/change-password', model)
      .pipe(
        map((response: User) => {
          const user = response;
          if (user) {
            localStorage.setItem('user', JSON.stringify(user));
            this.setCurrentUser(user);
          }
        })
      );
  }
}
