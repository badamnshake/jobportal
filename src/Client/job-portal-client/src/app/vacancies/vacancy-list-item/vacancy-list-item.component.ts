import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Employer } from 'src/app/_models/employer';
import { Vacancy } from 'src/app/_models/vacancy';
import { VacancyRequest } from 'src/app/_models/vacancy-request';
import { AccountService } from 'src/app/_services/account.service';
import { EmployerService } from 'src/app/_services/employer.service';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';

@Component({
  selector: 'app-vacancy-list-item',
  templateUrl: './vacancy-list-item.component.html',
  styleUrls: ['./vacancy-list-item.component.css'],
})
export class VacancyListItemComponent implements OnInit {
  @Input() vacancy: Vacancy;
  @Input() isVacancyList: boolean = false;
  @Input() isAppliedVacancies: boolean = false;
  @Input() isEmployerVacancyList: boolean = false;
  @Input() vacanciesApplied: number[] = [];
  employerInfo: Employer = null;

  constructor(
    public accountService: AccountService,
    private employerService: EmployerService,
    private jobSeekerService: JobSeekerService,
    private router: Router,
    private toastr: ToastrService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {}
  async applyForVacancy(id: number) {
    let vr: VacancyRequest = {
      vacancyId: id,
    };
    this.jobSeekerService.createVacancyRequest(vr).subscribe(() => {
      this.vacanciesApplied.push(id);
      localStorage.setItem(
        'vacanciesApplied',
        JSON.stringify(this.vacanciesApplied)
      );
      this.toastr.success('Applied For Vacancy');
    });
  }
  getEmployerDetails(id: number) {
    // this.router.navigateByUrl(`employer-profile/${id}`);
  }
  open(content: any, id: number) {
    this.employerService.getEmployerFromId(id).subscribe((response) => {
      this.employerInfo = response;
      this.modalService
        .open(content, { ariaLabelledBy: 'modal-basic-title' })
        .result.then(
          (result) => {
            this.employerInfo == null
          },
          (reason) => {}
        );
    });
  }
  deleteVacancyRequest(vacId: number) {
    if (confirm('Are you sure you want to delete this Vacancy Request')) {
      this.jobSeekerService.deleteVacancyRequest(vacId).subscribe(() => {
        this.toastr.success('Vacancy Request Deleted');
        // this.router.navigateByUrl('/js-applied-vacancies')
        window.location.reload();
      });
    }
  }
  editVacancy(id: number) {
    this.router.navigateByUrl(`/employer-vacancy-edit/${id}`);
  }
  deleteVacancy(id: number) {
    if (confirm('Are you sure you want to delete this Vacancy')) {
      this.employerService.deleteVacancy(id).subscribe(() => {
        this.toastr.success('Vacancy Deleted');
        window.location.reload();
      });
    }
  }
  viewVacancyRequest(id: number) {
    this.router.navigateByUrl(`/employer-view-vacancy-reqs/${id}`);
  }
}
