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
    this.route.paramMap.subscribe((params) => {
      let id: number;
      
      id =  parseInt(params.get('empId'))
        ? parseInt(params.get('empId'))
        : parseInt(localStorage.getItem('empId'));
      if (!id) {
        this.router.navigateByUrl('/employer-profile-edit');
      } else {
        this.employerService.getEmployerFromId(id).subscribe((response) => {
          this.employer = response;
        });
      }
    });
  }
}
