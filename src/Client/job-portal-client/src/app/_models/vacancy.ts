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

export enum ToOrderBy {
  MinSalaryAscending = 0,
  MinSalaryDescending = 1,
  MaxSalaryDescending = 2,
  LastDateToApply = 3,
  PublishedDate = 4,
}
