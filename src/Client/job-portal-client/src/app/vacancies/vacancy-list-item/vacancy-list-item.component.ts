import { Component, Input, OnInit } from '@angular/core';
import { Vacancy } from 'src/app/_models/vacancy';

@Component({
  selector: 'app-vacancy-list-item',
  templateUrl: './vacancy-list-item.component.html',
  styleUrls: ['./vacancy-list-item.component.css']
})
export class VacancyListItemComponent implements OnInit {
  @Input() vacancy: Vacancy;

  constructor() { }

  ngOnInit(): void {
  }

}
