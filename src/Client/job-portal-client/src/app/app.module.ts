import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbDatepickerModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegisterComponent } from './landing-page-components/register/register.component';
import { HomeComponent } from './landing-page-components/home/home.component';
import { NavbarComponent } from './landing-page-components/navbar/navbar.component';
import { LoginComponent } from './landing-page-components/login/login.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EmployerProfileComponent } from './employer/employer-profile/employer-profile.component';
import { EmployerVacancyEditComponent } from './employer/employer-vacancy-edit/employer-vacancy-edit.component';
import { EmployerVacancyListComponent } from './employer/employer-vacancy-list/employer-vacancy-list.component';
import { EmployerProfileEditComponent } from './employer/employer-profile-edit/employer-profile-edit.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { SharedModule } from './_modules/shared.module';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { VacancyPageComponent } from './vacancies/vacancy-page/vacancy-page.component';
import { VacancyDetailsComponent } from './vacancies/vacancy-details/vacancy-details.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    HomeComponent,
    NavbarComponent,
    LoginComponent,
    EmployerProfileComponent,
    EmployerProfileEditComponent,
    EmployerVacancyEditComponent,
    EmployerVacancyListComponent,
    VacancyPageComponent,
    VacancyDetailsComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
