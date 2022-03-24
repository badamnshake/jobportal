import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';

@Component({
  selector: 'app-js-create-experience',
  templateUrl: './js-create-experience.component.html',
  styleUrls: ['./js-create-experience.component.css'],
})
export class JsCreateExperienceComponent implements OnInit {
  faCalendar = faCalendar;
  experienceForm: FormGroup;
  startDate: string;
  endDate: string;

  // date things
  dateErrorMsg: string;

  constructor(
    private fb: FormBuilder,
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService,
    private ngbDateParserFormatter: NgbDateParserFormatter,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  createDetails() {
    this.formatDates();
    this.jobSeekerService
      .createExperience(this.experienceForm.value)
      .subscribe(() => {
        this.router.navigateByUrl('js-profile');
        this.toastr.success('Experience Created');
      });
  }

  initializeForm() {
    this.experienceForm = this.fb.group({
      companyName: ['', Validators.required],
      companyUrl: [
        '',
        [
          Validators.required,
          Validators.pattern(
            '^[a-zA-Z0-9][a-zA-Z0-9-]{1,61}[a-zA-Z0-9](?:.[a-zA-Z]{2,})+$'
          ),
        ],
      ],
      startDate: [''],
      endDate: [''],
      designation: ['', Validators.required],
      jobDescription: ['', Validators.minLength(10)],
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
  areDatesValid() {
    if (this.startDate == null || this.endDate == null) {
      this.dateErrorMsg = 'Start and End Dates are Required';
      return false;
    }

    if (
      this.startDate != null &&
      this.endDate != null &&
      new Date(this.startDate.substring(0, 10)) >
        new Date(this.endDate.substring(0, 10))
    ) {
      this.dateErrorMsg = 'End date should be greater then start date.';
      return false;
    }
    return true;
  }
}
