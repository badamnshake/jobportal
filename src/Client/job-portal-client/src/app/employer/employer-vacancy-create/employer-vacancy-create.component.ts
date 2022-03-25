import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Vacancy } from 'src/app/_models/vacancy';
import { EmployerService } from 'src/app/_services/employer.service';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';
import { RxwebValidators } from '@rxweb/reactive-form-validators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employer-vacancy-create',
  templateUrl: './employer-vacancy-create.component.html',
  styleUrls: ['./employer-vacancy-create.component.css'],
})
export class EmployerVacancyCreateComponent implements OnInit {
  faCalendar = faCalendar;
  vacancy: Vacancy;
  vacancyCreateForm: FormGroup;
  lastDateToApply: string;

  constructor(
    private fb: FormBuilder,
    private employerService: EmployerService,
    private toastr: ToastrService,
    private ngbDateParserFormatter: NgbDateParserFormatter,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.vacancyCreateForm = this.fb.group({
      noOfVacancies: [0, [Validators.required, RxwebValidators.digit()]],
      minimumQualification: ['', Validators.required],
      jobDescription: ['', Validators.required],
      experienceRequired: ['', Validators.required],
      location: ['', Validators.required],
      lastDateToApply: [''],
      minSalary: [
        0,
        [
          Validators.required,
          RxwebValidators.digit(),
          RxwebValidators.lessThan({ fieldName: 'maxSalary' }),
        ],
      ],
      maxSalary: [
        0,
        [
          Validators.required,
          RxwebValidators.digit(),
          RxwebValidators.greaterThan({ fieldName: 'minSalary' }),
        ],
      ],
    });
  }
  createVacancy() {
    this.vacancyCreateForm.value.lastDateToApply = this.lastDateToApply;
    this.employerService
      .createVacancy(this.vacancyCreateForm.value)
      .subscribe(() => {
        this.router.navigateByUrl('/employer-vacancy-list');
        this.toastr.success('Vacancy Created');
      });
  }
  onLastDateToApplySelect(event: NgbDateStruct) {
    this.lastDateToApply = new Date(
      this.ngbDateParserFormatter.format(event)
    ).toISOString();
  }
  isLastDateValid() {
    return this.lastDateToApply != null;
  }
}
