import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Employer } from 'src/app/_models/employer';
import { User } from 'src/app/_models/user';
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

  employer: Employer;
  constructor(
    private router: Router,
    private employerService: EmployerService
  ) {}

  ngOnInit(): void {
    let id: number = parseInt(localStorage.getItem('empId'));
    if (id) {
      this.employerService.getEmployerFromId(id).subscribe((response) => {
        this.checkForEmployerAndSetValues(response);
      });
    } else {
      let user: User = JSON.parse(localStorage.getItem('user'));
      this.employerService
        .getEmployerFromEmail(user.email)
        .subscribe((response) => {
          this.checkForEmployerAndSetValues(response);
        });
    }
  }

  checkForEmployerAndSetValues(response: Employer) {
    this.employer = response;
    if (!this.employer) this.doesEmpExist = false;
    else localStorage.setItem('empId', this.employer.id.toString());
  }

  setDisplayTexts() {
    if (this.doesEmpExist) {
      this.submitButtonText = 'Create Profile';
      this.descriptionText = 'Add Details  & Create Profile';
    } else {
      this.submitButtonText = 'Update Details';
      this.descriptionText = 'Profile Settings';
    }
  }
  updateOrCreateDetails() {
    if (this.doesEmpExist) {
      this.employerService.createEmployer(this.employer).subscribe((empId) => {
        localStorage.setItem('empId', empId.toString());
      });
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
