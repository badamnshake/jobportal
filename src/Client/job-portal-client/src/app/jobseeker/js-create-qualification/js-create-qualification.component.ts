import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';
import { NavigationService } from 'src/app/_services/navigation.service';

@Component({
  selector: 'app-js-create-qualification',
  templateUrl: './js-create-qualification.component.html',
  styleUrls: ['./js-create-qualification.component.css'],
})
export class JsCreateQualificationComponent implements OnInit {
  qualificationForm: FormGroup;
  endDate: string;

  constructor(
    private fb: FormBuilder,
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService,
    private ngbDateParserFormatter: NgbDateParserFormatter,
    private navigationService: NavigationService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  createDetails() {
    this.formatDates();
    this.jobSeekerService
      .createQualification(this.qualificationForm.value)
      .subscribe(() => {
        this.navigationService.back();
        this.toastr.success('Qualification Created');
      });
  }

  initializeForm() {
    this.qualificationForm = this.fb.group({
      qualificationName: ['', Validators.required],
      university: ['', Validators.required],
      dateOfCompletion: ['', Validators.required],
      gradeOrScore: ['', Validators.maxLength(3)],
    });
  }
  onEndDateSelect(event: NgbDateStruct) {
    this.endDate = new Date(
      this.ngbDateParserFormatter.format(event)
    ).toISOString();
  }
  formatDates() {
    this.qualificationForm.value.dateOfCompletion = this.endDate;
  }
}
