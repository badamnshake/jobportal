import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Employer } from 'src/app/_models/employer';

@Component({
  selector: 'app-employer-vacancy-list',
  templateUrl: './employer-vacancy-list.component.html',
  styleUrls: ['./employer-vacancy-list.component.css']
})
export class EmployerVacancyListComponent implements OnInit {

  employer: Employer;
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.employer.id = params['empId'];
      console.log(this.employer.id);
    });
  }

}
