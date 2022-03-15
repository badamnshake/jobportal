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
import { VacancyDetailsComponent } from './vacancies/vacancy-details/vacancy-details.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { VacancyListComponent } from './vacancies/vacancy-list/vacancy-list.component';
import { JsAppliedVacanciesComponent } from './jobseeker/js-applied-vacancies/js-applied-vacancies.component';
import { JsProfileComponent } from './jobseeker/js-profile/js-profile.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { JsEditProfileComponent } from './jobseeker/js-edit-profile/js-edit-profile.component';
import { JsCreateExperienceComponent } from './jobseeker/js-create-experience/js-create-experience.component';
import { JsCreateQualificationComponent } from './jobseeker/js-create-qualification/js-create-qualification.component';

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
    VacancyDetailsComponent,
    VacancyListComponent,
    JsAppliedVacanciesComponent,
    JsProfileComponent,
    JsEditProfileComponent,
    JsCreateExperienceComponent,
    JsCreateQualificationComponent,
    PageNotFoundComponent,
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
