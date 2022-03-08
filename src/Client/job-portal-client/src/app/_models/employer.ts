import { Vacancy } from './vacancy';

export interface Employer {
  id: number;
  organizationName: string;
  organizationType: string;
  companyEmail: string;
  companyPhone: string;
  noOfEmployees: number;
  startYear: number;
  about: string;
  vacancies: Vacancies;
}
export interface Vacancies {
  $id: string;
  $values: VacResValue[];
}
export interface VacResValue {
  $id: string;
  id: number;
  publishedBy: string;
  publishedDate: Date;
  noOfVacancies: number;
  minimumQualification: string;
  jobDescription: string;
  experienceRequired?: any;
  location: string;
  lastDateToApply: Date;
  minSalary: number;
  maxSalary: number;
  employerEntityId: number;
}

