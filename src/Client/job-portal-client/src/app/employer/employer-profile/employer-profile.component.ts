import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Employer } from 'src/app/_models/employer';
import { EmployerService } from 'src/app/_services/employer.service';
@Component({
  selector: 'app-employer-profile',
  templateUrl: './employer-profile.component.html',
  styleUrls: ['./employer-profile.component.css'],
})
export class EmployerProfileComponent implements OnInit {
  @Input()
  employer: Employer;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employerService: EmployerService
  ) {}

  ngOnInit(): void {
    // let id = parseInt(this.route.snapshot.paramMap.get('id'));

    // if (id) {
    //   this.employerService.getEmployerFromId(id).subscribe((response) => {
    //     this.employer = response;
    //   });
    //   return;
    // }
    // this.employerService.getEmployerMe().subscribe((response) => {
    //   this.employer = response;
    // });
  }
}
