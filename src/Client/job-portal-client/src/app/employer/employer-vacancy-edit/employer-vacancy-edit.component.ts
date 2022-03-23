import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {Vacancy} from 'src/app/_models/vacancy';
import {EmployerService} from 'src/app/_services/employer.service';
import {VacancyService} from 'src/app/_services/vacancy.service';

@Component({
  selector: 'app-employer-vacancy-edit',
  templateUrl: './employer-vacancy-edit.component.html',
  styleUrls: ['./employer-vacancy-edit.component.css'],
})
export class EmployerVacancyEditComponent implements OnInit {
  vacancy: Vacancy;

  constructor(
    private employerService: EmployerService,
    private vacancyService: VacancyService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,

  ) {
  }

  ngOnInit(): void {
    let vacId = parseInt(this.route.snapshot.paramMap.get('id'));

    this.vacancyService.getVacancyFromId(vacId).subscribe((vacancy) => {
      this.vacancy = vacancy;
      // if vacancy is not found then user is on wrong url
      // or vacancy doesn't exist, or server issue (toast will be raised)
      if (!vacancy) this.router.navigateByUrl('/');
    });
  }


  updateDetails() {
    this.employerService.updateVacancy(this.vacancy).subscribe(() => {
      this.router.navigateByUrl('/employer-vacancy-list')
      this.toastr.success('Vacancy Updated');
    });
  }
}
