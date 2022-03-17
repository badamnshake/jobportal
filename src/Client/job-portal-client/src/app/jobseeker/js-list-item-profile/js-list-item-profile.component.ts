import { Component, Input, OnInit } from '@angular/core';
import { JobSeeker } from 'src/app/_models/job-seeker';

@Component({
  selector: 'app-js-list-item-profile',
  templateUrl: './js-list-item-profile.component.html',
  styleUrls: ['./js-list-item-profile.component.css'],
})
export class JsListItemProfileComponent {
  @Input() js: JobSeeker;
  isCollapsed = true;

  constructor() {}
}
