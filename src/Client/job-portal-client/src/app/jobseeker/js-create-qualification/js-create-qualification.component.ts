import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';

@Component({
  selector: 'app-js-create-qualification',
  templateUrl: './js-create-qualification.component.html',
  styleUrls: ['./js-create-qualification.component.css'],
})
export class JsCreateQualificationComponent implements OnInit {
  qualificationForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  createDetails() {
    this.jobSeekerService
      .createQualification(this.qualificationForm.value)
      .subscribe(() => {
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
}
