import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Employer } from 'src/app/_models/employer';
import { EmployerService } from 'src/app/_services/employer.service';

@Component({
  selector: 'app-employer-profile-edit',
  templateUrl: './employer-profile-edit.component.html',
  styleUrls: ['./employer-profile-edit.component.css'],
})
export class EmployerProfileEditComponent implements OnInit {
  submitButtonText: string;
  descriptionText: string;
  doesEmpExist = true;
  loaded = false;

  employer: Employer;
  constructor(
    private router: Router,
    private employerService: EmployerService
  ) {}

  ngOnInit(): void {
    this.employerService.getEmployerMe().subscribe((response) => {
      this.checkForEmployerAndSetValues(response);
    });
    this.setDisplayTexts();
    this.loaded = true;
  }

  checkForEmployerAndSetValues(response: Employer) {
    this.employer = response;
    if (!this.employer) this.doesEmpExist = false;
  }

  setDisplayTexts() {
    if (!this.doesEmpExist) {
      this.submitButtonText = 'Create Profile';
      this.descriptionText = 'Add Details  & Create Profile';
    } else {
      this.submitButtonText = 'Update Details';
      this.descriptionText = 'Profile Settings';
    }
  }
  updateOrCreateDetails() {
    if (!this.doesEmpExist) {
      this.employerService.createEmployer(this.employer).subscribe(() => {});
    } else {
      this.employerService.updateEmployer(this.employer).subscribe({
        next: () => {},
        error: () => {
          console.log('failed to udpate');
        },
      });
    }
  }
}
