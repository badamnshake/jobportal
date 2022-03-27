import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  // account service to fetch role and display actions according
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }

}
