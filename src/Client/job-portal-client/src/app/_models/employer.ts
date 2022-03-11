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
  vacancies: Vacancy[];
}