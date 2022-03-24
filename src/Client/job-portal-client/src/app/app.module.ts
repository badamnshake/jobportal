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
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { VacancyListComponent } from './vacancies/vacancy-list/vacancy-list.component';
import { JsAppliedVacanciesComponent } from './jobseeker/js-applied-vacancies/js-applied-vacancies.component';
import { JsProfileComponent } from './jobseeker/js-profile/js-profile.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { JsEditProfileComponent } from './jobseeker/js-edit-profile/js-edit-profile.component';
import { JsCreateExperienceComponent } from './jobseeker/js-create-experience/js-create-experience.component';
import { JsCreateQualificationComponent } from './jobseeker/js-create-qualification/js-create-qualification.component';
import { EmployerViewVacancyReqsComponent } from './employer/employer-view-vacancy-reqs/employer-view-vacancy-reqs.component';
import { VacancyListItemComponent } from './vacancies/vacancy-list-item/vacancy-list-item.component';
import { JsListItemProfileComponent } from './jobseeker/js-list-item-profile/js-list-item-profile.component';
import { JsListItemQualificationComponent } from './jobseeker/js-list-item-qualification/js-list-item-qualification.component';
import { JsListItemExperienceComponent } from './jobseeker/js-list-item-experience/js-list-item-experience.component';
import { SeederComponent } from './_seed/seeder/seeder.component';
import { EmployerVacancyCreateComponent } from './employer/employer-vacancy-create/employer-vacancy-create.component';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { RxReactiveFormsModule } from '@rxweb/reactive-form-validators';

              // "node_modules/bootstrap/dist/css/bootstrap.min.css",
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
    VacancyListComponent,
    JsAppliedVacanciesComponent,
    JsProfileComponent,
    JsEditProfileComponent,
    JsCreateExperienceComponent,
    JsCreateQualificationComponent,
    PageNotFoundComponent,
    EmployerViewVacancyReqsComponent,
    VacancyListItemComponent,
    JsListItemProfileComponent,
    JsListItemQualificationComponent,
    JsListItemExperienceComponent,
    SeederComponent,
    EmployerVacancyCreateComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    // forms
    FormsModule,
    ReactiveFormsModule,
    RxReactiveFormsModule,
    HttpClientModule,
    SharedModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: LocationStrategy, useClass: HashLocationStrategy}
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
