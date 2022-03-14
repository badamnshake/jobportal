import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
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

  login(model: any) {
    return this.http.post<User>(this.baseUrl + '/user/login', model).pipe(
      map((response: User) => {
        const user = response;
        console.log(user);

        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.setCurrentUser(user);
        }
      })
    );
  }
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

  delete() {
    return this.http.delete(this.baseUrl + '/user/delete').pipe(
      map(() => {
        this.logout();
      })
    );
  }
  setCurrentUser(user: User) {
    if (!user) {
      this.logout();
      return;
    }
    this.currentUserSource.next(user);
    let decodedJwt: any = jwtDecode(user.token);
    this.currentRoleSource.next(decodedJwt.role);
  }
  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentRoleSource.next(null);
  }
}
