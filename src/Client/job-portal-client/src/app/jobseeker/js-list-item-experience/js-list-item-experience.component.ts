import { Component, Input, OnInit } from '@angular/core';
import { Experience } from 'src/app/_models/experience';

@Component({
  selector: 'app-js-list-item-experience',
  templateUrl: './js-list-item-experience.component.html',
  styleUrls: ['./js-list-item-experience.component.css']
})
export class JsListItemExperienceComponent implements OnInit {

  @Input() e: Experience;
  constructor() { }

  ngOnInit(): void {
  }

}
