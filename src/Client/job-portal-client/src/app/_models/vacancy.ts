export interface Vacancy {
  id: number;
  publishedBy: string;
  publishedDate: Date;
  noOfVacancies: number;
  minimumQualification: string;
  jobDescription: string;
  experienceRequired: string;
  location: string;
  lastDateToApply: Date;
  minSalary: number;
  maxSalary: number;
  employerEntityId: number;
}
