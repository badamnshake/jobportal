import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployerProfileEditComponent } from './employer/employer-profile-edit/employer-profile-edit.component';
import { EmployerProfileComponent } from './employer/employer-profile/employer-profile.component';
import { EmployerVacancyEditComponent } from './employer/employer-vacancy-edit/employer-vacancy-edit.component';
import { EmployerVacancyListComponent } from './employer/employer-vacancy-list/employer-vacancy-list.component';
import { EmployerViewVacancyReqsComponent } from './employer/employer-view-vacancy-reqs/employer-view-vacancy-reqs.component';
import { JsAppliedVacanciesComponent } from './jobseeker/js-applied-vacancies/js-applied-vacancies.component';
import { JsCreateExperienceComponent } from './jobseeker/js-create-experience/js-create-experience.component';
import { JsCreateQualificationComponent } from './jobseeker/js-create-qualification/js-create-qualification.component';
import { JsEditProfileComponent } from './jobseeker/js-edit-profile/js-edit-profile.component';
import { JsProfileComponent } from './jobseeker/js-profile/js-profile.component';
import { HomeComponent } from './landing-page-components/home/home.component';
import { LoginComponent } from './landing-page-components/login/login.component';
import { RegisterComponent } from './landing-page-components/register/register.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { VacancyListComponent } from './vacancies/vacancy-list/vacancy-list.component';
import { AuthEmployerGuard } from './_guards/auth-employer.guard';
import { AuthJobSeekerGuard } from './_guards/auth-job-seeker.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },

  { path: 'employer-profile/:id', component: EmployerProfileComponent },
  { path: 'vacancy-list', component: VacancyListComponent },

  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthJobSeekerGuard],

    children: [
      {
        path: 'js-profile',
        component: JsProfileComponent,
      },
      {
        path: 'js-edit-profile',
        component: JsEditProfileComponent,
      },
      {
        path: 'js-create-qualification',
        component: JsCreateQualificationComponent,
      },
      {
        path: 'js-create-experience',
        component: JsCreateExperienceComponent,
      },
      {
        path: 'js-applied-vacancies',
        component: JsAppliedVacanciesComponent,
      },
    ],
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthEmployerGuard],

    children: [
      {
        path: 'employer-profile',
        component: EmployerProfileComponent,
      },
      {
        path: 'employer-view-vacancy-reqs/:id',
        component: EmployerViewVacancyReqsComponent,
      },
      {
        path: 'employer-profile-edit',
        component: EmployerProfileEditComponent,
      },
      {
        path: 'employer-vacancy-list',
        component: EmployerVacancyListComponent,
      },
      {
        path: 'employer-vacancy-edit/:id',
        component: EmployerVacancyEditComponent,
      },
    ],
  },
  { path: 'not-found', component: PageNotFoundComponent },
  { path: '**', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
