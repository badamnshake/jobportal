import { Vacancy } from '../vacancy';

export interface GetVacancyResponse {
  $id: string;
  $values: Vacancy[];
}
