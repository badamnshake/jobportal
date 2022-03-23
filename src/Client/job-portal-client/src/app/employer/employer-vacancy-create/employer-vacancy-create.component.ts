import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Vacancy } from 'src/app/_models/vacancy';
import { EmployerService } from 'src/app/_services/employer.service';

@Component({
  selector: 'app-employer-vacancy-create',
  templateUrl: './employer-vacancy-create.component.html',
  styleUrls: ['./employer-vacancy-create.component.css'],
})
export class EmployerVacancyCreateComponent implements OnInit {
  vacancy: Vacancy;
  vacancyCreateForm: FormGroup;
  lastDateToApply: string;

  constructor(
    private fb: FormBuilder,
    private employerService: EmployerService,
    private toastr: ToastrService,
    private ngbDateParserFormatter: NgbDateParserFormatter
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.vacancyCreateForm = this.fb.group({
      publishedBy: ['', Validators.required],
      publishedDate: ['', Validators.required],
      noOfVacancies: [0, Validators.required],
      minimumQualification: ['', Validators.required],
      jobDescription: ['', Validators.required],
      experienceRequired: ['', Validators.required],
      location: ['', Validators.required],
      lastDateToApply: ['', Validators.required],
      minSalary: [0, Validators.required],
      maxSalary: [0, Validators.required],
    });
  }
  createVacancy() {
    this.vacancyCreateForm.value.lastDateToApply = this.lastDateToApply;
    this.employerService
      .createVacancy(this.vacancyCreateForm.value)
      .subscribe(() => {
        // this.navigation.back();
        this.toastr.success('Vacancy Created');
      });
  }
  onLastDateToApplySelect(event: NgbDateStruct) {
    this.lastDateToApply = new Date(
      this.ngbDateParserFormatter.format(event)
    ).toISOString();
  }
}
