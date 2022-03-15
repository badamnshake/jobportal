import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Employer } from 'src/app/_models/employer';
import { EmployerService } from 'src/app/_services/employer.service';
@Component({
  selector: 'app-employer-profile',
  templateUrl: './employer-profile.component.html',
  styleUrls: ['./employer-profile.component.css'],
})
export class EmployerProfileComponent implements OnInit {
  employer: Employer;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employerService: EmployerService
  ) {}

  ngOnInit(): void {
    let id = parseInt(this.route.snapshot.paramMap.get('id'));
    if (id !== null) {
      this.employerService.getEmployerFromId(id).subscribe((response) => {
        this.employer = response;
      });
      return;
    }
    this.employerService.getEmployerMe().subscribe((response) => {
      this.employer = response;
    });
  }
}
