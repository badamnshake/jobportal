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
import { RxwebValidators } from '@rxweb/reactive-form-validators';

@Component({
  selector: 'app-js-create-qualification',
  templateUrl: './js-create-qualification.component.html',
  styleUrls: ['./js-create-qualification.component.css'],
})
export class JsCreateQualificationComponent implements OnInit {
  qualificationForm: FormGroup;
  endDate: string;
  faCalendar = faCalendar;

  constructor(
    private fb: FormBuilder,
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService,
    private ngbDateParserFormatter: NgbDateParserFormatter,
    private router: Router // private navigationService: NavigationService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  createDetails() {
    this.formatDates();
    this.jobSeekerService
      .createQualification(this.qualificationForm.value)
      .subscribe(() => {
        this.router.navigateByUrl('js-profile');
        this.toastr.success('Qualification Created');
      });
  }

  initializeForm() {
    this.qualificationForm = this.fb.group({
      qualificationName: ['', Validators.required],
      university: ['',  Validators.required],
      dateOfCompletion: [''],
      gradeOrScore: ['',[ Validators.maxLength(5), Validators.required]],
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
  isLastDateValid() {
    return this.endDate != null;
  }
}
