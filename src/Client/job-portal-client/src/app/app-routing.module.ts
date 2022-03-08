import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmployerProfileEditComponent } from './employer/employer-profile-edit/employer-profile-edit.component';
import { EmployerProfileComponent } from './employer/employer-profile/employer-profile.component';
import { EmployerVacancyEditComponent } from './employer/employer-vacancy-edit/employer-vacancy-edit.component';
import { EmployerVacancyListComponent } from './employer/employer-vacancy-list/employer-vacancy-list.component';
import { HomeComponent } from './landing-page-components/home/home.component';
import { LoginComponent } from './landing-page-components/login/login.component';
import { RegisterComponent } from './landing-page-components/register/register.component';
import { AuthEmployerGuard } from './_guards/auth-employer.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },

  { path: 'employer-profile/:empId', component: EmployerProfileComponent },

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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
