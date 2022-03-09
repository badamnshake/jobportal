import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { Vacancy } from 'src/app/_models/vacancy';
import { VacancyService } from 'src/app/_services/vacancy.service';

@Component({
  selector: 'app-vacancy-list',
  templateUrl: './vacancy-list.component.html',
  styleUrls: ['./vacancy-list.component.css'],
})
export class VacancyListComponent implements OnInit {
  vacancies: Vacancy[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 5;

  constructor(private vacancyService: VacancyService) {}

  ngOnInit(): void {
    this.loadVacancies();
  }

  loadVacancies() {
    this.vacancyService.getVacancies(this.pageNumber, this.pageSize).subscribe((response) => {
      this.vacancies = response.result;
      console.log(this.vacancies);
      
      this.pagination = response.pagination;
    })
  }


  pageChanged(event: any){
    
    this.pageNumber = event;
    this.loadVacancies()
  }
}