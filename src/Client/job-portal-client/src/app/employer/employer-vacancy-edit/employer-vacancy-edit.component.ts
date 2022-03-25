import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  NgbDateParserFormatter,
  NgbDateStruct,
} from '@ng-bootstrap/ng-bootstrap';
import { RxwebValidators } from '@rxweb/reactive-form-validators';
import { ToastrService } from 'ngx-toastr';
import { Vacancy } from 'src/app/_models/vacancy';
import { EmployerService } from 'src/app/_services/employer.service';
import { VacancyService } from 'src/app/_services/vacancy.service';
import { faCalendar } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-employer-vacancy-edit',
  templateUrl: './employer-vacancy-edit.component.html',
  styleUrls: ['./employer-vacancy-edit.component.css'],
})
export class EmployerVacancyEditComponent implements OnInit {
  faCalendar = faCalendar;
  vacancy: Vacancy;
  vacancyUpdateForm: FormGroup;
  lastDateToApply: string;

  constructor(
    private fb: FormBuilder,
    private employerService: EmployerService,
    private vacancyService: VacancyService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private ngbDateParserFormatter: NgbDateParserFormatter
  ) {}

  ngOnInit(): void {
    let vacId = parseInt(this.route.snapshot.paramMap.get('id'));

    this.initializeForm();
    this.vacancyService.getVacancyFromId(vacId).subscribe((vacancy) => {
      this.vacancy = vacancy;
      // if vacancy is not found then user is on wrong url
      // or vacancy doesn't exist, or server issue (toast will be raised)
      if (!vacancy) this.router.navigateByUrl('/');
      this.patchValuesIntoForm();
    });
  }

  initializeForm() {
    this.vacancyUpdateForm = this.fb.group({
      publishedDate: ['', Validators.required],
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
  patchValuesIntoForm() {
    this.vacancyUpdateForm.patchValue({
      publishedBy: this.vacancy.publishedBy,
      publishedDate: this.vacancy.publishedDate,
      noOfVacancies: this.vacancy.noOfVacancies,
      minimumQualification: this.vacancy.minimumQualification,
      jobDescription: this.vacancy.jobDescription,
      experienceRequired: this.vacancy.experienceRequired,
      location: this.vacancy.location,
      minSalary: this.vacancy.minSalary,
      maxSalary: this.vacancy.maxSalary,
      lastDateToApply: this.ngbDateParserFormatter.parse(
        this.vacancy.lastDateToApply.toString()
      ),
    });
    this.lastDateToApply = new Date(
      this.ngbDateParserFormatter.format(
        this.vacancyUpdateForm.value.lastDateToApply
      )
    ).toISOString();
  }
  updateVacancy() {
    this.vacancyUpdateForm.value.lastDateToApply = this.lastDateToApply;
    this.vacancyUpdateForm.value.id = this.vacancy.id;
    this.employerService
      .updateVacancy(this.vacancyUpdateForm.value)
      .subscribe(() => {
        this.router.navigateByUrl('/employer-vacancy-list');
        this.toastr.success('Vacancy Updated');
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
