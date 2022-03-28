import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Experience } from 'src/app/_models/experience';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';

@Component({
  selector: 'app-js-list-item-experience',
  templateUrl: './js-list-item-experience.component.html',
  styleUrls: ['./js-list-item-experience.component.css'],
})
export class JsListItemExperienceComponent implements OnInit {
  // experience data
  @Input() e: Experience;
  @Input() showDeleteButton = false;
  constructor(
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  deleteExp(id: number) {
    if (confirm('Are you sure you want to delete this Experience')) {
      this.jobSeekerService.deleteExperience(id).subscribe(() => {
        this.toastr.success('Experince Deleted');
        window.location.reload();
      });
    }
  }
}
