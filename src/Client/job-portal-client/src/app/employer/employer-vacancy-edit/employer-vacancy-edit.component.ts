import { Component, OnInit } from '@angular/core';
import { Vacancy } from 'src/app/_models/vacancy';

@Component({
  selector: 'app-employer-vacancy-edit',
  templateUrl: './employer-vacancy-edit.component.html',
  styleUrls: ['./employer-vacancy-edit.component.css']
})
export class EmployerVacancyEditComponent implements OnInit {
  vacancy: Vacancy;

  constructor() { }

  ngOnInit(): void {
  }

  updateOrCreateDetails() {

  }
}
