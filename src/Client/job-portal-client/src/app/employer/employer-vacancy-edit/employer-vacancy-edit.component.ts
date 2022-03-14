import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Vacancy } from 'src/app/_models/vacancy';
import { EmployerService } from 'src/app/_services/employer.service';
import { VacancyService } from 'src/app/_services/vacancy.service';

@Component({
  selector: 'app-employer-vacancy-edit',
  templateUrl: './employer-vacancy-edit.component.html',
  styleUrls: ['./employer-vacancy-edit.component.css'],
})
export class EmployerVacancyEditComponent implements OnInit {
  vacancy: Vacancy;
  vacancyCreate: boolean = undefined;
  submitButtonText: string;
  descriptionText: string;
  loaded = false;

  constructor(
    private employerService: EmployerService,
    private vacancyService: VacancyService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.loaded = false;
    let vacId = parseInt(this.route.snapshot.paramMap.get('id'));
    console.log(vacId);

    if (!vacId) {
      this.vacancyCreate = true;
    } else {
      this.vacancyCreate = false;
      this.vacancyService.getVacancyFromId(vacId).subscribe((vacancy) => {
        this.vacancy = vacancy;
        // if vacancy is not found then user is on wrong url
        // or vacancy doesn't exist, or server issue (toast will be raised)
        if (!vacancy) this.router.navigateByUrl('');
      });
      this.vacancyCreate = false;
    }
    this.setDisplayTexts();
    this.loaded = true;
  }

  setDisplayTexts() {
    if (this.vacancyCreate) {
      this.submitButtonText = 'Create Vacancy';
      this.descriptionText = 'Add Details & Create Vacancy';
    } else {
      this.submitButtonText = 'Update Vacancy';
      this.descriptionText = 'Update Vacancy Details';
    }
  }

  updateOrCreateDetails() {
    if (this.vacancyCreate) {
      this.employerService.createVacancy(this.vacancy).subscribe(() => {
        this.toastr.success('Vacancy Created');
      });
    } else {
      this.employerService.updateVacancy(this.vacancy).subscribe(() => {
        this.toastr.success('Vacancy Updated');
      });
    }
  }
}
