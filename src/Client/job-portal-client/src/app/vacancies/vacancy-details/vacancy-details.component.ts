import { Component, OnInit } from '@angular/core';
import { Vacancy } from 'src/app/_models/vacancy';

@Component({
  selector: 'app-vacancy-details',
  templateUrl: './vacancy-details.component.html',
  styleUrls: ['./vacancy-details.component.css']
})
export class VacancyDetailsComponent implements OnInit {

  vacancy: Vacancy;
  constructor() { }

  ngOnInit(): void {
  }

}
