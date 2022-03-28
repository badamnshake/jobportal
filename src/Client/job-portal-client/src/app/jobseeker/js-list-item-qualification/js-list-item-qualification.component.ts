import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Qualification } from 'src/app/_models/qualification';
import { JobSeekerService } from 'src/app/_services/job-seeker.service';

@Component({
  selector: 'app-js-list-item-qualification',
  templateUrl: './js-list-item-qualification.component.html',
  styleUrls: ['./js-list-item-qualification.component.css']
})
export class JsListItemQualificationComponent implements OnInit {
  @Input() q: Qualification;
  @Input() showDeleteButton = false;

  constructor(
    private jobSeekerService: JobSeekerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
  }

  deleteQual(id: number) {
    if (confirm('Are you sure you want to delete this Qualification')) {
      this.jobSeekerService.deleteQualification(id).subscribe(() => {
        this.toastr.success('Qualification Deleted');
        window.location.reload();
      });
    }
  }
}
