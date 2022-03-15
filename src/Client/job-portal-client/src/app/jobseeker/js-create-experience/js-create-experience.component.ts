import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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

  constructor(
    private fb: FormBuilder,
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  createDetails() {
    this.jobSeekerService.createExperience(this.experienceForm.value).subscribe(() => {
      this.toastr.success('Experience Created');
    });
  }

  initializeForm() {
    this.experienceForm = this.fb.group({
      companyName: ['', Validators.required],
      companyUrl: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      designation: ['', Validators.required],
      jobDescription: [''],
    });
  }
}