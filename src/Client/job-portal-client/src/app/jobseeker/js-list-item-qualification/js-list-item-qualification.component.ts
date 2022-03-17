import { Component, Input, OnInit } from '@angular/core';
import { Qualification } from 'src/app/_models/qualification';

@Component({
  selector: 'app-js-list-item-qualification',
  templateUrl: './js-list-item-qualification.component.html',
  styleUrls: ['./js-list-item-qualification.component.css']
})
export class JsListItemQualificationComponent implements OnInit {
  @Input() q: Qualification;

  constructor() { }

  ngOnInit(): void {
  }

}
