import { Experience } from './experience';
import { Qualification } from './qualification';
import { VacancyRequest } from './vacancy-request';

export interface JobSeeker {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  address: string;
  totalExperience: number;
  expectedSalaryAnnual: number;
  dateOfBirth: Date | string;
  qualifications: Qualification[];
  experiences: Experience[];
  vacancyRequests: VacancyRequest[];
}
