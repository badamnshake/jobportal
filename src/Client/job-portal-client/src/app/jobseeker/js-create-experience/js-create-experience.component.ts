import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Experience } from 'src/app/_models/experience';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';

@Component({
  selector: 'app-js-create-experience',
  templateUrl: './js-create-experience.component.html',
  styleUrls: ['./js-create-experience.component.css'],
})
export class JsCreateExperienceComponent implements OnInit {
  experienceForm: FormGroup;
  startDate: string;
  endDate: string;

  constructor(
    private fb: FormBuilder,
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService,
    private ngbDateParserFormatter: NgbDateParserFormatter
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  createDetails() {
    this.formatDates();
    this.jobSeekerService
      .createExperience(this.experienceForm.value)
      .subscribe(() => {
        this.toastr.success('Experience Created');
      });
  }

  initializeForm() {
    this.experienceForm = this.fb.group({
      companyName: ['', Validators.required],
      companyUrl: ['', Validators.required],
      startDate: [''],
      endDate: [''],
      designation: ['', Validators.required],
      jobDescription: ['', Validators.required],
    });
  }
  onStartDateSelect(event: NgbDateStruct) {
    this.startDate = new Date(
      this.ngbDateParserFormatter.format(event)
    ).toISOString();
  }
  onEndDateSelect(event: NgbDateStruct) {
    this.endDate = new Date(
      this.ngbDateParserFormatter.format(event)
    ).toISOString();
  }
  formatDates() {
    this.experienceForm.value.endDate = this.endDate;
    this.experienceForm.value.startDate = this.startDate;
  }
}
